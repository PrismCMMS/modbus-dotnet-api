using Com.DaacoWorks.Modbus.Pdu.Constants;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// WriteSingleCoil class is used to write a single output to either ON or OFF in a remote device
    /// </summary>
    public class WriteSingleCoilRequest : ModbusRequest
    {

        /// <summary>
        /// Instantiates a new write single coil.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="onOrOff">On or off</param>
        /// <param name="convertToHex">convert to hex</param>
        public WriteSingleCoilRequest(byte slaveId, ushort startAddress, CoilState onOrOff, bool convertToHex) : base(slaveId, startAddress, (ushort)onOrOff, convertToHex)
        {
            
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.WRITE_SINGLE_COIL;
        }

        /// <summary>
        /// Gets the CoilSate that is to be written
        /// </summary>
        /// <returns></returns>
        public CoilState GetCoilState()
        {
            return (CoilState)Enum.ToObject(typeof(CoilState), Quantity);
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