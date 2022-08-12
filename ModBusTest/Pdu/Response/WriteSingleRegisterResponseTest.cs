
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class WriteSingleRegisterResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteSingleRegisterResponse_GetStartingAddress()
        {            
            var response = GetWriteSingleRegisterResponse();

            Assert.AreEqual(1, response.GetStartingAddress());
           
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteSingleRegisterResponse_GetQuantity()
        {
            var response = GetWriteSingleRegisterResponse();

            var actualValue = response.GetValue();
            Assert.AreEqual(3, actualValue);
        }

        public WriteSingleRegisterResponse GetWriteSingleRegisterResponse()
        {

            var request = new WriteSingleRegisterRequest(1, 1, 3, true); 
            var response = new WriteSingleRegisterResponse(request);


            ModbusResponse pdu = new ModbusResponse(0x06, new byte[] { (byte)0x00, (byte)0x01, 0x00, 0x03 });
            response.SetResponsePDU(pdu);

            return response;
        }
    }
}
