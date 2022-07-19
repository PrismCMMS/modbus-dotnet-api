using Com.DaacoWorks.Modbus.Pdu.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// WriteSingleRegister class is used to write a single holding register in a remote device
    /// </summary>
    public class WriteSingleRegisterRequest : ModbusRequest
    {

        /// <summary>
        /// Instantiates a new write single register.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="address">address</param>
        /// <param name="value">value</param>
        /// <param name="convertToHex">convert to hex</param>
        public WriteSingleRegisterRequest(byte slaveId, ushort address, ushort value, bool convertToHex) : base(slaveId, address, value, convertToHex)
        {

        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.WRITE_SINGLE_REGISTER;
        }

        /// <summary>
        /// Gets the register value to be written
        /// </summary>
        /// <returns></returns>
        public ushort GetWriteValue()
        {
            return Quantity;
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