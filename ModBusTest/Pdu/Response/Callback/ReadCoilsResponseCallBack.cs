

using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Logger;
using System.IO;

public class ReadCoilsResponseCallBack : IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> {
	
	private static ILogger logger = LoggerFactory.GetLogger(typeof(ReadCoilsResponseCallBack).FullName);
    	
	public void OnSuccess(ModbusSuccessResponse response) {
		//ByteBuffer.allocate(response.GetData().Length);
		byte [] data = new MemoryStream(response.GetData()).ToArray();
		for(int i=0;i<data.Length;i++) {
			logger.Info("Response received in ReadCoilsResponseCallback "+data[i]);
		}
	}

	
	public void OnError(ModbusErrorResponse error) {
		logger.Info("Error response received in ReadCoilsResponseCallBack "+ error.GetErrorCode());
	}
}