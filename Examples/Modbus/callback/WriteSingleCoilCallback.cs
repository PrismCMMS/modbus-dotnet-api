

using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class WriteSingleCoilCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
	   WriteSingleCoilResponse success = ((WriteSingleCoilResponse)response);
	   var value = success.GetCoilState();//value that was sent to Modbus device is return as a success response by the device
        Console.WriteLine("received async/scheduled response for  WriteSingleCoil");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("WriteSingleCoil: {0}: {1}", errorCode, errorMessage));
        //handle the error
    }

}
