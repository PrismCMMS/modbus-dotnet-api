using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using DaacoWorks.Extension.ResponseHandlers;


namespace DaacoWorks.Extension
{
    class ModbusResponseCallback : IModbusResponseCallback
    {

        public void OnSuccess(ModbusSuccessResponse response)
        {
            ResponseHandler.GetResponseHandler().AddToQueue(response);
        }

        public void OnError(ModbusErrorResponse error)
        {
            ResponseHandler.GetResponseHandler().AddToQueue(error);
        }
    }
}
