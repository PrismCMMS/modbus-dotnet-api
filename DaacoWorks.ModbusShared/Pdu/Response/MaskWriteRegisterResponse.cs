using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Util;
using System;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{

    /// <summary>
    /// MaskWriteRegistersResponse is a success response for the request <see cref="MaskWriteRegisterRequest"/> }
    /// </summary>
    public class MaskWriteRegisterResponse : ModbusSuccessResponse
    {
        /// <summary>
        /// Instantiates a new modbus mask write registers response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public MaskWriteRegisterResponse(ModbusRequest requestPDU) : base(requestPDU)
        {

        }

        /// <summary>
        /// Gets the and mask.
        /// </summary>
        /// <returns>And Mask value</returns>
        public ushort GetAndMask()
        {
            byte[] data = responsePDU.GetDataInBytes();
            return ModbusUtil.ToInt16(data, 0);
        }

        /// <summary>
        /// Gets the or mask.
        /// </summary>
        /// <returns>Or Mask Value</returns>
        public ushort GetOrMask()
        {
            byte[] data = responsePDU.GetDataInBytes();
            return ModbusUtil.ToInt16(data, 2);
        }

    }

}