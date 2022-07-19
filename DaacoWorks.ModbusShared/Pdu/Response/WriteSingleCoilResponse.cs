using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Util;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ModbusWriteSingleCoilResponse is a success response for the request <see cref="WriteSingleCoilRequest" />
    /// </summary>
    public class WriteSingleCoilResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new modbus write single coil response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public WriteSingleCoilResponse(ModbusRequest requestPDU): base(requestPDU)
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
        public CoilState GetCoilState()
        {
            if (ModbusUtil.ToInt16(GetData(), 2) == (ushort)CoilState.ON)
                return CoilState.ON;
            return CoilState.OFF;
        }


    }
}