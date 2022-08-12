using Com.DaacoWorks.Modbus.Pdu.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// ReadHoldingRegisters class is used to read the contents of a contiguous block of holding registers in a remote device.
    /// </summary>
    public class ReadHoldingRegistersRequest : ModbusRequest {

        /// <summary>
        /// Instantiates a new read holding registers.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="quantity">quantity</param>
        /// <param name="convertToHex">convert to hex</param>
        public ReadHoldingRegistersRequest(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex) : base(slaveId, startAddress, quantity, convertToHex)
        {

        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns>function code</returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.READ_HOLDING_REGISTERS;
        }

        /// <summary>
        /// Validate request
        /// </summary>
        public override void Validate()
        {
            //Let device take care of validation and not our layer
        }

    }
}