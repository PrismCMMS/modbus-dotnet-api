using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{

    [TestClass]
    public class ReadFIFOQueueRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadFIFOQueue")]
        [Timeout(1000)]
        public void FIFOQueueRequest_Success()
        {
            requestPDU = new ReadFIFOQueueRequest(0xFF, 1, true);

            var success = (ReadFIFOQueueResponse)client.Submit(requestPDU).Get();

            Assert.IsTrue(success.GetData().Length == 62);
        }

    }
}