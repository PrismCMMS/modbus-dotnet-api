
namespace Com.DaacoWorks.Modbus.Pdu.Constants
{
    /// <summary>
    /// ModbusErrorCodes class has Modbus specific error code and error messages.
    /// </summary>
    public class ModbusErrorCodes
    {
        /// <summary>
        /// Represent input validation error
        /// </summary>
        public const int INVALID_INPUT = 0;
        /// <summary>
        /// Length of data given for conversion to flat should be in multiples of 4 to convert to float properly
        /// </summary>
        public const string INVALID_FLOAT_VALUE_MSG = "unable to convert to float value. insufficient byte array size...";
        /// <summary>
        /// Length of data given for conversion to flat should be in multiples of 4 to convert to float properly
        /// </summary>
        public const string INVALID_INT_VALUE_MSG = "unable to convert to integer value. insufficient byte array size...";
        /// <summary>
        /// Valid object id ranges between 0 and 255
        /// </summary>
        public const string INVALID_OBJECTID = "objectId exceeds the allowed limit 255";
        /// <summary>
        /// Output length should be exactly equal to (write quantity x 2)
        /// </summary>
        public const string INVALID_OUTPUT_LENGTH = "size of outputValue is not equal to writeQuantity";
        /// <summary>
        /// A file record size cannot be more than 252 bytes
        /// </summary>
        public const string INVALID_FILE_RECORD_SIZE = "file record size exceeds the allowed limit 252 bytes";
        /// <summary>
        /// Record number range is between 0 and 9999
        /// </summary>
        public const string INVALID_RECORD_NUMBER = "record number value is greater then 9999";
        /// <summary>
        /// Record data cannot be null or empty
        /// </summary>
        public const string MISSING_RECORD_DATA = "record data is missing in one of the file records";
        /// <summary>
        /// No file record is set
        /// </summary>
        public const string EMPTY_FILE_RECORD = "File Record is empty";


    }

}