
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class WriteSingleCoilResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteSingleCoilResponse_GetStartingAddress()
        {            
            var response = GetWriteSingleCoilResponse();

            Assert.AreEqual(0xAC, response.GetStartingAddress());
           
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteSingleCoilResponse_GetQuantity()
        {
            var response = GetWriteSingleCoilResponse();

            var actualCoilState = response.GetCoilState();
            Assert.AreEqual(CoilState.ON, actualCoilState);
        }

        public WriteSingleCoilResponse GetWriteSingleCoilResponse()
        {

            var request = new WriteSingleCoilRequest(1, 0xAC, CoilState.ON, true); 
            var response = new WriteSingleCoilResponse(request);


            ModbusResponse pdu = new ModbusResponse(0x05, new byte[] { (byte)0x00, (byte)0xAC, 0xFF, 0x00 });
            response.SetResponsePDU(pdu);

            return response;
        }
    }
}
