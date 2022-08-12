
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;

public class ReadInputRegistersCallBack : IModbusResponseCallback
{

    private static ILogger logger = LoggerFactory.GetLogger(typeof(ReadInputRegistersCallBack).FullName);


    public void OnSuccess(ModbusSuccessResponse response)
    {
        //ByteBuffer buffer = ByteBuffer.allocate(response.getData().length);
        //float value = ByteBuffer.wrap(response.getData()).getFloat();
        //int capacity = fBuffer.capacity();
        //byte [] data = ByteBuffer.wrap(response.getData()).array();
        //for(int i=0;i<capacity;i++) {
        logger.Info("Response received in ReadInputRegistersCallBack " + response.GetData());
        //}

    }


    public void OnError(ModbusErrorResponse error)
    {
        logger.Info("Error response received in ReadInputRegistersCallBack " + error.GetErrorCode());
    }

}
