

using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class WriteFileRecordCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		var success = (WriteFileRecordResponse) response;
	    FileRecordData[] fileRecords = success.GetFileRecords();
        Console.WriteLine("received async/scheduled response for  WriteFileRecord");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("WriteFileRecord: {0}: {1}", errorCode, errorMessage));
        //handle the error response
    }
}
