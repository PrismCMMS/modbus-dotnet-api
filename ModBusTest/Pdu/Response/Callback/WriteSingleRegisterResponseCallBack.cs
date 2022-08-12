using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;

public class WriteSingleRegisterResponseCallBack : IModbusResponseCallback {

	private static ILogger logger = LoggerFactory.GetLogger(typeof(WriteSingleCoilResponseCallBack).FullName);

	
	public void OnSuccess(ModbusSuccessResponse response) {		
		logger.Info("Response received in WriteSingleRegisterResponseCallBack ");
	}

	
	public void OnError(ModbusErrorResponse error) {
		logger.Info("Error response received in WriteSingleRegisterResponseCallBack "+ error.GetErrorCode());
	}

}