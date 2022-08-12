using Com.DaacoWorks.Modbus.Pdu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ModBusTest.Pdu.Util
{
    [TestClass]
    public class ModbusUtilTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestCRC16()
        {
            byte[] data = new byte[] { (byte)0xFD, (byte)0xFC };
            byte[] crc = ModbusUtil.CRC16(data);
            Assert.IsTrue(crc[0] == 65 && crc[1] == 97);

            data = new byte[] { (byte)0x88, (byte)0x88 };
            crc = ModbusUtil.CRC16(data);
            Assert.IsTrue(crc[0] == 103 && crc[1] == 0xD6);

            data = new byte[] { (byte)0x65, (byte)0x65 };
            crc = ModbusUtil.CRC16(data);
            Assert.IsTrue(crc[0] == 0xEA && crc[1] == 0xCB);

            data = new byte[] { (byte)0xFF, (byte)0xFF };
            crc = ModbusUtil.CRC16(data);
            Assert.IsTrue(crc[0] == 0 && crc[1] == 0);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestLRC()
        {
            byte[] data = new byte[] { 0x11, 0x03, 0x00, 0x6B, 0x00, 0x03 };
            byte[] lrc = ModbusUtil.LRC(data);
            Assert.IsTrue(lrc[0] == 0x37 && lrc[1] == 0x45);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToHexByteArray_1Character()
        {
            var expected = new byte[] { 0x0A };
            int data = 0x0A;
            var actual = ModbusUtil.ToHexByteArray(data, 1);
            CompareBytes(expected, actual);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToHexByteArray_2Characters()
        {
            var expected = new byte[] { 0xAB };
            int data = 0xAB;
            var actual = ModbusUtil.ToHexByteArray(data, 1);
            CompareBytes(expected, actual);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToHexByteArray_3Characters()
        {
            var expected = new byte[] { 0x0A, 0xBC };
            int data = 0x0ABC;
            var actual = ModbusUtil.ToHexByteArray(data, 2);
            CompareBytes(expected, actual);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToHexByteArray_4Characters()
        {
            var expected = new byte[] { 0xAB, 0xCD };
            int data = 0xABCD;
            var actual = ModbusUtil.ToHexByteArray(data, 2);
            CompareBytes(expected, actual);
        }

        private void CompareBytes(byte[] expected, byte[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestGetASCII()
        {
            byte[] data = new byte[] { 0x5B };
            byte[] res = ModbusUtil.GetASCII(data);
            Assert.IsTrue(res[0] == 0x35 && res[1] == 0x42);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestGetASCII2()
        {
            byte[] data = new byte[] { 0x03, 0x00, 0x6B, 0x00, 0x03, 0x7E };
            byte[] res = ModbusUtil.GetASCII(data);
            Assert.IsTrue(res[0] == 0x30 && res[11] == 0x45);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_NoSwap()
        {
            float f = 233.9f;
            byte[] data = ToByteArray(f);

            float[] floatVals = ModbusUtil.ToFloatValue(data, false, false);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);

        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_NoSwap1()
        {
            float f = 231.4f;
            byte[] data = ToByteArray(f); 

            float[] floatVals = ModbusUtil.ToFloatValue(data, false, false);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_NoSwap2()
        {
            float[] f = { 3.14f, 2.28f, 4.55f };
            byte[] data = ToByteArray(f);

            float[] floatVals = ModbusUtil.ToFloatValue(data, false, false);

            Assert.AreEqual(3, floatVals.Length);
            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(f[0], res);
            res = float.Parse(floatVals[1].ToString("n2"));
            Assert.AreEqual(f[1], res);
            res = float.Parse(floatVals[2].ToString("n2"));
            Assert.AreEqual(f[2], res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapByte()
        {
            float f = 233.9f;
            byte[] data = SwapByte(ToByteArray(f));

            float[] floatVals = ModbusUtil.ToFloatValue(data, true, false);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);

        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapByte1()
        {
            float f = 231.4f;
            byte[] data = SwapByte(ToByteArray(f));

            float[] floatVals = ModbusUtil.ToFloatValue(data, true, false);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapByte2()
        {
            float[] f = { 3.14f, 2.28f, 4.55f };
            byte[] data = SwapByte(ToByteArray(f));

            float[] floatVals = ModbusUtil.ToFloatValue(data, true, false);

            Assert.AreEqual(3, floatVals.Length);
            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(f[0], res);
            res = float.Parse(floatVals[1].ToString("n2"));
            Assert.AreEqual(f[1], res);
            res = float.Parse(floatVals[2].ToString("n2"));
            Assert.AreEqual(f[2], res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapWord()
        {
            float f = 233.9f;
            byte[] data = SwapWord(ToByteArray(f));

            float[] floatVals = ModbusUtil.ToFloatValue(data, false, true);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);

        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapWord1()
        {
            float f = 231.4f;
            byte[] data = SwapWord(ToByteArray(f));

            float[] floatVals = ModbusUtil.ToFloatValue(data, false, true);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapWord2()
        {
            float[] f = { 3.14f, 2.28f, 4.55f };
            byte[] data = SwapWord(ToByteArray(f));

            float[] floatVals = ModbusUtil.ToFloatValue(data, false, true);

            Assert.AreEqual(3, floatVals.Length);
            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(f[0], res);
            res = float.Parse(floatVals[1].ToString("n2"));
            Assert.AreEqual(f[1], res);
            res = float.Parse(floatVals[2].ToString("n2"));
            Assert.AreEqual(f[2], res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapByteAndWord()
        {
            float f = 233.9f;
            byte[] data = SwapWord(SwapByte(ToByteArray(f)));

            float[] floatVals = ModbusUtil.ToFloatValue(data, true, true);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);

        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapByteAndWord1()
        {
            float f = 231.4f;
            byte[] data = SwapWord(SwapByte(ToByteArray(f)));

            float[] floatVals = ModbusUtil.ToFloatValue(data, true, true);

            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(1, floatVals.Length);
            Assert.AreEqual(f, res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToFloatValues_SwapByteAndWord2()
        {
            float[] f = { 3.14f, 2.28f, 4.55f };
            byte[] data = SwapWord(SwapByte(ToByteArray(f)));

            float[] floatVals = ModbusUtil.ToFloatValue(data, true, true);

            Assert.AreEqual(3, floatVals.Length);
            float res = float.Parse(floatVals[0].ToString("n2"));
            Assert.AreEqual(f[0], res);
            res = float.Parse(floatVals[1].ToString("n2"));
            Assert.AreEqual(f[1], res);
            res = float.Parse(floatVals[2].ToString("n2"));
            Assert.AreEqual(f[2], res);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValue_NoSwap()
        {
            int i = 65365;
            byte[] data = ToByteArray(i);

            int[] intVal = ModbusUtil.ToIntValue(data, false, false);

            Assert.AreEqual(i, intVal[0]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_NoSwap2()
        {
            int[] i = { 32128, 52535 };
            byte[] data = ToByteArray(i);

            int[] intVals = ModbusUtil.ToIntValue(data, false, false);

            Assert.AreEqual(2, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_NoSwap3()
        {
            int[] i = { 32128, 52535, 42345 };
            byte[] data = ToByteArray(i);

            int[] intVals = ModbusUtil.ToIntValue(data, false, false);

            Assert.AreEqual(3, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
            Assert.AreEqual(i[2], intVals[2]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValue_SwapByte()
        {
            int i = 65365;
            byte[] data = SwapByte(ToByteArray(i));

            int[] intVal = ModbusUtil.ToIntValue(data, true, false);

            Assert.AreEqual(i, intVal[0]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_SwapByte2()
        {
            int[] i = { 32128, 52535 };
            byte[] data = SwapByte(ToByteArray(i));

            int[] intVals = ModbusUtil.ToIntValue(data, true, false);

            Assert.AreEqual(2, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_SwapByte3()
        {
            int[] i = { 32128, 52535, 42345 };
            byte[] data = SwapByte(ToByteArray(i));

            int[] intVals = ModbusUtil.ToIntValue(data, true, false);

            Assert.AreEqual(3, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
            Assert.AreEqual(i[2], intVals[2]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValue_SwapWord()
        {
            int i = 65365;
            byte[] data = SwapWord(ToByteArray(i));

            int[] intVal = ModbusUtil.ToIntValue(data, false, true);

            Assert.AreEqual(i, intVal[0]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_SwapWord2()
        {
            int[] i = { 32128, 52535 };
            byte[] data = SwapWord(ToByteArray(i));

            int[] intVals = ModbusUtil.ToIntValue(data, false, true);

            Assert.AreEqual(2, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_SwapWord3()
        {
            int[] i = { 32128, 52535, 42345 };
            byte[] data = SwapWord(ToByteArray(i));

            int[] intVals = ModbusUtil.ToIntValue(data, false, true);

            Assert.AreEqual(3, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
            Assert.AreEqual(i[2], intVals[2]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValue_SwapByteAndWord()
        {
            int i = 65365;
            byte[] data = SwapWord(SwapByte(ToByteArray(i)));

            int[] intVal = ModbusUtil.ToIntValue(data, true, true);

            Assert.AreEqual(i, intVal[0]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_SwapByteAndWord2()
        {
            int[] i = { 32128, 52535 };
            byte[] data = SwapWord(SwapByte(ToByteArray(i)));

            int[] intVals = ModbusUtil.ToIntValue(data, true, true);

            Assert.AreEqual(2, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusUtil")]
        public void TestToIntValues_SwapByteAndWord3()
        {
            int[] i = { 32128, 52535, 42345 };
            byte[] data = SwapWord(SwapByte(ToByteArray(i)));

            int[] intVals = ModbusUtil.ToIntValue(data, true, true);

            Assert.AreEqual(3, intVals.Length);
            Assert.AreEqual(i[0], intVals[0]);
            Assert.AreEqual(i[1], intVals[1]);
            Assert.AreEqual(i[2], intVals[2]);
        }

        private byte[] ToByteArray(float f)
        {
            using (var buffer1 = new MemoryStream(4))
            {
                using (var bw = new BinaryWriter(buffer1))
                {
                    bw.Write(f);
                    var data = buffer1.ToArray();
                    return new byte[] { data[0], data[1], data[2], data[3]};
                }
            }
        }

        private byte[] SwapByte(byte[] data)
        {
            var result = new byte[data.Length];
            for(int index=0; index<data.Length; index+=2)
            {
                result[index] = data[index + 1];
                result[index+1] = data[index];
            }
            return result;
        }

        private byte[] SwapWord(byte[] data)
        {
            var result = new byte[data.Length];
            for (int index = 0; index < data.Length; index += 4)
            {
                result[index] = data[index + 2];
                result[index + 1] = data[index + 3];
                result[index + 2] = data[index + 0];
                result[index + 3] = data[index + 1];
            }

            return result;
        }

        private byte[] ToByteArray(float[] floats)
        {
            var buffer = new byte[floats.Length * 4];

            for (int index = 0, bufferIndex = 0; index < floats.Length; index++, bufferIndex += 4)
            {
                var data = ToByteArray(floats[index]);
                buffer[bufferIndex] = data[0];
                buffer[bufferIndex + 1] = data[1];
                buffer[bufferIndex + 2] = data[2];
                buffer[bufferIndex + 3] = data[3];
            }
            return buffer;

        }

        private byte[] ToByteArray(byte[] data, int[] dataOrder)
        {
            var buffer = new byte[dataOrder.Length];
            for (var index = 0; index < dataOrder.Length; index++)
            {
                buffer[index] = data[dataOrder[index]];
            }
            return buffer;
        }

        private byte[] ToByteArray(int i)
        {
            using (var buffer1 = new MemoryStream(4))
            {
                using (var bw = new BinaryWriter(buffer1))
                {
                    bw.Write(i);
                    var data = buffer1.ToArray();
                    return new byte[] { data[0], data[1], data[2], data[3], };
                }
            }
        }

        private byte[] ToByteArray(int[] ints)
        {
            var buffer = new byte[ints.Length * 4];

            for (int index = 0, bufferIndex = 0; index < ints.Length; index++, bufferIndex += 4)
            {
                var data = ToByteArray(ints[index]);
                buffer[bufferIndex] = data[0];
                buffer[bufferIndex + 1] = data[1];
                buffer[bufferIndex + 2] = data[2];
                buffer[bufferIndex + 3] = data[3];
            }
            return buffer;
        }

    }
}
