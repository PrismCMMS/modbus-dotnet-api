using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// MaskWriteRegister class is used to modify the contents of a specified holding register 
    /// using a combination of an AND mask, an OR mask, and the register's current contents. 
    /// The function can be used to set or clear individual bits in the register
    /// </summary>
    public class MaskWriteRegisterRequest : ModbusRequest {

        /// <summary>
        /// Instantiates a new mask write register.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="address">address</param>
        /// <param name="andMask">And Mask</param>
        /// <param name="orMask">Or Mask</param>
        /// <param name="convertToHex">convert to hex</param>
        public MaskWriteRegisterRequest(byte slaveId, ushort address, ushort andMask, ushort orMask, bool convertToHex):base(slaveId, address, 0, convertToHex)
        {            
            OrMask = orMask;
            AndMask = andMask;
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns>function code</returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.MASK_WRITE_REGISTER;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns>length</returns>
        public override int GetLength()
        {
            return length;
        }

        /// <summary>
        /// Gets data in bytes
        /// </summary>
        /// <returns>data in bytes</returns>
        public override byte[] GetDataInBytes()
        {
            using (var buffer = new MemoryStream(7))
            {
                using (var binaryWriter = new BinaryWriter(buffer))
                {
                    binaryWriter.Write(GetFunctionCode());
                    binaryWriter.Write(GetHexByteArray(StartAddress, 2));
                    binaryWriter.Write(GetHexByteArray(AndMask, 2));
                    binaryWriter.Write(GetHexByteArray(OrMask, 2));

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

        /// <summary>
        /// Gets/Sets And Mask
        /// </summary>
        public ushort AndMask
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets Or Mask
        /// </summary>
        public ushort OrMask
        {
            get;
            set;
        }

    }
}