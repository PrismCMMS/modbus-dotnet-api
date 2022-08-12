using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// ReadWriteMultipleRegisters class performs a combination of one read operation and one write operation in a 
    /// single MODBUS transaction. The write operation is performed before the read.
    /// </summary>
    public class ReadWriteMultipleRegistersRequest : ModbusRequest {
        
        private byte byteCount;

        /// <summary>
        /// Instantiates a new read write multiple registers.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="readAddress">read address</param>
        /// <param name="readQuantity">read quantity</param>
        /// <param name="writeAddress">write address</param>
        /// <param name="writeQuantity">write quantity</param>
        /// <param name="convertToHex">convert to hex</param>
        public ReadWriteMultipleRegistersRequest(byte slaveId, ushort readAddress, ushort readQuantity, ushort writeAddress, byte writeQuantity, bool convertToHex) : base(slaveId, readAddress, readQuantity, convertToHex)
        {
            this.WriteAddress = writeAddress;
            this.WriteQuantity = writeQuantity;
            this.byteCount = (byte)(writeQuantity * 2);
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.READ_WRITE_MULTIPLE_REGISTERS;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            return base.GetLength() + 5 + WriteValues.Length;
        }

        /// <summary>
        /// Gets/Sets Write Value
        /// </summary>
        public byte[] WriteValues
        {
            get;
            set;
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
                    binaryWriter.Write(GetHexByteArray(WriteAddress, 2));
                    binaryWriter.Write(GetHexByteArray(WriteQuantity, 2));
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
            if (WriteValues.Length != (WriteQuantity * 2))
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_OUTPUT_LENGTH);
            }
        }

        /// <summary>
        /// gets/sets Write address
        /// </summary>
        public ushort WriteAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets write quantity
        /// </summary>
        public byte WriteQuantity
        {
            get;
            set;
        }

    }
}