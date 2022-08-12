using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class ReadHoldingRegistersRequestTest : RequestTestBase
    {


        [TestMethod]
        [TestCategory("Modbus\\ReadHoldingRegisters")]
        [Timeout(1000)]
        public void ReadHoldingRegistersRequest_1digitHexStartAddress()
        {
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new ReadHoldingRegistersRequest(0x01, 142, 2, true)).Get();
            float[] value = ModbusUtil.ToFloatValue(((ModbusSuccessResponse)response).GetData(), false, true);

            Assert.IsTrue(response is ModbusSuccessResponse);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadHoldingRegisters")]
        [Timeout(1000)]
        public void ReadHoldingRegistersRequest_2digitsHexStartAddress()
        {
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new ReadHoldingRegistersRequest(0xFF, 100, 2, true)).Get();

            Assert.IsTrue(response is ModbusSuccessResponse);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadHoldingRegisters")]
        [Timeout(1000)]
        public void ReadHoldingRegistersRequest_3digitsHexStartAddress()
        {
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new ReadHoldingRegistersRequest(0xFF, 624, 2, true)).Get();

            Assert.IsTrue(response is ModbusSuccessResponse);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadHoldingRegisters")]
        [Timeout(1000)]
        public void ReadHoldingRegistersRequest_4digitsHexStartAddress()
        {
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new ReadHoldingRegistersRequest(0xFF, 39321, 2, true)).Get();

            Assert.IsTrue(response is ModbusSuccessResponse);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadHoldingRegisters")]
        [Timeout(1000)]
        [Ignore]
        public void ReadHoldingRegistersRequest_InvalidInput_quantity()
        {
            try
            {
                client.Submit(new ReadHoldingRegistersRequest(0xFF, 100, 126, true));

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadHoldingRegisters")]
        [Timeout(1000)]
        [Ignore]
        public void ReadHoldingRegistersRequest_InvalidInput_65536()
        {
            try
            {
                client.Submit(new ReadHoldingRegistersRequest(0xFF, 65535, 2, true));

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

    }
}