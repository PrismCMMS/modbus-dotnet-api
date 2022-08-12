
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu
{

    [TestClass]
    public class ModbusSuccessResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ModbusSuccessResponse")]
        [Timeout(1000)]
        public void ModbusSuccessResponse_Constructor()
        {

            var request = new ReadCoilsRequest(1, 9, 2, true);
            request.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);
            var response = new ModbusSuccessResponseMock(request);
            

            ModbusResponse pdu = new ModbusResponse((byte)3, new byte[] { (byte)0x02, (byte)0x2B, 0x00, 0x64 });          

            Assert.AreEqual(1, ((ModbusRequestIdentifier)response.Request.RequestIdentifier).GetRequestId());
            Assert.AreSame(request, response.Request);


        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusSuccessResponse")]
        [Timeout(1000)]
        public void ModbusSuccessResponse_SetResponsePDU()
        {

            var request = new ReadCoilsRequest(1, 9, 2, true);
            request.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);
            var response = new ModbusSuccessResponseMock(request);


            ModbusResponse pdu = new ModbusResponse((byte)3, new byte[] { (byte)0x02, (byte)0x2B, 0x00, 0x64 });
            response.SetResponsePDU(pdu);

            byte[] respData = response.GetData();

            Assert.AreEqual(1, ((ModbusRequestIdentifier)response.Request.RequestIdentifier).GetRequestId());
            Assert.AreSame(pdu, response.GetResponse());
            Assert.AreEqual(0, response.GetLength());
            Assert.IsTrue(0x02 == respData[0] && 0x2B == respData[1] &&
                            0x00 == respData[2] && 0x64 == respData[3]);


        }

    }

    
}
