using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class WriteMultipleCoilsRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleCoils")]
        [Timeout(1000)]
        public void WriteMultipleCoilsRequest_Success_for_8Bits()
        {
            WriteMultipleCoil(9, 8, new byte[] { (byte)0xFF });
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleCoils")]
        [Timeout(1000)]
        public void WriteMultipleCoilsRequest_Success_for_10Bits()
        {
            WriteMultipleCoil(9, 10, new byte[] { (byte)0xCD, (byte)0x01 });
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleCoils")]
        [Timeout(1000)]
        [Ignore]
        public void WriteMultipleCoilsRequest_InvalidInput_address()
        {
            try
            {
                client.Submit(new WriteMultipleCoilsRequest(0xFF, ushort.MaxValue, 1, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleCoils")]
        [Timeout(1000)]
        [Ignore]
        public void WriteMultipleCoilsRequest_InvalidInput_quantity()
        {
            try
            {
                client.Submit(new WriteMultipleCoilsRequest(0xFF, 65535, 0x7B1, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        private void WriteMultipleCoil(ushort address, ushort quantity, byte[] value)
        {

            requestPDU = new WriteMultipleCoilsRequest(0xFF, address, quantity, true);
            ((WriteMultipleCoilsRequest)requestPDU).WriteValues = value;
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(requestPDU).Get();
            Assert.IsTrue(response is ModbusSuccessResponse);

            ModbusSuccessResponse success = (ModbusSuccessResponse)client.Submit(new ReadCoilsRequest(0xFF, address, quantity, true)).Get();

            var input = value;
            var output = success.GetData();
            for (var i = 0; i < input.Length; i++)
            {
                Assert.IsTrue(input[i] == output[i]);
            }


        }


    }
}