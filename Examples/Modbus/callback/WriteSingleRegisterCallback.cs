

using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class WriteSingleRegisterCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		WriteSingleRegisterResponse success = ((WriteSingleRegisterResponse)response);
	   int value = success.GetValue();//value that was sent to Modbus device is return as a success response by the device
        Console.WriteLine("received async/scheduled response for  WriteSingleRegister");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("WriteSingleRegister: {0}: {1}", errorCode, errorMessage));
        //handle the error
    }

}
