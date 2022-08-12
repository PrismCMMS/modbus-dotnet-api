using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Pdu.Request;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ModbusWriteMultipleRegistersResponse is a success response for the request <see cref="WriteMultipleRegistersRequest"/> 
    /// </summary>
    public class WriteMultipleRegistersResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new modbus write multiple registers response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public WriteMultipleRegistersResponse(ModbusRequest requestPDU): base(requestPDU)
        {
            
        }

        /// <summary>
        /// Gets the starting address.
        /// </summary>
        /// <returns>start address</returns>
        public ushort GetStartingAddress()
        {
            return ModbusUtil.ToInt16(GetData(), 0);
        }

        /// <summary>
        /// Gets quantity of written values
        /// </summary>
        /// <returns>values</returns>
        public ushort GetQuantity()
        {
            return ModbusUtil.ToInt16(GetData(), 2);
        }

    }
}