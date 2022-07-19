using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Extensions;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadFileRecordResponse is a success response for the request <see cref="ReadFileRecordRequest"/> 
    /// </summary>
    public class ReadFileRecordResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new modbus file record success response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public ReadFileRecordResponse(ModbusRequest requestPDU):base(requestPDU)
        {
        }

        /// <summary>
        /// Gets the file records.
        /// </summary>
        /// <returns></returns>
        public FileRecordData[] GetFileRecords()
        {
            var data = GetData();
            using (var buffer = new MemoryStream(data.Length))
            {
                buffer.Write(data, (int)buffer.Position, data.Length);
                buffer.Flip();
                if (requestPDU.GetFunctionCode() == FunctionCodes.READ_FILE_RECORD)
                    return GetFileReadRecords(buffer);                
                else
                    return null;
            }
        }

        //TODO validate the READ_FILE_RECORD and WRITE_FILE_RECORD against spec and see all the restrictions are addressed.

        private FileRecordData[] GetFileReadRecords(MemoryStream buffer)
        {

            byte[] dst = new byte[1];
            buffer.Read(dst);
            byte respDataLength = dst[0];
            byte pendingRespDataLength = respDataLength;
            FileRecordData[] fileRecords = ((ReadFileRecordRequest)requestPDU).GetFileRecords();
            int index = 0;

            while (pendingRespDataLength > 0)
            {
                buffer.Read(dst); pendingRespDataLength--;//file resp length
                byte fileRespLength = dst[0];
                buffer.ReadByte();
                pendingRespDataLength--;//ignore reference type
                byte[] recordData = new byte[fileRespLength - 1];
                buffer.Read(recordData);
                pendingRespDataLength = (byte)(pendingRespDataLength - recordData.Length);
                FileRecordData fileRecord = fileRecords[index++];
                fileRecord.RecordData = recordData;
            }
            return fileRecords;
        }

    }
}