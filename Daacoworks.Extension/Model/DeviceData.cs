using System;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Request;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace DaacoWorks.Extension.Model
{
    public class DeviceData
    {
        #region Request Data
        /// <summary>
        /// Unique request Id to track the response
        /// </summary>
        /// <remarks>
        /// Request Id will be reused in multiple responses in case of scheduled request
        /// </remarks>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Date and Time at which request was generated and submitted to device
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// Id of the peer that responded for the request sent
        /// </summary>
        public byte RequestSlaveId { get; set; }

        /// <summary>
        /// Requested Modbus function code
        /// </summary>
        public byte RequestFunctionCode { get; set; }

        /// <summary>
        /// Start address from where data is read
        /// </summary>
        public ushort RequestReadAddress { get; set; }

        /// <summary>
        /// Quantity of data to be read
        /// </summary>
        public ushort RequestReadQuantity { get; set; }

        /// <summary>
        /// Modbus device Identification type
        /// </summary>
        /// <remarks>
        ///  Used for <see cref="ReadDeviceIdentificationRequest"/> request (i.e., Modbus Function - 0x2B - Encapsulated Interface Transport)
        /// </remarks>
        public byte RequestDeviceIdentificationType { get; set; }

        /// <summary>
        /// Gets Object Id
        /// </summary>
        /// <remarks>
        ///  Used for <see cref="ReadDeviceIdentification"/> request (i.e., Modbus Function - 0x2B - Encapsulated Interface Transport)
        /// </remarks>
        public byte RequestObjectId { get; set; }

        /// <summary>
        /// Maintains the file read request
        /// </summary>
        /// <remarks>
        /// Used for <see cref="ReadDiscreteInputRequest"/> request (i.e., Modbus Function - 0x14 - Read File Record) and
        /// Used for <see cref="WriteFileRecordRequest"/> request (i.e., Modbus Function - 0x15 - Write File Record)
        /// </remarks>
        public FileRecordData[] RequestFileRecords { get; set; }

        /// <summary>
        /// Start address from where data is to be written
        /// </summary>
        /// <remarks>
        /// Used for <see cref="ReadWriteMultipleRegistersRequest"/> request (i.e., Modbus Function - 0x17 - Read Write Multiple Registers)
        /// </remarks>
        public ushort RequestWriteAddress { get; set; }

        /// <summary>
        /// Quantity of data to be written
        /// </summary>
        /// <remarks>
        /// Used for <see cref="ReadWriteMultipleRegisters"/> request (i.e., Modbus Function - 0x17 - Read Write Multiple Registers)
        /// </remarks>
        public ushort RequestWriteQuantity { get; set; }

        /// <summary>
        /// AND-Mask value to be written
        /// </summary
        /// <remarks>
        /// Used for <see cref="MaskWriteRegisterRequest"/> request (i.e., Modbus Function - 0x16 - Mask Write Register)
        /// </remarks>
        public ushort RequestAndMask { get; set; }

        /// <summary>
        /// OR-Mask value to be written
        /// </summary
        /// <remarks>
        /// Used for <see cref="MaskWriteRegisterRequest"/> request (i.e., Modbus Function - 0x16 - Mask Write Register)
        /// </remarks>
        public ushort RequestOrMask { get; set; }

        /// <summary>
        /// On/Off coil state where 1 represents On and 0 represents Off
        /// </summary>
        /// <remarks>
        /// Used for <see cref="WriteSingleCoilRequest"/> request (i.e., Modbus Function - 0x05 - Write Single Coil)
        /// </remarks>
        public bool RequestCoilState { get; set; }        

        /// <summary>
        /// 2 byte register data to be written is maintained as a int value
        /// </summary>
        /// <remarks>
        ///  Used for <see cref="WriteSingleRegisterRequest"/> request (i.e., Modbus Function - 0x06 - Write Single Register)
        /// </remarks>
        public ushort RequestWriteValue { get; set; }

        /// <summary>
        /// Data to be written to multiple registers.
        /// Each register data need to be represented with 2 bytes in array.
        /// Hence to write to x registers, array size will be (x * 2)
        /// </summary>
        /// <remarks>
        ///  Used for <see cref="WriteMultipleRegistersRequest"/> request (i.e., Modbus Function - 0x10 - Write Multiple Registers)
        /// </remarks>
        public ushort[] RequestWriteValues { get; set; }

        /// <summary>
        /// Represents whether the request made was scheduled or not.
        /// True represents scheduled request and False represents Sync or Async requests
        /// </summary>
        /// <remarks>
        /// When this flag is True, the request id will be same for all responses as the 
        /// same request is submitted to device in the scheduled interval
        /// </remarks>
        public bool IsScheduledRequest { get; set; }
        #endregion

        #region Response Data
        /// <summary>
        /// Date and Time at which response was received from device
        /// </summary>
        public DateTime ResponseTime { get; set; }

        /// <summary>
        /// Actual response provided by the device
        /// </summary>
        public byte[] ResponseRawData { get; set; }

        /// <summary>
        /// Single coil state is maintained. 
        /// </summary>
        /// <remarks>
        /// Used for <see cref="WriteSingleRegisterRequest"/> request (i.e., Modbus Function - 0x06 - Read Coils)
        /// </remarks>
        public ushort ResponseRegisterValue { get; set; }

        /// <summary>
        /// Single coil state is maintained. 
        /// </summary>
        /// <remarks>
        /// Used for <see cref="WriteSingleCoilRequest"/> request (i.e., Modbus Function - 0x01 - Read Coils)
        /// </remarks>
        public CoilState ResponseCoilState { get; set; }

        /// <summary>
        /// Multiple coils state are maintained as array.
        /// </summary>
        /// <remarks>
        /// Used for <see cref="ReadCoilsRequest"/> request (i.e., Modbus Function - 0x01 - Read Coils)
        /// </remarks>
        public CoilState[] ResponseCoilStates { get; set; }

        /// <summary>
        /// Multiple Discrete Input status are maintained as array.
        /// </summary>
        /// <remarks>
        /// Used for <see cref="ReadDiscreteInputsRequest"/> request (i.e., Modbus Function - 0x02 - Read Discrete Input Status)
        /// </remarks>
        public CoilState[] ResponseDicreteInputStatuses { get; set; }        

        /// <summary>
        /// Maintains the file read request
        /// </summary>
        /// <remarks>
        /// Used for <see cref="ReadDiscreteInputRequest"/> request (i.e., Modbus Function - 0x14 - Read File Record) and
        /// Used for <see cref="WriteFileRecordRequest"/> request (i.e., Modbus Function - 0x15 - Write File Record)
        /// </remarks>
        public FileRecordData[] ResponseFileRecords { get; set; }

        /// <summary>
        /// AND-Mask value received from device
        /// </summary
        /// <remarks>
        /// Used for <see cref="MaskWriteRegisterRequest"/> request (i.e., Modbus Function - 0x16 - Mask Write Register)
        /// </remarks>
        public ushort ResponseAndMask { get; set; }

        /// <summary>
        /// OR-Mask value received from device
        /// </summary
        /// <remarks>
        /// Used for <see cref="MaskWriteRegisterRequest"/> request (i.e., Modbus Function - 0x16 - Mask Write Register)
        /// </remarks>
        public ushort ResponseOrMask { get; set; }

        /// <summary>
        /// Maintans the device information responded
        /// </summary>
        ///<remarks>
        /// Used for <see cref="ReadDeviceIdentificationRequest"/> request (i.e., Modbus Function - 0x2B - Encapsulated Interface Transport)
        /// </remarks>
        public DeviceInfo[] ResponseDeviceInformation { get; set; }

        /// <summary>
        /// Response data interpreted as integer if the data size is 4 bytes
        /// </summary>
        /// <remarks>
        /// Note: this interpretation is not done by Modbus Api 
        /// rather by the consuming code to give an idea of interpreting received data
        /// </remarks>
        public int ResponseIntData { get; set; }

        /// <summary>
        /// Response data interpreted as float if the data size is 4 bytes
        /// </summary>
        public float ResponseFloatData { get; set; }

        /// <summary>
        /// Error code when failure response received from device
        /// </summary>
        public byte ResponseErrorCode { get; set; }

        /// <summary>
        /// Error message when failure response is received from device
        /// </summary>
        public string ResponseErrorMessage { get; set; }

        #endregion
    }
}
