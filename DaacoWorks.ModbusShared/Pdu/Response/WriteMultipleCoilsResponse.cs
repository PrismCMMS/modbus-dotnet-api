using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Pdu.Request;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// WriteMultipleCoilsResponse is a success response for the request <see cref="WriteMultipleCoilsRequest"/> 
    /// </summary>
    public class WriteMultipleCoilsResponse : ModbusSuccessResponse
    {

        /// <summary>
        /// Instantiates a new modbus write multiple coils response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public WriteMultipleCoilsResponse(ModbusRequest requestPDU): base(requestPDU)
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
        /// Gets the Quantity of values written.
        /// </summary>
        /// <returns>values</returns>
        public ushort GetQuantity()
        {
            return ModbusUtil.ToInt16(GetData(), 2);
        }

    }
}