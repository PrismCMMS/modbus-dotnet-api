using Com.DaacoWorks.Modbus.Codec;
using System.IO;
using TestSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Codec
{
    [TestClass]
    public class ModbusAsCIIOverTCPUDPDecoderTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ModbusAsCIIOverTCPUDPDecoder")]
        public void TestPerformLRC()
        {
            byte[] dataWithLRC = new byte[] { 0x31, 0x31, 0x30, 0x33, 0x30, 0x30, 0x36, 0x42, 0x30, 0x30, 0x30, 0x33, 0x37, 0x45 };
            var decoder = new ModbusASCIIOverTCPUDPDecoder_Accessor();
            Assert.IsTrue(decoder.PerformLRC_Accessor(new MemoryStream(dataWithLRC)));
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusAsCIIOverTCPUDPDecoder")]
        public void TestGetDataFromASCII()
        {
            var decoder = new ModbusASCIIOverTCPUDPDecoder_Accessor();
            byte[] data = new byte[] { 0x31, 0x32 };
            byte[] res = decoder.GetDataFromASCII_Accessor(data);
            Assert.IsTrue(res[0] == 18);
        }


        internal class ModbusASCIIOverTCPUDPDecoder_Accessor
        {
            private ModbusASCIIOverTCPUDPDecoder modbusASCIIOverTCPUDPDecoder;

            public ModbusASCIIOverTCPUDPDecoder_Accessor()
            {
                modbusASCIIOverTCPUDPDecoder = new ModbusASCIIOverTCPUDPDecoder();
            }
            public byte[] GetDataFromASCII_Accessor(byte[] data)
            {
                return (byte[])modbusASCIIOverTCPUDPDecoder.RunPrivateMethod("GetDataFromASCII", data);
            }

            public bool PerformLRC_Accessor(MemoryStream buf)
            {
                return (bool)modbusASCIIOverTCPUDPDecoder.RunPrivateMethod("PerformLRC", buf);
            }
        }
    }
}