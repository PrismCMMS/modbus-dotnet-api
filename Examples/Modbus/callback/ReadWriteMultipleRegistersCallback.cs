
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class ReadWriteMultipleRegistersCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		ModbusSuccessResponse success = (ModbusSuccessResponse) response;
		success.GetData();//expect the values that was sent to the device to set.
        Console.WriteLine("received async/scheduled response for  ReadWriteMultipleRegisters");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadWriteMultipleRegisters: {0}: {1}", errorCode, errorMessage));
    }
}
