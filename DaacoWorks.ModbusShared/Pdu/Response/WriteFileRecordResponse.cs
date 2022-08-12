using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// WriteFileRecordResponse is a success response for the request <see cref="WriteFileRecordRequest"/>
    /// </summary>
    public class WriteFileRecordResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new modbus file record success response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public WriteFileRecordResponse(ModbusRequest requestPDU):base(requestPDU)
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
                if (requestPDU.GetFunctionCode() == FunctionCodes.WRITE_FILE_RECORD)
                    return GetFileWriteRecord(buffer);                
                else
                    return null;
            }
        }

        //TODO validate the READ_FILE_RECORD and WRITE_FILE_RECORD against spec and see all the restrictions are addressed.

        private FileRecordData[] GetFileWriteRecord(MemoryStream buffer)
        {
            List<FileRecordData> records = new List<FileRecordData>();
            byte[] dst = new byte[1];
            buffer.Read(dst);
            byte respDataLength = dst[0];
            while (respDataLength > 0)
            {
                buffer.ReadByte();
                respDataLength--; //ignore ref.type
                FileRecordData record = new FileRecordData(buffer.ReadShort(), buffer.ReadShort(), buffer.ReadShort());
                respDataLength = (byte)(respDataLength - 6);
                byte[] recData = new byte[record.RecordLength * 2];
                respDataLength = (byte)(respDataLength - recData.Length);
                buffer.Read(recData);
                record.RecordData = recData;
                records.Add(record);
            }
            return records.ToArray();
        }


    }
}