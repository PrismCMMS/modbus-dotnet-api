

using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Util;
using System.IO;

namespace Com.DaacoWorks.Modbus.Header {

    /// <summary>
    /// ModbusTCPHeader class represents the header information present in the Modbus TCP message.
    /// </summary>
    public class ModbusTCPHeader : Com.DaacoWorks.Protocol.Headers.Header
    {
        /// <summary>
        /// Instantiates a new modbus TCP header.
        /// </summary>
        /// <param name="pdu">the pdu</param>
        public ModbusTCPHeader(ModbusRequest pdu):base(pdu) {
            
            this.length = pdu == null ? 0 : pdu.GetLength();
        }

        private int transactionId = 0;

        private int protocolId = 0;

        private int length = 0;

        /// <summary>
        /// Get Header as bytes
        /// </summary>
        /// <returns></returns>
        public override byte[] GetHeaderInBytes() {

            //MBAP header size is 7 bytes
            using (var buffer = new MemoryStream(7))
            {
                using (var binaryWriter = new BinaryWriter(buffer))
                {

                    //TransactionId (2 bytes),ProtocolId (2 bytes),Length (2 bytes),slaveId (1 byte)
                    binaryWriter.Write(GetNextTransactionId());
                    binaryWriter.Write((short)0);
                    binaryWriter.Write(ModbusUtil.ToShortWithBytesSwapped(length + 1));//add +1 to include slaveId
                    binaryWriter.Write(((ModbusRequest)pdu).SlaveId);
                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Sets the transaction id.
        /// </summary>
        /// <param name="transactionId">transaction id</param>
        /// <returns>the modbus TCP header</returns>
        public ModbusTCPHeader SetTransactionId(int transactionId) {
            this.transactionId = transactionId;
            return this;
        }

        /// <summary>
        /// Sets the protocol id.
        /// </summary>
        /// <param name="protocolId">protocol id</param>
        /// <returns>the modbus TCP header</returns>
        public ModbusTCPHeader SetProtocolId(int protocolId) {
            this.protocolId = protocolId;
            return this;
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name="length">the length</param>
        /// <returns>the modbus TCP header</returns>
        public ModbusTCPHeader SetLength(int length) {
            this.length = length;
            return this;
        }

        /// <summary>
        /// Gets transaction id
        /// </summary>
        /// <returns>transaction id</returns>
        public int GetTransactionId() {
            return transactionId;
        }

        /// <summary>
        /// Gets protocol id
        /// </summary>
        /// <returns>protocol id</returns>
        public int GetProtocolId() {
            return protocolId;
        }

        /// <summary>
        /// Gets the length
        /// </summary>
        /// <returns>the length</returns>
        public int GetLength() {
            return length;
        }

        private byte[] GetNextTransactionId() {
            return ModbusUtil.ToHexByteArray(transactionId & 0x0000FFFF, 2);
        }

        /// <summary>
        /// Gets request id
        /// </summary>
        /// <returns>the request id</returns>
        public int GetRequestId() {
            return transactionId;
        }

        /// <summary>
        /// Sets request id
        /// </summary>
        /// <param name="requestId">the request id</param>
        public void SetRequestId(int requestId) {
            this.transactionId = requestId;
        }

    }
}