

using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class ReadFileRecordCallback : IModbusResponseCallback
{

	
	public void OnSuccess(ModbusSuccessResponse response) {
		ReadFileRecordResponse success = (ReadFileRecordResponse) response;
	    FileRecordData[] fileRecords = success.GetFileRecords();
        Console.WriteLine("received async/scheduled response for  ReadFileRecord");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadFileRecord: {0}: {1}", errorCode, errorMessage));
        //handle the error response
    }

}
