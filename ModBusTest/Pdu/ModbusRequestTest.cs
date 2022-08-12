
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModBusTest.Pdu
{

    [TestClass]
    public class ModbusRequestTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ModbusSuccessResponse")]
        [Timeout(1000)]
        public void ModbusRequest_Constructor()
        {

            var request = new ModbusRequestMock(1, 9, 2, true);
            request.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);

            Assert.AreEqual(1, request.SlaveId);
            Assert.AreEqual(9, request.StartAddress);
            Assert.AreEqual(2, request.Quantity);
            Assert.AreEqual(5, request.GetLength());
            Assert.AreEqual(true, request.IsConvertToHex);
            Assert.AreEqual(0x01, request.GetFunctionCode());
        }

        [TestMethod]
        [TestCategory("Modbus\\ModbusSuccessResponse")]
        [Timeout(1000)]
        public void ModbusRequest_GetDataInBytes()
        {

            var request = new ModbusRequestMock(1, 9, 2, true);
            request.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);

            byte[] requestData = request.GetDataInBytes();

            Assert.AreEqual(5, requestData.Length);           
            Assert.IsTrue(0x01 == requestData[0] && 0x0 == requestData[1] &&
                            0x09 == requestData[2] && 0x0 == requestData[3] && 0x2 == requestData[4]);


        }

    }

    
}
