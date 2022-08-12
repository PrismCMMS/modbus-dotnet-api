
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
    public class ReadFileRecordResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadFileRecordResponse")]
        [Timeout(1000)]
        public void ReadFileRecordResponse_GetFileRecords()
        {
            var fileRecordData = new List<FileRecordData>();
            fileRecordData.Add(new FileRecordData(4, 1, 2));
            fileRecordData.Add(new FileRecordData(3, 9, 2));

            var request = new ReadFileRecordRequest(1, fileRecordData.ToArray());
            var response = new ReadFileRecordResponse(request);

            var dataFromDevice = new List<byte>();
            dataFromDevice.Add(0x0C); //response data length
            dataFromDevice.Add(0x05); //file 1 response length
            dataFromDevice.Add(0x06); //ref type
            dataFromDevice.Add(0x0D); //file 1 data
            dataFromDevice.Add(0xFE); //file 1 data
            dataFromDevice.Add(0x00); //file 1 data
            dataFromDevice.Add(0x20); //file 1 data

            dataFromDevice.Add(0x05); //file 2 response length
            dataFromDevice.Add(0x06); //ref type
            dataFromDevice.Add(0x33); //file 2 data
            dataFromDevice.Add(0xCD); //file 2 data
            dataFromDevice.Add(0x00); //file 2 data
            dataFromDevice.Add(0x40); //file 2 data

            var pdu = new ModbusResponse(0x14, dataFromDevice.ToArray());
            response.SetResponsePDU(pdu);

            var actualFileRecordData = response.GetFileRecords();

            for (var index = 0; index < fileRecordData.Count; index++)
            {
                Assert.AreEqual(fileRecordData[index].FileNumber, actualFileRecordData[index].FileNumber);
                Assert.AreEqual(fileRecordData[index].RecordNumber, actualFileRecordData[index].RecordNumber);
                Assert.AreEqual(fileRecordData[index].RecordLength, actualFileRecordData[index].RecordLength);
            }

            Assert.AreEqual(dataFromDevice[3], actualFileRecordData[0].RecordData[0]);
            Assert.AreEqual(dataFromDevice[4], actualFileRecordData[0].RecordData[1]);
            Assert.AreEqual(dataFromDevice[5], actualFileRecordData[0].RecordData[2]);
            Assert.AreEqual(dataFromDevice[6], actualFileRecordData[0].RecordData[3]);

            Assert.AreEqual(dataFromDevice[9], actualFileRecordData[1].RecordData[0]);
            Assert.AreEqual(dataFromDevice[10], actualFileRecordData[1].RecordData[1]);
            Assert.AreEqual(dataFromDevice[11], actualFileRecordData[1].RecordData[2]);
            Assert.AreEqual(dataFromDevice[12], actualFileRecordData[1].RecordData[3]);

        }

    }
}
