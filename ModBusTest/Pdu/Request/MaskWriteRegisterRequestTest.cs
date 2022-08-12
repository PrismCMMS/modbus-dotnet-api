using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.DaacoWorks.Modbus.Pdu;

namespace ModBusTest.Pdu.Request
{

    [TestClass]    
    public class MaskWriteRegisterRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\MaskWriteRegister")]
        [Timeout(1000)]
        public void MaskWriteRegisterRequest_SuccessRequest()
        {
            
            WriteSingleRegister(10, 624);
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new ReadHoldingRegistersRequest(0xFF, 10, 1, true)).Get();           
            requestPDU = new MaskWriteRegisterRequest(0xFF, 10, 242, 37, true);
            response = client.Submit(requestPDU).Get();
            Assert.IsTrue(response is ModbusSuccessResponse);            

            response = client.Submit(new ReadHoldingRegistersRequest(0xFF, 10, 1, true)).Get();
            
            Assert.IsTrue(response is ModbusSuccessResponse);
            //var actualData = ((ModbusSuccessResponse)response).GetData()[1];
            //Assert.AreEqual(117, actualData);

        }

        [TestMethod]
        [TestCategory("Modbus\\MaskWriteRegister")]
        [Timeout(1000)]
        public void MaskWriteRegisterRequest_AndMask_InvalidInput()
        {
           
            requestPDU = new MaskWriteRegisterRequest(0xFF, 10, ushort.MaxValue, 37, true);
            try
            {
                client.Submit(requestPDU).Get();
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }          

        }

        [TestMethod]
        [TestCategory("Modbus\\MaskWriteRegister")]
        [Timeout(1000)]
        public void MaskWriteRegisterRequest_OrMask_InvalidInput()
        {
            requestPDU = new MaskWriteRegisterRequest(0xFF, 10, 242, ushort.MaxValue, true);
            try
            {
                client.Submit(requestPDU).Get();
                
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        private void WriteSingleRegister(ushort address, ushort value)
        {

            client.Submit(new WriteSingleRegisterRequest(0xFF, address, value, true)).Get();

        }

    }
}