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
    public class ReadCoilsResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilStatusResponse_2Coils()
        {
            var expectedStatus = new List<CoilState>();
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF); 

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD, (byte)0x01 });

            ReadCoilStatus(2, expectedStatus, pdu);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilStatusResponse_8Inputs()
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

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD });

            ReadCoilStatus(8, expectedStatus, pdu);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilStatusResponse_10Coils()
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

            ReadCoilStatus(10, expectedStatus, pdu);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilStatusResponse_16Coils()
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

            ReadCoilStatus(16, expectedStatus, pdu);
        }

        [TestMethod]
        [TestCategory("Modbus\\ReadCoils")]
        [Timeout(1000)]
        public void ReadCoilStatusResponse_17Coils()
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

            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);
            expectedStatus.Add(CoilState.ON);
            expectedStatus.Add(CoilState.OFF);

            ModbusResponse pdu = new ModbusResponse((byte)1, new byte[] { (byte)0xCD, (byte)0x01, (byte)0x58 });

            ReadCoilStatus(17, expectedStatus, pdu);

        }

        public void ReadCoilStatus(ushort coilsCount, List<CoilState> expectedStatus, ModbusResponse pdu)
        {            

            var coils = new ReadCoilsRequest(1, 9, coilsCount, true);
            coils.RequestIdentifier = new ModbusRequestIdentifier(null, 0, 1);
            ReadCoilsResponse response = new ReadCoilsResponse(coils);
            
            response.SetResponsePDU(pdu);
            var coilStatus = response.GetCoilStatus();

            Assert.AreEqual(1, ((ModbusRequestIdentifier)response.Request.RequestIdentifier).GetRequestId());
            for (int i = 0; i < coilStatus.Length; i++)
            {
                Assert.AreEqual(expectedStatus[i], coilStatus[i]);
            }

        }

    }
}