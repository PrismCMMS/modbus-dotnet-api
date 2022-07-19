using Com.DaacoWorks.Modbus.Pdu.Request;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadInputRegistersResponse is a success response for the request <see cref="ReadInputRegistersRequest"/> 
    /// </summary>
    public class ReadInputRegistersResponse : ModbusSuccessResponse {

        /// <summary>
        /// Instantiates a new modbus read input registers response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public ReadInputRegistersResponse(ModbusRequest requestPDU) : base(requestPDU)
        {
            
        }
        
    }
}