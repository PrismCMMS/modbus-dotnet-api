using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Modbus.Pdu.Executor {

    /// <summary>
    /// ModbusPDUWrapper is a wrapper which wraps the Modbus request, callback and associated generated requestId.
    /// </summary>
    public class ModbusPDUWrapper : RequestPDUWrapper<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse> {

        /// <summary>
        /// Instantiates a new modbus PDU wrapper.
        /// </summary>
        /// <param name="callBack">callback</param>
        /// <param name="pdu">the pdu</param>
        public ModbusPDUWrapper(IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack, ModbusRequest pdu): base(callBack, pdu)
        {
            
        }

        /// <summary>
        /// Gets timeout error which is thrown internally when peer did not respond for more than specified timeout time
        /// </summary>
        /// <returns></returns>
        public override ModbusErrorResponse GetTimeoutError() {
            ModbusErrorResponse error = new ModbusErrorResponse();
            error.Request = Pdu;

            ModbusResponse pdu = new ModbusResponse(Pdu.GetFunctionCode(), new byte[] { 0x0B });
            error.SetResponsePDU(pdu);
            return error;
        }


    }
}