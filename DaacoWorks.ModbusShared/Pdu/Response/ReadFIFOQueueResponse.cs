using Com.DaacoWorks.Modbus.Pdu.Request;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadFIFOQueueResponse is a success response for the request <see cref="ReadFIFOQueueRequest" /> 
    /// </summary>
    public class ReadFIFOQueueResponse : ModbusSuccessResponse {

        /// <summary>
        /// Instantiates a new modbus FIFO queue response.
        /// </summary>
        /// <param name="requestPDU">request PDU</param>
        public ReadFIFOQueueResponse(ModbusRequest requestPDU): base(requestPDU)
        {
            
        }        

    }
}
