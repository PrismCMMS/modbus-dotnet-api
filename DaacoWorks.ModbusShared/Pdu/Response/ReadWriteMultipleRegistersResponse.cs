using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadWriteMultipleRegistersResponse is a success response for the request <see cref="ReadWriteMultipleRegistersRequest"/> 
    /// </summary>
    public class ReadWriteMultipleRegistersResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new Read Write Multiple Registers Response
        /// </summary>
        /// <param name="requestPDU"></param>
        public ReadWriteMultipleRegistersResponse(ModbusRequest requestPDU) : base(requestPDU)
        {
        }
        
    }
}
