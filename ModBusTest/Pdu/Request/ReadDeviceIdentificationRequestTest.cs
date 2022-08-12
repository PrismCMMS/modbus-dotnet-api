using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace ModBusTest.Pdu.Request
{

    [TestClass]
    public class ReadDeviceIdentificationRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadDeviceIdentification")]
        [Timeout(1000)]
        public void ReadDeviceIdentificationRequest_Success()
        {
            requestPDU = new ReadDeviceIdentificationRequest(0xFF, DeviceID.BASIC_DEVICE_IDENTIFICATION, 0);

            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(requestPDU).Get();

            Assert.IsTrue(response is ReadDeviceIdentificationResponse);
            ReadDeviceIdentificationResponse success = (ReadDeviceIdentificationResponse)response;
            success.GetDeviceInformation();

        }

    }
}