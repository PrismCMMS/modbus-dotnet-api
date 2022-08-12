using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using Com.DaacoWorks.Protocol.Util;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// ModbusASCIIOverTCPUDPDecoder is a decoder class invoked to decode the ModbusASCII responses.
    /// </summary>
    public class ModbusASCIIOverTCPUDPDecoder : IDecoder<Protocol.Model.Response>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusASCIIOverTCPUDPDecoder).FullName);

        private byte[] value = new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46 };

        /// <summary>
        /// Decode the bytes available in the input buffer
        /// </summary>
        /// <param name="connectionParameters"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public Protocol.Model.Response Decode(ConnectionParameters connectionParameters, MemoryStream input)
        {
            if (PerformLRC(input))
            {
                input.Capacity = (input.Capacity - 2);
                int slaveId = input.ReadByte();
                int functionCode = input.ReadByte();
                int pduLength = input.Remaining();
                return (new ModbusResponseParser()).ParseModbusResponsePDU(new ModbusRequestIdentifier(connectionParameters.GetHost(), functionCode, slaveId), pduLength, functionCode, input);
            }
            else
            {
                logger.Warn("Incorrect Modbus Response. CRC check failed. data " + ProtocolUtils.GetHexByteString(input));
                return null;
            }
        }

        /// <summary>
        /// Gets the data from ASCII.
        /// </summary>
        /// <param name="data">the data</param>
        /// <returns>data from ASCII</returns>
        protected byte[] GetDataFromASCII(byte[] data)
        {
            byte[] res = new byte[data.Length / 2];
            int counter = 0; // start reading after header bytes
            int i = 0;
            while (counter < data.Length)
            {
                byte hsb = data[counter++];
                //convert to ASCII char
                char hsbChar = (char)hsb;
                byte lsb = data[counter++];
                char lsbChar = (char)lsb;
                var hexString = hsbChar + "" + lsbChar;
                var result = (byte)short.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
                res[i++] = result;
            }
            return res;
        }

        /// <summary>
        /// Perform LRC.
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <returns>true, if successful</returns>
        protected bool PerformLRC(MemoryStream buffer)
        {
            byte[] data = new byte[buffer.Capacity - 2];
            buffer.Read(data);
            byte[] computedLRC = ModbusUtil.LRC(GetDataFromASCII(data));
            data = new byte[2];//read the LRC bytes
            buffer.Read(data);
            buffer.Flip();
            return computedLRC[0] == data[0] && computedLRC[1] == data[1];
        }
    }
}