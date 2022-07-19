using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class ReadDeviceIndentificationCallback : IModbusResponseCallback{

	
	public void OnSuccess(ModbusSuccessResponse response) {
		ReadDeviceIdentificationResponse success = (ReadDeviceIdentificationResponse) response;
		DeviceInfo[] deviceInfo = success.GetDeviceInformation();
        Console.WriteLine("received async/scheduled response for  ReadDeviceIndentification");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadDeviceIndentification: {0}: {1}", errorCode, errorMessage));
        //handle the error response
    }

}
