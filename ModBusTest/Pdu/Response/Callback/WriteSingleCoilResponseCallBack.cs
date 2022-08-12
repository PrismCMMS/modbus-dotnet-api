using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;

public class WriteSingleCoilResponseCallBack : IModbusResponseCallback
{

    private static ILogger logger = LoggerFactory.GetLogger(typeof(WriteSingleCoilResponseCallBack).FullName);


    public void OnSuccess(ModbusSuccessResponse response)
    {
        logger.Info("Response received in WriteSingleCoilResponseCallBack ");
    }


    public void OnError(ModbusErrorResponse error)
    {
        logger.Info("Error response received in WriteSingleCoilResponseCallBack " + error.GetErrorCode());
    }

}
