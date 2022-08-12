using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using Com.DaacoWorks.Protocol.Logger;
using System;

public class ReadHoldingRegistersCallBack : IModbusResponseCallback {

	private static ILogger logger = LoggerFactory.GetLogger(typeof(ReadHoldingRegistersCallBack).FullName);

	
	public void OnSuccess(ModbusSuccessResponse response) {
        //ByteBuffer.allocate(response.getData().length);
        var byteArray = response.GetData();
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);       

		int capacity = floatArray.Length;
		//byte [] data = ByteBuffer.wrap(response.getData()).array();
		for(int i=0;i<capacity;i++) {
			logger.Info("Response received in ReadHoldingRegistersCallBack "+ floatArray[i]);
		}

	}

	
	public void OnError(ModbusErrorResponse error) {
		logger.Info("Error response received in ReadHoldingRegistersCallBack "+error.GetErrorCode());
	}

}
