
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

public class ReadDiscreteInputCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		var success = (ReadDiscreteInputsResponse) response;
	    CoilState[] coilStatus = success.GetDiscreteInputStatus();// returns the status of each coil.
        Console.WriteLine("received async/scheduled response for  ReadDiscreteInput");
    }

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadDiscreteInput: {0}: {1}", errorCode, errorMessage));
        //handle the error response
    }

}
