using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Logger;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Com.DaacoWorks.Modbus.Pdu.Util
{
    /// <summary>
    /// ModbusUtil class has a set of utility methods.
    /// </summary>
    public class ModbusUtil
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusUtil).FullName);

        private static byte[] asciiTable = new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46 };

        /// <summary>
        /// Converts integer value to hexa decimal byte array
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="size">the size</param>
        /// <returns>hexa byte array</returns>
        public static byte[] ToHexByteArray(int data, int size)
        {
            return ToHexByteArray(data.ToString("X2"), size);
        }

        /// <summary>
        /// Converts hex string to hexa byte array
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] ToHexByteArray(String hexString, int size)
        {

            hexString = hexString.Length % 2 == 1 ? "0" + hexString : hexString;
            var data = ParseHexBinary(hexString);

            if (data.Length < size)
            {
                using (var buffer = new MemoryStream(size))
                {
                    for (int i = 0; i < size - data.Length; i++)
                    {
                        buffer.WriteByte(0x00);
                    }
                    buffer.Write(data, 0, data.Length);
                    return buffer.ToArray();
                }
            }
            return data;
        }

        private static byte[] ParseHexBinary(String data)
        {
            var len = data.Length;

            byte[] output = new byte[len / 2];

            for (int i = 0; i < len; i += 2)
            {
                int h = HexToBin(data[i]);
                int l = HexToBin(data[i + 1]);
                if (h == -1 || l == -1)
                    throw new ArgumentException("contains illegal character for hexBinary: " + data);

                output[i / 2] = (byte)(h * 16 + l);
            }

            return output;
        }

        private static int HexToBin(char ch)
        {
            if ('0' <= ch && ch <= '9') return ch - '0';
            if ('A' <= ch && ch <= 'F') return ch - 'A' + 10;
            if ('a' <= ch && ch <= 'f') return ch - 'a' + 10;
            return -1;
        }

        /// <summary>
        /// Gets the CRC16 byte array for a given data array.
        /// </summary>
        /// <param name="dataArr">byte array</param>
        /// <returns>CRC16 byte array</returns>
        public static byte[] CRC16(byte[] dataArr)
        {
            int crc = 0xFFFF; //step 1

            foreach (byte dataByte in dataArr)
            {
                crc = (crc ^ (0x000000FF & dataByte)); //step 2
                //logger.Debug(string.Format("dataByte {0} xor {1,-16}", dataByte, Convert.ToString(0x0000FFFF & crc, 2)));
                crc = ExamineLSB(crc);
                //logger.Debug(string.Format("dataByte  {0} crc {1}", dataByte, Convert.ToString(0x0000FFFF & crc, 16)));
            }//steps 6
            //logger.Debug("CRC value " + Convert.ToString(crc, 16));
            var crcBytes = new byte[2];
            return BitConverter.GetBytes((short)crc); //steps 7, 8
        }

        private static int ExamineLSB(int crc)
        {
            //steps 3, 4 , 5
            for (int i = 0; i < 8; i++)
            {
                if ((crc & 0x0001) == 1)
                {
                    crc = ((crc >> 1) ^ 0xA001);
                    //logger.Debug(string.Format("{0} shift xor crc {1,-16}", (i + 1), Convert.ToString(0x0000FFFF & crc, 2)));
                }
                else
                {
                    crc = (crc >> 1);
                    //logger.Debug(string.Format("{0} shift crc {1}", (i + 1), Convert.ToString(0x0000FFFF & crc, 16)));
                }
            }

            return crc;
        }

        private static byte[] GetAsciiTable()
        {
            return asciiTable;
        }

        /// <summary>
        /// Gets LRC byte array for a given data array
        /// </summary>
        /// <param name="dataArr">data array</param>
        /// <returns>LRC byte array</returns>
        public static byte[] LRC(byte[] dataArr)
        {
            byte lrc = 0x00;
            byte[] lrcArr = new byte[2];
            foreach (byte data in dataArr)
            {
                lrc = (byte)((byte)(lrc + data) & 0xFF);
            }

            lrc = (byte)(~lrc + 1);

            byte hsb = (byte)(lrc >> 4);
            hsb = (byte)(0x0F & hsb);
            lrcArr[0] = asciiTable[hsb];
            byte lsb = (byte)(lrc & 0x0F);
            lrcArr[1] = asciiTable[lsb];
            return lrcArr;
        }

        /// <summary>
        /// Gets ASCII byte array for hex byte array.
        /// </summary>
        /// <param name="data">byte array</param>
        /// <returns>ascii byte array</returns>
        public static byte[] GetASCII(byte[] data)
        {
            byte[] res = new byte[data.Length * 2];
            int counter = 0;
            foreach (byte byt in data)
            {
                byte temp = (byte)(0xF0 & byt);
                temp = (byte)(temp >> 4);
                temp = (byte)(0x0F & temp);
                res[counter++] = ModbusUtil.GetAsciiTable()[temp]; //MSB               
                res[counter++] = ModbusUtil.GetAsciiTable()[byt & 0x0F]; //LSB
            }
            return res;
        }

        /// <summary>
        /// converts the byte array into float value.
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="byteSwap">the order of bytes should be swapped within a two byte word or not</param>
        /// <param name="wordSwap">the order of words should be swapped or not</param>
        /// <returns>the float[]</returns>
        public static float[] ToFloatValue(byte[] data, bool byteSwap, bool wordSwap)
        {

            if (data == null && data.Length % 4 != 0)
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_FLOAT_VALUE_MSG);
            }

            float[] floatValues = new float[data.Length / 4];
            for (int i = 0, floatIndex = 0; i < data.Length; i += 4)
            {
                var floatBuf = new byte[4];
                CopyBytes(data, byteSwap, wordSwap, floatBuf, i);
                floatValues[floatIndex++] = BitConverter.ToSingle(floatBuf, 0);

            }

            return floatValues;
        }

        private static void CopyBytes(byte[] data, bool byteSwap, bool wordSwap, byte[] floatBuf, int index)
        {
            if (byteSwap)
            {

                if (wordSwap)
                {
                    floatBuf[0] = data[index + 3];
                    floatBuf[1] = data[index + 2];
                    floatBuf[2] = data[index + 1];
                    floatBuf[3] = data[index];
                }
                else
                {
                    floatBuf[0] = data[index + 1];
                    floatBuf[1] = data[index];
                    floatBuf[2] = data[index + 3];
                    floatBuf[3] = data[index + 2];
                }
            }
            else
            {
                if (wordSwap)
                {
                    floatBuf[0] = data[index + 2];
                    floatBuf[1] = data[index + 3];
                    floatBuf[2] = data[index];
                    floatBuf[3] = data[index + 1];
                }
                else
                {
                    floatBuf[0] = data[index];
                    floatBuf[1] = data[index + 1];
                    floatBuf[2] = data[index + 2];
                    floatBuf[3] = data[index + 3];
                }
            }
        }

        /// <summary>
        /// Converts byte array and a position given, converts to 32 bit integer
        /// </summary>
        /// <param name="data">byte array</param>
        /// <param name="index">start position in the byte array</param>
        /// <returns>32 bit int value</returns>
        public static int ToInt32(byte[] data, int index)
        {
            if (data.Length == 0 || index >= data.Length)
                throw new ArgumentException("Insufficient data to convert or index out of range");

            var dataLength = data.Length - index;

            switch (dataLength)
            {
                case 1:
                    return data[index];
                case 2:
                    return (data[index++] << 8) | data[index];
                case 3:
                    return (data[index++] << 16) | (data[index++] << 8) | data[index];
                default:
                    return (data[index++] << 24) | (data[index++] << 16) | (data[index++] << 8) | data[index];
            }
        }


        /// <summary>
        /// Converts byte array and a position given, converts to 16 bit integer
        /// </summary>
        /// <param name="data">byte array</param>
        /// <param name="index">start position in the byte array</param>
        /// <returns>16 bit int value</returns>
        public static ushort ToInt16(byte[] data, int index)
        {
            if (data.Length == 0 || index >= data.Length)
                throw new ArgumentException("Insufficient data to convert or index out of range");

            var dataLength = data.Length - index;

            switch (dataLength)
            {
                case 1:
                    return data[index];
                default:
                    return (ushort)((data[index++] << 8) | data[index]);
            }
        }

        internal static short ToShortWithBytesSwapped(int value)
        {
            var shortValue = (short)value;

            return (short)(((shortValue & 0xFF00) >> 8) | ((shortValue & 0x00FF) << 8));
        }

        /// <summary>
        /// Converts the byte array into integer value
        /// </summary>
        /// <param name="data">byte array data</param>
        /// <param name="byteSwap">the order of bytes should be swapped within a two byte word or not</param>
        /// <param name="wordSwap">the order of words should be swapped or not</param>
        /// <returns></returns>
        public static int[] ToIntValue(byte[] data, bool byteSwap, bool wordSwap)
        {
            if (data == null && data.Length % 4 != 0)
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_INT_VALUE_MSG);
            }

            int[] intValues = new int[data.Length / 4];
            for (int i = 0, floatIndex = 0; i < data.Length; i += 4)
            {
                var intBuffer = new byte[4];
                CopyBytes(data, byteSwap, wordSwap, intBuffer, i);
                intValues[floatIndex++] = BitConverter.ToInt32(intBuffer, 0);
            }

            return intValues;
        }


    }

}