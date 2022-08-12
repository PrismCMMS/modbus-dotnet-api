using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;
using Com.DaacoWorks.Modbus.Pdu;

namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class WriteSingleCoilRequestTest : RequestTestBase
    {


        [TestMethod]
        [TestCategory("Modbus\\WriteSingleCoil")]
        [Timeout(1000)]
        public void WriteSingleCoilRequest_OFF()
        {
            WriteSingleCoil(10, CoilState.OFF);
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleCoil")]
        [Timeout(1000)]
        public void WriteSingleCoilRequest_ON()
        {
            WriteSingleCoil(10, CoilState.ON);
        }

        [TestMethod]
        [TestCategory("Modbus\\WriteSingleCoil")]
        [Timeout(1000)]
        [Ignore]
        public void WriteSingleCoilRequest_InvalidInput()
        {
            try
            {
                client.Submit(new WriteSingleCoilRequest(0xFF, ushort.MaxValue, CoilState.ON, true));
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        private void WriteSingleCoil(ushort address, CoilState state)
        {
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(new WriteSingleCoilRequest(0xFF, address, state, true)).Get();
            Assert.IsTrue(response is ModbusSuccessResponse);

            ModbusSuccessResponse success = (ModbusSuccessResponse)client.Submit(new ReadCoilsRequest(0xFF, address, 1, true)).Get();

            Assert.IsTrue(GetInt(success.GetData()) == ((state == CoilState.ON) ? 1 : 0));


        }

    }
}