
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class MaskWriteRegisterResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\MaskWriteRegister")]
        [Timeout(1000)]
        public void MaskWriteRegistersResponse()
        {
            
            var request = new MaskWriteRegisterRequest(1, 9, 0xf2, 0x25, true);
            request.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);
            var response = new MaskWriteRegisterResponse(request);


            ModbusResponse pdu = new ModbusResponse(0x16, new byte[] { 0x0, 0xf2, 0x0, 0x25 });
            response.SetResponsePDU(pdu);

            var respData = response.GetData();

            Assert.AreEqual(1, ((ModbusRequestIdentifier)response.Request.RequestIdentifier).GetRequestId());
            Assert.AreEqual(0xf2, response.GetAndMask());
            Assert.AreEqual(0x25, response.GetOrMask());     

        }

    }
}
