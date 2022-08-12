
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class ReadDiscreteInputsResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputStatus")]
        [Timeout(1000)]
        public void ReadDiscreteInputResponse_2Coils()
        {
            var expectedStatus = new List<CoilState>();
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD, (byte)0x01 });

            ReadDiscreteInputStatus(2, expectedStatus, pdu);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputStatus")]
        [Timeout(1000)]
        public void ReadDiscreteInputResponse_8Inputs()
        {
            var expectedStatus = new List<CoilState>();
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
           

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD});

            ReadDiscreteInputStatus(8, expectedStatus, pdu);

        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputStatus")]
        [Timeout(1000)]
        public void ReadDiscreteInputResponse_10Inputs()
        {
            var expectedStatus = new List<CoilState>();
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            
            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD, (byte)0x01 });

            ReadDiscreteInputStatus(10, expectedStatus, pdu);

        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputStatus")]
        [Timeout(1000)]
        public void ReadDiscreteInputResponse_16Coils()
        {
            var expectedStatus = new List<CoilState>();
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);

            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD, (byte)0x01 });

            ReadDiscreteInputStatus(16, expectedStatus, pdu);

        }

        [TestMethod]
        [TestCategory("Modbus\\ReadDiscreteInputStatus")]
        [Timeout(1000)]
        public void ReadDiscreteInputResponse_17Coils()
        {
            var expectedStatus = new List<CoilState>();
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);

            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);

            expectedStatus.Add(CoilState.ON);
            

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD, (byte)0x01, (byte)0x03 });

            ReadDiscreteInputStatus(17, expectedStatus, pdu);

        }

       

        private void ReadDiscreteInputStatus(ushort statusCount, List<CoilState> expectedStatus, ModbusResponse pdu)
        {
            
            var inputs = new ReadDiscreteInputsRequest(1, 9, statusCount, true);
            inputs.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);
            ReadDiscreteInputsResponse response = new ReadDiscreteInputsResponse(inputs);
            
            response.SetResponsePDU(pdu);
            var inputStatus = response.GetDiscreteInputStatus();

            Assert.AreEqual(1, ((ModbusRequestIdentifier)response.Request.RequestIdentifier).GetRequestId());
            for (int i = 0; i < inputStatus.Length; i++)
            {
                Assert.AreEqual(expectedStatus[i], inputStatus[i]);
            }

        }

    }
}