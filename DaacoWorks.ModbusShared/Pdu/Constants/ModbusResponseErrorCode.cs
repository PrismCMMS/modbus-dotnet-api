


namespace Com.DaacoWorks.Modbus.Pdu.Constants
{
    /// <summary>
    /// ModbusResponseErrorCode has the error codes sent by Modbus server as error responses.
    /// </summary>
    public enum ModbusResponseErrorCode
    {
        /// <summary>
        /// Illegal function code
        /// </summary>
        ILLEGAL_FUNCTION_CODE = 0x01,
        /// <summary>
        /// Illegal data address
        /// </summary>
        ILLEGAL_DATA_ADDRESS = 0x02,
        /// <summary>
        /// Illegal data value
        /// </summary>
        ILLEGAL_DATA_VALUE = 0x03,
        /// <summary>
        /// Slave device failure
        /// </summary>
        SLAVE_DEVICE_FAILURE = 0x04,
        /// <summary>
        /// Acknowledge
        /// </summary>
        ACKNOWLEDGE = 0x05,
        /// <summary>
        /// Slave device is busy
        /// </summary>
        SLAVE_DEVICE_BUSY = 0x06,
        /// <summary>
        /// Memory parity error
        /// </summary>
        MEMORY_PARITY_ERROR = 0x08,
        /// <summary>
        /// Gateway path not available
        /// </summary>
        GATEWAY_PATH_UNAVAILABLE = 0x0A,
        /// <summary>
        /// Gateway target device failed to respond
        /// </summary>
        GATEWAY_TARGET_DEVICE_FAILED_TO_RESPOND = 0x0B

    }
}
