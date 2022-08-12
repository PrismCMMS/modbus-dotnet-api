
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class WriteFileRecordResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadFileRecordResponse")]
        [Timeout(1000)]
        public void WriteFileRecordResponse_GetFileRecords()
        {
            var fileRecordData = new List<FileRecordData>();
            var recordData = new byte[] { 0x06, 0xAF, 0x04, 0xBE, 0x10, 0x0D };
            fileRecordData.Add(new FileRecordData(4, 7, recordData));

            var request = new WriteFileRecordRequest(1, fileRecordData.ToArray());
            var response = new WriteFileRecordResponse(request);

            var dataFromDevice = new List<byte>();
            dataFromDevice.Add(0x0D); //response data length            
            dataFromDevice.Add(0x06); //ref type
            dataFromDevice.Add(0x00); //file number
            dataFromDevice.Add(0x04); //file number
            dataFromDevice.Add(0x00); //record number
            dataFromDevice.Add(0x07); //record number
            dataFromDevice.Add(0x00); //record length
            dataFromDevice.Add(0x03); //record length
            dataFromDevice.AddRange(recordData); //file 1 data

            var pdu = new ModbusResponse(0x14, dataFromDevice.ToArray());
            response.SetResponsePDU(pdu);

            var actualFileRecordData = response.GetFileRecords();

            Assert.AreEqual(fileRecordData[0].FileNumber, actualFileRecordData[0].FileNumber);
            Assert.AreEqual(fileRecordData[0].RecordNumber, actualFileRecordData[0].RecordNumber);
            Assert.AreEqual(fileRecordData[0].RecordLength, actualFileRecordData[0].RecordLength);

            Assert.AreEqual(dataFromDevice[8], actualFileRecordData[0].RecordData[0]);
            Assert.AreEqual(dataFromDevice[9], actualFileRecordData[0].RecordData[1]);
            Assert.AreEqual(dataFromDevice[10], actualFileRecordData[0].RecordData[2]);
            Assert.AreEqual(dataFromDevice[11], actualFileRecordData[0].RecordData[3]);
            Assert.AreEqual(dataFromDevice[12], actualFileRecordData[0].RecordData[4]);
            Assert.AreEqual(dataFromDevice[13], actualFileRecordData[0].RecordData[5]);


        }

    }
}
