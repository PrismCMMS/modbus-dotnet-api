using Com.DaacoWorks.Modbus.Pdu.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{
    /// <summary>
    /// ReadCoils request is used to read from 1 to 2000 contiguous status of coils in a remote device.
    /// </summary>
    public class ReadCoilsRequest : ModbusRequest
    {
        /// <summary>
        /// Instantiates a new read coils.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="quantity">quantity</param>
        /// <param name="convertToHex">convert to hex</param>
        public ReadCoilsRequest(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex) : base(slaveId, startAddress, quantity, convertToHex)
        {

        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.READ_COILS;
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