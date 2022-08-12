using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Modbus.Pdu
{
    /// <summary>
    /// ModbusSuccessResponse base class is representing the success response received from Modbus server.
    /// </summary>
    public class ModbusSuccessResponse : SuccessResponse
    {

        /// <summary>
        /// response pdu
        /// </summary>
        protected ModbusResponse responsePDU;

        /// <summary>
        /// request pdu
        /// </summary>
        protected ModbusRequest requestPDU;

        /// <summary>
        /// Instantiates a new modbus success response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public ModbusSuccessResponse(ModbusRequest requestPDU)
        {
            this.requestPDU = requestPDU;
        }

        /// <summary>
        /// Sets response pdu
        /// </summary>
        /// <param name="pdu"></param>
        public void SetResponsePDU(ModbusResponse pdu)
        {
            this.responsePDU = pdu;
        }

        /// <summary>
        /// Gets data in bytes
        /// </summary>
        /// <returns></returns>
        public virtual byte[] GetData()
        {
            return responsePDU.GetDataInBytes();
        }

        /// <summary>
        /// Gets response length
        /// </summary>
        /// <returns></returns>
        public override int GetLength()
        {
            return 0;
        }

        /// <summary>
        /// Gets/Sets the request.
        /// </summary>
        public override Protocol.Model.Request Request
        {
            get { return requestPDU; }
            set { requestPDU = (ModbusRequest)value; }
        }
    }
}