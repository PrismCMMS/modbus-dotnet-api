using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class WriteMultipleRegistersRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisterRequest_Success_FFFF()
        {
            WriteMultipleRegister(9, 1, new ushort[] { 0xFFFF });
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisteRequestr_Success_FF()
        {
            WriteMultipleRegister(9, 1, new ushort[] { 0xFF });
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisteRequestr_Success_MultipleValues()
        {
            WriteMultipleRegister(9, 2, new ushort[] { 0x0000, 0x00FF });
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        [Ignore]
        public void WriteMultipleRegisterRequest_InvalidInput_address()
        {
            try
            {
                client.Submit(new WriteMultipleRegistersRequest(0xFF, ushort.MaxValue, 1, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteMultipleRegisters")]
        [Timeout(1000)]
        [Ignore]
        public void WriteMultipleRegisterRequest_InvalidInput_quantity()
        {
            try
            {
                client.Submit(new WriteMultipleRegistersRequest(0xFF, ushort.MaxValue, 123, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        private void WriteMultipleRegister(ushort address, ushort quantity, ushort[] value)
        {
            requestPDU = new WriteMultipleRegistersRequest(0xFF, address, quantity, true);
            ((WriteMultipleRegistersRequest)requestPDU).WriteValues = value;
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(requestPDU).Get();
            Assert.IsTrue(response is ModbusSuccessResponse);

            ModbusSuccessResponse success = (ModbusSuccessResponse)client.Submit(new ReadHoldingRegistersRequest(0xFF, address, quantity, true)).Get();

            var input = value;
            var output = success.GetData();
            for (int i = 0, j = 0; i < input.Length; i++, j += 2)
            {
                Assert.AreEqual(input[i], ModbusUtil.ToInt16(output, j));
            }

        }

    }
}