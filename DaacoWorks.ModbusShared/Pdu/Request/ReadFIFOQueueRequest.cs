using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// ReadFIFOQueue class allows to read the contents of a First-In-First-Out (FIFO) queue of register in a remote device.
    /// </summary>
    public class ReadFIFOQueueRequest : ModbusRequest {

        /// <summary>
        /// Instantiates a new read FIFO queue.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="FIFOAddress">FIFO address</param>
        /// <param name="convertToHex">convert to hex</param>
        public ReadFIFOQueueRequest(byte slaveId, ushort FIFOAddress, bool convertToHex): base(slaveId, FIFOAddress, 0, convertToHex)
        {
            
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns></returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.READ_FIFO_QUEUE;
        }

        /// <summary>
        /// Gets data in bytes
        /// </summary>
        /// <returns></returns>
        public override byte[] GetDataInBytes()
        {
            using (var buffer = new MemoryStream(3))
            {
                using (var binaryWriter = new BinaryWriter(buffer))
                {
                    binaryWriter.Write(GetFunctionCode());
                    binaryWriter.Write(GetHexByteArray(StartAddress, 2));
                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            return 3;
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
