using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// WriteMultipleCoils class is used to force each coil in a sequence of coils to either ON or OFF in a remote device.
    /// </summary>
    public class WriteMultipleCoilsRequest : ModbusRequest {

        byte byteCount;

        /// <summary>
        /// Instantiates a new write multiple coils.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="quantity">quantity</param>
        /// <param name="convertToHex">convert to hex</param>
        public WriteMultipleCoilsRequest(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex) : base(slaveId, startAddress, quantity, convertToHex)
        {
            byteCount = (byte)(quantity <= 8 ? 1 : (byte)(quantity / 8) + (quantity % 8 > 0 ? 1 : 0));
        }

        /// <summary>
        /// Gets/sets write values
        /// </summary>
        public byte[] WriteValues
        {
            get;
            set;
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.WRITE_MULTIPLE_COILS;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            return base.GetLength() + 1 + WriteValues.Length;
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
                    var data = base.GetDataInBytes();
                    binaryWriter.Write(base.GetDataInBytes());
                    binaryWriter.Write(GetHexByteArray(byteCount, 1));
                    binaryWriter.Write(WriteValues);
                    return buffer.ToArray();
                }
            }
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