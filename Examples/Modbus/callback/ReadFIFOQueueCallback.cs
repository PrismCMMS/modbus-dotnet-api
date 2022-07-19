
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class ReadFIFOQueueCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		ReadFIFOQueueResponse success = (ReadFIFOQueueResponse) response;
		success.GetData();//value of the register
        Console.WriteLine("received async/scheduled response for  ReadFIFOQueue");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadFIFOQueue: {0}: {1}", errorCode, errorMessage));
        //handle the error response
    }


}
