using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Util;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ModbusWriteSingleRegisterResponse is a success response for the request <see cref="WriteSingleRegisterRequest"/>}
    /// </summary>
    public class WriteSingleRegisterResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new modbus write single register response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public WriteSingleRegisterResponse(ModbusRequest requestPDU) : base(requestPDU)
        {

        }

        /// <summary>
        /// Gets start address
        /// </summary>
        /// <returns>start address</returns>
        public ushort GetStartingAddress()
        {
            return ModbusUtil.ToInt16(GetData(), 0);
        }

        /// <summary>
        /// Gets value
        /// </summary>
        /// <returns>value</returns>
        public ushort GetValue()
        {
            return ModbusUtil.ToInt16(GetData(), 2);
        }
    }
}