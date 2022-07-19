

namespace Com.DaacoWorks.Modbus.Pdu
{

    /// <summary>
    /// Base class for all the Modbus response classes.
    /// </summary>
    public class ModbusResponse : Protocol.Model.Response
    {

        private byte functionCode = 0x80;

        private byte byteCount;

        private byte[] data;

        /// <summary>
        /// Gets/Sets Request
        /// </summary>
        public override Protocol.Model.Request Request { get; set; }

        /// <summary>
        /// Instantiates a new modbus response PDU.
        /// </summary>
        /// <param name="functionCode">function code</param>
        /// <param name="data">data byte array</param>
        public ModbusResponse(byte functionCode, byte[] data) {
            this.functionCode = functionCode;
            this.data = data;
            this.byteCount = (byte)data.Length;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns>length</returns>
        public override int GetLength() {
            return data == null ? 0 : data.Length;
        }

        /// <summary>
        /// Gets the data in bytes.
        /// </summary>
        /// <returns>data in bytes</returns>
        public byte[] GetDataInBytes() {
            return data;
        }

        /// <summary>
        /// Gets the function code
        /// </summary>
        /// <returns>function code</returns>
        public byte GetFunctionCode() {
            return functionCode;
        }

        /// <summary>
        /// Sets the function code.
        /// </summary>
        /// <param name="functionCode">function code</param>
        public void SetFunctionCode(byte functionCode) {
            this.functionCode = functionCode;
        }

        /// <summary>
        /// Gets the byte count.
        /// </summary>
        /// <returns>the byte count</returns>
        public byte GetByteCount() {
            return byteCount;
        }

    }
}