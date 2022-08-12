
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class ReadInputRegistersResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadInputRegister")]
        [Timeout(1000)]
        public void ReadInputRegisterResponse()
        {

            ReadInputRegistersRequest holdReg = new ReadInputRegistersRequest(1, 9, 2, true);
            holdReg.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);
            ReadInputRegistersResponse response = new ReadInputRegistersResponse(holdReg);


            ModbusResponse pdu = new ModbusResponse((byte)4, new byte[] { (byte)0x02, (byte)0x2B, 0x00, 0x64 });
            response.SetResponsePDU(pdu);

            byte[] respData = response.GetData();

            Assert.AreEqual(1, ((ModbusRequestIdentifier)response.Request.RequestIdentifier).GetRequestId());
            Assert.IsTrue(0x02 == respData[0] && 0x2B == respData[1] &&
                               0x00 == respData[2] && 0x64 == respData[3]);


        }
    }
}
