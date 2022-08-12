using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// ReadFileRecord class is used to perform a file record read. 
    /// </summary>
    public class ReadFileRecordRequest : ModbusRequest {

        private FileRecordData[] fileRecords;

        /// <summary>
        /// Instantiates a new read file record.
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="fileRecords"></param>
        public ReadFileRecordRequest(byte slaveId, FileRecordData[] fileRecords) : base(slaveId, 0, 0, true)
        {

            this.SetFileRecords(fileRecords);
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.READ_FILE_RECORD;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            //function code (1), byte count (1), (ref.type (1), fileNumber (2), rec.Number (2), rec.Length (2))
            return 2 + (GetFileRecords().Length * 7);
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
                    binaryWriter.Write(GetHexByteArray(GetFileRecords().Length * 7, 1));
                    foreach (FileRecordData record in GetFileRecords())
                    {
                        binaryWriter.Write((byte)0x06);
                        binaryWriter.Write(GetHexByteArray(record.FileNumber, 2));
                        binaryWriter.Write(GetHexByteArray(record.RecordNumber, 2));
                        binaryWriter.Write(GetHexByteArray(record.RecordLength, 2));
                    }
                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets file records
        /// </summary>
        /// <returns>file record</returns>
        public FileRecordData[] GetFileRecords()
        {
            return fileRecords;
        }

        /// <summary>
        /// Sets file records
        /// </summary>
        /// <param name="fileRecords">file records</param>
        public void SetFileRecords(FileRecordData[] fileRecords)
        {
            this.fileRecords = fileRecords;
        }

        /// <summary>
        /// Validate request
        /// </summary>
        public override void Validate()
        {
            if (fileRecords == null || fileRecords.Length == 0)
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.EMPTY_FILE_RECORD);
            }
            else if (!IsRecordNumberValid())
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_RECORD_NUMBER);
            }
            
        }

        private bool IsRecordNumberValid()
        {
            foreach (FileRecordData record in GetFileRecords())
            {
                if (record.RecordNumber > 0x270F)
                {
                    return false;
                }
            }
            return true;
        }


    }
}