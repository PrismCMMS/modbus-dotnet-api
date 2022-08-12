using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Model;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.DaacoWorks.Protocol.Model;

namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class ReadFileRecordRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadFileRecord")]
        [Timeout(1000)]
        public void ReadFileRecordRequest_Success()
        {
            var fileRecords = GetFileRecords(2);
            requestPDU = new ReadFileRecordRequest(0xFF, fileRecords);

            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(requestPDU).Get();

            Assert.IsTrue(response is ReadFileRecordResponse);
            ReadFileRecordResponse success = (ReadFileRecordResponse)response;
            foreach (FileRecordData fileRec in success.GetFileRecords())
            {
                Assert.IsTrue(fileRec.RecordData != null && fileRec.RecordData.Length == 4);
            }

        }

        private FileRecordData[] GetFileRecords(ushort size)
        {
            List<FileRecordData> fileRecords = new List<FileRecordData>();
            for (ushort i = 0; i < size; i++)
            {
                for (ushort j = 0; j < size * size; j++)
                {
                    FileRecordData fileRecord = new FileRecordData(i, j, size);
                    fileRecords.Add(fileRecord);
                }
            }
            return fileRecords.ToArray();
        }

    }
}