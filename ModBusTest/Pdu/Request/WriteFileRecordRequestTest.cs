using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Pdu.Request;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.DaacoWorks.Protocol.Model;

namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class WriteFileRecordRequestTest : RequestTestBase
    {

        [TestMethod]
        [TestCategory("Modbus\\WriteFileRecord")]
        [Timeout(1000)]
        public void WriteFileRecordRequest()
        {

            var fileRecords = GetFileRecords(2);

            requestPDU = new WriteFileRecordRequest(0xFF, fileRecords);
            Com.DaacoWorks.Protocol.Model.Response response = client.Submit(requestPDU).Get();
            Assert.IsTrue(response is WriteFileRecordResponse);
            var success = (WriteFileRecordResponse)response;
            Assert.IsTrue(success.GetFileRecords().Length == fileRecords.Length);
        }

        private FileRecordData[] GetFileRecords(ushort size)
        {
            List<FileRecordData> fileRecords = new List<FileRecordData>();
            for (ushort i = 0; i < size; i++)
            {
                for (ushort j = 0; j < size * size; j++)
                {
                    FileRecordData fileRecord = new FileRecordData(i, j, new byte[] { 1, 1 });
                    fileRecords.Add(fileRecord);
                }
            }
            return fileRecords.ToArray();
        }



    }
}