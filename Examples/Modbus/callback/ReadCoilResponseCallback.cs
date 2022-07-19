

using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

public class ReadCoilResponseCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		ReadCoilsResponse success = (ReadCoilsResponse) response;
        CoilState[] coilStatus = success.GetCoilStatus();// returns the status of each coil.
        Console.WriteLine("received async/scheduled response for ReadCoil");
	}

	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadCoil: {0}: {1}",errorCode, errorMessage));
        //handle the error response
    }

}
