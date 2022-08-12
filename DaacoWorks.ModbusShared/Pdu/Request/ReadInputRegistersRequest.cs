using Com.DaacoWorks.Modbus.Pdu.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// ReadInputRegisters class is used to read from 1 to 125 contiguous input registers in a remote device.
    /// </summary>
    public class ReadInputRegistersRequest : ModbusRequest {

        /// <summary>
        /// Instantiates a new read input registers.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="quantity">quantity</param>
        /// <param name="convertToHex">convert to hex</param>
        public ReadInputRegistersRequest(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex) : base(slaveId, startAddress, quantity, convertToHex)
        {


        }
       
        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.READ_INPUT_REGISTERS;
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