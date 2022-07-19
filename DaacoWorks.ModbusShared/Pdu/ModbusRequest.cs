using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Protocol.Model;
using System.IO;

namespace Com.DaacoWorks.Modbus.Pdu
{
    /// <summary>
    ///  Base class for all the Modbus Request classes.
    /// </summary>
    public abstract class ModbusRequest : Protocol.Model.Request
    {
        
        /// <summary>
        /// Length
        /// </summary>
        protected int length = 5;

        /// <summary>
        /// Instantiates a new Modbus request PDU.
        /// </summary>
        /// <param name="slaveId">the slave id</param>
        /// <param name="startAddress">start address</param>
        /// <param name="quantity">quantity</param>
        /// <param name="convertToHex">convert to hex or not</param>
        public ModbusRequest(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex)
        {
            SlaveId = slaveId;
            StartAddress = startAddress;
            Quantity = quantity;
            IsConvertToHex = convertToHex;
        }

        /// <summary>
        /// Gets/sets Reuqest identifier
        /// </summary>
        public override RequestIdentifier RequestIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets Function Code
        /// </summary>
        /// <returns>function code</returns>
        public abstract byte GetFunctionCode();

        /// <summary>
        /// Validate request
        /// </summary>
        public abstract void Validate();

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns>length</returns>
        public override int GetLength()
        {
            return length;
        }

        /// <summary>
        /// Gets/sets slave id
        /// </summary>
        public byte SlaveId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets start address
        /// </summary>
        public ushort StartAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets quantity
        /// </summary>
        public ushort Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets flag to decide whether to Convert To Hex or not
        /// </summary>
        public bool IsConvertToHex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the data in bytes.
        /// </summary>
        /// <returns>data in byte array</returns>
        public virtual byte[] GetDataInBytes()
        {
            using (var buffer = new MemoryStream(length))
            {
                using (var binaryWriter = new BinaryWriter(buffer))
                {
                    binaryWriter.Write(GetFunctionCode());
                    binaryWriter.Write(GetHexByteArray(StartAddress, 2)); //, (int)buffer.Position, 2);
                    binaryWriter.Write(GetHexByteArray(Quantity, 2)); //, (int)buffer.Position, 2);
                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets the hex byte array.
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="length">length</param>
        /// <returns></returns>
        protected byte[] GetHexByteArray(int value, int length)
        {
            return IsConvertToHex ?
                 ModbusUtil.ToHexByteArray((value & 0x0000FFFF), length) :
            ModbusUtil.ToHexByteArray((value & 0x0000FFFF).ToString(), length);
        }

        /// <summary>
        /// Gets/Sets whether the request made is scheduled request or not
        /// True represents Scheduled request and false represents either Synchronous or Asynchronous request
        /// </summary>
        public override bool IsScheduledRequest { get; set; }

    }
}