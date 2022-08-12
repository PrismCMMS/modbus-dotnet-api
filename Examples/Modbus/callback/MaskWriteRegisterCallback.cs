
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class MaskWriteRegisterCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		MaskWriteRegisterResponse success = (MaskWriteRegisterResponse) response;
		success.GetAndMask();
		success.GetOrMask();
        Console.WriteLine("received async/scheduled response for MaskWriteRegister");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("MaskWriteRegister: {0}: {1}", errorCode, errorMessage));
    }

}
