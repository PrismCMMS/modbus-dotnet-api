using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class ReadCoilRequestTest : RequestTestBase
    {        

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        //[Timeout(1000)]
        public void ReadCoilsRequest_Success_1digitHexStartAddress()
        {
            ReadCoils(1, 12);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilsRequest_Success_2digitHexStartAddress()
        {
            ReadCoils(100, 2);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilsRequest_Success_3digitHexStartAddress()
        {
            ReadCoils(624, 2);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilsRequest_Success_4digitHexStartAddress()
        {
            ReadCoils(39321, 2);
        }
        //TODO: submit the request using callback option and schedule option. This has to be done for all the test classes.
        private void ReadCoils(ushort address, ushort quantity)
        {

            Com.DaacoWorks.Protocol.Model.Response success = client.Submit(new ReadCoilsRequest(0xFF, address, quantity, true)).Get();
            Assert.IsTrue(success is ModbusSuccessResponse);

        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        [Ignore]
        public void ReadCoilsRequest_InvalidInput_quantity()
        {
            try
            {
                requestPDU = new ReadCoilsRequest(0xFF, 100, 2001, true);
                client.Submit(requestPDU);
                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        [Ignore]
        public void ReadCoilsRequest_InvalidInput_65536()
        {
            try
            {
                requestPDU = new ReadCoilsRequest(0xFF, 65535, 2, true);

                client.Submit(requestPDU);

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }
    }
}
