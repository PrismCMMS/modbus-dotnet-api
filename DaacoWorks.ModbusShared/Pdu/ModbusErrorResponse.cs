using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Protocol.Model;
using System;

namespace Com.DaacoWorks.Modbus.Pdu
{

    /// <summary>
    /// ModbusErrorResponse class represents the error response received for a modbus request.
    /// </summary>
    public class ModbusErrorResponse : ErrorResponse {

        
        /// <summary>
        /// response PDU
        /// </summary>
        protected ModbusResponse responsePDU;        

        /// <summary>
        /// Sets the response PDU.
        /// </summary>
        /// <param name="pdu"></param>
        public void SetResponsePDU(ModbusResponse pdu) {
            this.responsePDU = pdu;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <returns></returns>
        public byte GetErrorCode() {
            return responsePDU.GetDataInBytes()[0];
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            return Enum.ToObject(typeof(ModbusResponseErrorCode),responsePDU.GetDataInBytes()[0]).ToString();
        }

        /// <summary>
        /// Gets length
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
            get; set;
        }
       
    }
}