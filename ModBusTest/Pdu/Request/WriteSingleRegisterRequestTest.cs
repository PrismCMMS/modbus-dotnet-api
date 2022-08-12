using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.DaacoWorks.Modbus.Pdu.Util;

namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class WriteSingleRegisterRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleRegister")]
        [Timeout(1000)]
        public void WriteSingleRegisterRequest_1digitHexStartAddress()
        {
            WriteSingleRegister(9, 2);
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleRegister")]
        [Timeout(1000)]
        public void WriteSingleRegisterRequest_2digitHexStartAddress()
        {
            WriteSingleRegister(9, 100);
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleRegister")]
        [Timeout(1000)]
        public void WriteSingleRegisterRequest_3digitHexStartAddress()
        {
            WriteSingleRegister(9, 624);
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleRegister")]
        [Timeout(1000)]
        public void WriteSingleRegisterRequest_4digitHexStartAddress()
        {
            WriteSingleRegister(9, 39321);
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleRegister")]
        [Timeout(1000)]
        [Ignore]
        public void WriteSingleRegisterRequest_InvalidInput_address()
        {
            try
            {
                client.Submit(new WriteSingleRegisterRequest(0xFF, ushort.MaxValue, 9, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleRegister")]
        [Timeout(1000)]
        [Ignore]
        public void WriteSingleRegisterRequest_InvalidInput_value()
        {
            try
            {
                client.Submit(new WriteSingleRegisterRequest(0xFF, 9, ushort.MaxValue, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        private void WriteSingleRegister(ushort address, ushort value)
        {

            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new WriteSingleRegisterRequest(0xFF, address, value, true)).Get();
            Assert.IsTrue(response is WriteSingleRegisterResponse);

            var success = (ReadHoldingRegistersResponse)client.Submit(new ReadHoldingRegistersRequest(0xFF, 9, 1, true)).Get();

            Assert.IsTrue(ModbusUtil.ToInt32(success.GetData(), 0) == value);

        }

    }
}