
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class WriteMultipleRegistersResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegistersResponse_GetStartingAddress()
        {
            var expectedWriteValues = new ushort[] { 0x000A, 0x0102 };
            var response = GetWriteMultipleRegistersResponse(expectedWriteValues);

            Assert.AreEqual(1, response.GetStartingAddress());
           
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegistersResponse_GetQuantity()
        {
            var expectedWriteValues = new ushort[] { 0x000A, 0x0102 };
            var response = GetWriteMultipleRegistersResponse(expectedWriteValues);

            var actualQuantity = response.GetQuantity();
            Assert.AreEqual(2, actualQuantity);
        }

        public WriteMultipleRegistersResponse GetWriteMultipleRegistersResponse(ushort[] writeValues)
        {

            var request = new WriteMultipleRegistersRequest(1, 1, 2, true);            
            request.WriteValues = writeValues;
            var response = new WriteMultipleRegistersResponse(request);


            ModbusResponse pdu = new ModbusResponse(0x10, new byte[] { (byte)0x00, (byte)0x01, 0x00, 0x02 });
            response.SetResponsePDU(pdu);

            return response;
        }
    }
}
