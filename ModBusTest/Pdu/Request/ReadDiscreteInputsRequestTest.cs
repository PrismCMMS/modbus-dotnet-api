using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class ReadDiscreteInputsRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputs")]
        [Timeout(1000)]
        public void ReadDiscreteInputsRequest_Success_1digitHexStartAddress()
        {
            ReadDiscreteInputs(12, 2);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputs")]
        [Timeout(1000)]
        public void ReadDiscreteInputsRequest_Success_2digitsHexStartAddress()
        {
            ReadDiscreteInputs(110, 2);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputs")]
        [Timeout(1000)]
        public void ReadDiscreteInputsRequest_Success_3digitsHexStartAddress()
        {
            ReadDiscreteInputs(629, 2);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputs")]
        [Timeout(1000)]
        public void ReadDiscreteInputsRequest_Success_4digitsHexStartAddress()
        {
            ReadDiscreteInputs(39321, 2);
        }

        private void ReadDiscreteInputs(ushort address, ushort quantity)
        {

            var futureResp = client.Submit(new ReadDiscreteInputsRequest(0xFF, address, quantity, true));
            Assert.IsTrue(futureResp.Get() is ModbusSuccessResponse);

        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputs")]
        [Timeout(1000)]
        [Ignore]
        public void ReadDiscreteInputsRequest_InvalidInput_quantity()
        {
            try
            {
                client.Submit(new ReadDiscreteInputsRequest(0xFF, 100, 2001, true));
                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputs")]
        [Timeout(1000)]
        [Ignore]
        public void ReadDiscreteInputsRequest_InvalidInput_65536()
        {
            try
            {
                client.Submit(new ReadDiscreteInputsRequest(0xFF, 65535, 2, true));
                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

    }
}