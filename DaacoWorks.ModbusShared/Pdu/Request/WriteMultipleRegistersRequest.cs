using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// WriteMultipleRegisters class is used to write a block of contiguous registers (1 to 123 registers) in a remote device.
    /// </summary>
    public class WriteMultipleRegistersRequest : ModbusRequest
    {

        private int byteCount;
        private byte[] writeValuesInBytes = null;
        private ushort[] writeValues = null;

        /// <summary>
        /// Instantiates a new write multiple registers.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="quantity">quantity</param>
        /// <param name="convertToHex">convert to hex</param>
        public WriteMultipleRegistersRequest(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex) : base(slaveId, startAddress, quantity, convertToHex)
        {
            //TODO validate the input params and throw exceptions
            byteCount = (byte)(quantity * 2);
        }

        /// <summary>
        /// Gets/Sets Write Values
        /// </summary>
        public ushort[] WriteValues
        {
            get { return writeValues; }
            set
            {
                writeValuesInBytes = null;
                writeValues = value;                
            }
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.WRITE_MULTIPLE_REGISTERS;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            return base.GetLength() + 1 + (WriteValues.Length * 2);
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
                    binaryWriter.Write(data);
                    binaryWriter.Write(GetHexByteArray(byteCount, 1));
                    binaryWriter.Write(GetValueInBytes());
                    return buffer.ToArray();
                }
            }
        }
        
        private byte[] GetValueInBytes()
        {
            if (writeValuesInBytes != null)
                return writeValuesInBytes;

            if (writeValues != null && writeValues.Length > 0)
            {
                var size = writeValues.Length * 2;
                writeValuesInBytes = new byte[size];
                for (int i = 0, j=0; i < size; j++)
                {
                    writeValuesInBytes[i++] = (byte)(writeValues[j]>>8);
                    writeValuesInBytes[i++] = (byte)(writeValues[j] & 0xff);
                }
                return writeValuesInBytes;
            }
            throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, "Write value is empty. set the register value");
        }

        /// <summary>
        /// Validate request
        /// </summary>
        public override void Validate()
        {
            GetValueInBytes();
        }

    }
}