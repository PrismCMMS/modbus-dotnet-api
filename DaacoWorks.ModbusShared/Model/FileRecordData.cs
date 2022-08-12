namespace Com.DaacoWorks.Modbus.Model
{
    /// <summary>
    /// FileRecordData is a model class to represent the requests and responses for the function codes
    /// Read File Record(0x14) and Write File Record(0x15)
    /// </summary>
    public class FileRecordData
    {
        /// <summary>
        /// Instantiates a new file record data.
        /// </summary>
        /// <param name="fileNumber">the file number</param>
        /// <param name="recordNumber">record number</param>
        /// <param name="recordLength">record length</param>
        public FileRecordData(ushort fileNumber, ushort recordNumber, ushort recordLength)
        {
            this.FileNumber = fileNumber;
            this.RecordNumber = recordNumber;
            this.RecordLength = recordLength;
        }

        /// <summary>
        /// Instantiates a new file record data.
        /// </summary>
        /// <param name="fileNumber">the file number</param>
        /// <param name="recordNumber">record number</param>
        /// <param name="recordData">record data</param>
        public FileRecordData(ushort fileNumber, ushort recordNumber, byte[] recordData)
        {
            this.FileNumber = fileNumber;
            this.RecordNumber = recordNumber;
            this.RecordLength = (ushort)(recordData.Length / 2);
            this.RecordData = recordData;
        }

        /// <summary>
        /// Gets/sets File number
        /// </summary>
        public ushort FileNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets record number
        /// </summary>
        public ushort RecordNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets record length
        /// </summary>
        public ushort RecordLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets record data
        /// </summary>
        public byte[] RecordData
        {
            get;
            set;
        }
    }
}