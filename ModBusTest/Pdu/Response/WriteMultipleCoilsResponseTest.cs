
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class WriteMultipleCoilsResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleCoils")]
        [Timeout(1000)]
        public void WriteMultipleCoilsResponse_GetStartingAddress()
        {
            var expectedWriteValues = new byte[] { 0xCD, 0x01 };
            var response = GetWriteMultipleCoilsResponse(expectedWriteValues);

            Assert.AreEqual(19, response.GetStartingAddress());
           
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleCoils")]
        [Timeout(1000)]
        public void WriteMultipleCoilsResponse_GetQuantity()
        {
            var expectedWriteValues = new byte[] { 0xCD, 0x01 };
            var response = GetWriteMultipleCoilsResponse(expectedWriteValues);

            var actualQuantity = response.GetQuantity();
            Assert.AreEqual(10, actualQuantity);
        }

        public WriteMultipleCoilsResponse GetWriteMultipleCoilsResponse(byte[] writeValues)
        {

            var request = new WriteMultipleCoilsRequest(1, 19, 10, true);            
            request.WriteValues = writeValues;
            var response = new WriteMultipleCoilsResponse(request);


            ModbusResponse pdu = new ModbusResponse((byte)0F, new byte[] { (byte)0x00, (byte)0x13, 0x00, 0x0A });
            response.SetResponsePDU(pdu);

            return response;
        }
    }
}
