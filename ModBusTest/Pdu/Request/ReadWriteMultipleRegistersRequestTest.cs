using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class ReadWriteMultipleRegistersRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void ReadWriteMultipleRegistersRequest_Success()
        {
            ReadWriteMultipleRegisters(9, 1, new byte[] { (byte)0x00, (byte)0xFF });
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void ReadWriteMultipleRegistersRequest_Success2()
        {
            ReadWriteMultipleRegisters(9, 2, new byte[] { (byte)0xFF, (byte)0xFF, (byte)0xBB, (byte)0xBB });
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisterRequest_InvalidInput_ReadAddress()
        {
            try
            {
                requestPDU = new ReadWriteMultipleRegistersRequest(0xFF, ushort.MaxValue, 1, 65535, 123, true);
                ((ReadWriteMultipleRegistersRequest)requestPDU).WriteValues = new byte[] { (byte)0x00, (byte)0xFF };
                client.Submit(requestPDU);

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);

            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisterRequest_InvalidInput_ReadAddress_Quantity()
        {
            try
            {
                requestPDU = new ReadWriteMultipleRegistersRequest(0xFF, 65535, 123, ushort.MaxValue, 123, true);
                ((ReadWriteMultipleRegistersRequest)requestPDU).WriteValues=new byte[] { (byte)0x00, (byte)0xFF };
                client.Submit(requestPDU);

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        [Ignore]
        public void WriteMultipleRegisterRequest_InvalidInput_Quantity()
        {
            try
            {
                client.Submit(new ReadWriteMultipleRegistersRequest(0xFF, 1, 0x007E, 1, 1, true));

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisterRequest_InvalidInput_WriteAddress()
        {
            try
            {
                requestPDU = new ReadWriteMultipleRegistersRequest(0xFF, 1, 1, ushort.MaxValue, 123, true);
                ((ReadWriteMultipleRegistersRequest)requestPDU).WriteValues = new byte[] { (byte)0x00, (byte)0xFF };
                client.Submit(requestPDU);

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisterRequest_InvalidInput_WriteAddress_Quantity()
        {
            try
            {
                requestPDU = new ReadWriteMultipleRegistersRequest(0xFF, 1, 1, 65535, 123, true);
                ((ReadWriteMultipleRegistersRequest)requestPDU).WriteValues = new byte[] { (byte)0x00, (byte)0xFF };
                client.Submit(requestPDU);

                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadWriteMultipleRegisters")]
        [Timeout(1000)]
        public void WriteMultipleRegisterRequest_InvalidInput_Output()
        {
            try
            {
                requestPDU = new ReadWriteMultipleRegistersRequest(0xFF, 1, 1, 1, 1, true);
                ((ReadWriteMultipleRegistersRequest)requestPDU).WriteValues = new byte[] { (byte)0xFF };
                client.Submit(requestPDU);
                Assert.IsTrue(false);
            }
            catch (ModbusException e)
            {
                Assert.IsTrue(e.ErrorCode == ModbusErrorCodes.INVALID_INPUT);
            }
        }

        private void ReadWriteMultipleRegisters(ushort address, byte quantity, byte[] value)
        {

            requestPDU = new ReadWriteMultipleRegistersRequest(0xFF, address, quantity, address, quantity, true);
            ((ReadWriteMultipleRegistersRequest)requestPDU).WriteValues = value;

            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(requestPDU).Get();

            Assert.IsTrue(response is ModbusSuccessResponse);            

        }

    }
}