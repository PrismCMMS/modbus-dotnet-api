using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;

public class WriteMultipleRegistersResponseCallBack : IModbusResponseCallback {
	
	private static ILogger logger = LoggerFactory.GetLogger(typeof(WriteMultipleRegistersResponseCallBack).FullName);

	
	public void OnSuccess(ModbusSuccessResponse response) {
		logger.Info("Response received in WriteMultipleRegistersResponseCallBack "+response.GetData());
	}

	
	public void OnError(ModbusErrorResponse error) {
		logger.Info("Error response received in WriteMultipleRegistersResponseCallBack "+ error.GetErrorCode());
	}

}
