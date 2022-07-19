using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Util;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadHoldingRegistersResponse is a success response for the request <see cref="ReadHoldingRegistersRequest"/> 
    /// </summary>
    public class ReadHoldingRegistersResponse : ModbusSuccessResponse
    {

        /// <summary>
        /// Instantiates a new modbus read holding registers response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public ReadHoldingRegistersResponse(ModbusRequest requestPDU) : base(requestPDU)
        {

        }

        /// <summary>
        /// Gets the register data responded
        /// </summary>
        /// <returns></returns>
        public ushort[] GetRegisterValues()
        {
            var responseData = responsePDU.GetDataInBytes();
            var size = responseData.Length / 2;
            ushort[] values = new ushort[size];
            for (int i = 0, j = 0; i < size; i++, j += 2)
            {
                values[i] = ModbusUtil.ToInt16(responseData, j);
            }
            return values;
        }
    }
}