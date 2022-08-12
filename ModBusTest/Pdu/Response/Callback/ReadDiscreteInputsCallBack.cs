using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;
using System.IO;

public class ReadDiscreteInputsCallBack : IModbusResponseCallback
{

    private static ILogger logger = LoggerFactory.GetLogger(typeof(ReadCoilsResponseCallBack).FullName);


    public void OnSuccess(ModbusSuccessResponse response)
    {
        //var buffer = ByteBuffer.allocate(response.getData().length);

        byte[] data = new MemoryStream(response.GetData()).ToArray();

        for (int i = 0; i < data.Length; i++)
        {
            logger.Info("Response received in ReadDiscreteInputsCallBack " + data[i]);
        }

    }


    public void OnError(ModbusErrorResponse error)
    {

        logger.Info("Error response received in ReadDiscreteInputsCallBack " + error.GetErrorCode());

    }

}
