using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// WriteFileRecord class is used to perform a file record write.
    /// </summary>
    public class WriteFileRecordRequest : ModbusRequest {

        /// <summary>
        /// Instantiates a new write file record.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="fileRecords">file records</param>
        public WriteFileRecordRequest(byte slaveId, FileRecordData[] fileRecords) : base(slaveId, 0, 0, true)
        {
            FileRecords = fileRecords;
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.WRITE_FILE_RECORD;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            //function code (1), byte count (1) + fileRecordSize
            return 2 + (GetFileRecordSize());
        }

        /// <summary>
        /// Gets file record size
        /// </summary>
        /// <returns></returns>
        private int GetFileRecordSize()
        {
            int length = 0;
            foreach (FileRecordData record in FileRecords)
            {
                length += (record.RecordData.Length + 7);
                //(ref.type (1), fileNumber (2), rec.Number (2), rec.Length (2), data length)
            }
            return length;
        }

        /// <summary>
        /// Gets data in bytes
        /// </summary>
        /// <returns></returns>
        public override byte[] GetDataInBytes()
        {
            using (var buffer = new MemoryStream(GetLength()))
            {
                using (var binaryWriter = new BinaryWriter(buffer))
                {
                    binaryWriter.Write(GetFunctionCode());
                    binaryWriter.Write((byte)GetFileRecordSize());
                    foreach (FileRecordData record in FileRecords)
                    {
                        binaryWriter.Write((byte)0x06);
                        binaryWriter.Write(GetHexByteArray(record.FileNumber, 2));
                        binaryWriter.Write(GetHexByteArray(record.RecordNumber, 2));
                        binaryWriter.Write(GetHexByteArray(record.RecordLength, 2));
                        var data = record.RecordData;
                        binaryWriter.Write(record.RecordData);
                    }
                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Validate request
        /// </summary>
        public override void Validate()
        {
            if (GetFileRecordSize() > 252)
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_FILE_RECORD_SIZE);
            }
            else if (!IsRecordNumberValid())
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_RECORD_NUMBER);
            }
            else if (!IsFileRecordDataValid())
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.MISSING_RECORD_DATA);
            }
        }

        
        private bool IsFileRecordDataValid()
        {
            foreach (FileRecordData fileRec in FileRecords)
            {
                if (fileRec.RecordData == null || fileRec.RecordData.Length == 0) return false;
            }
            return true;
        }

        
        private bool IsRecordNumberValid()
        {
            foreach (FileRecordData record in FileRecords)
            {
                if (record.RecordNumber > 0x270F)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets/Sets file records
        /// </summary>
        public FileRecordData[] FileRecords
        {
            get; set;
        }

    }
}