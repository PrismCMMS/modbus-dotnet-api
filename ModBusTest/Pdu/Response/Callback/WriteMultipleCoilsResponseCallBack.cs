using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;

public class WriteMultipleCoilsResponseCallBack : IModbusResponseCallback
{

    private static ILogger logger = LoggerFactory.GetLogger(typeof(WriteMultipleCoilsResponseCallBack).FullName);


    public void OnSuccess(ModbusSuccessResponse response)
    {
        logger.Info("Response received in WriteMultipleCoilsResponseCallBack ");
    }


    public void OnError(ModbusErrorResponse error)
    {
        logger.Info("Error response received in WriteMultipleCoilsResponseCallBack " + error.GetErrorCode());
    }

}
