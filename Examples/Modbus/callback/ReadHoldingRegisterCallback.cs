
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;
using System.Diagnostics;

public class ReadHoldingRegisterCallback : IModbusResponseCallback {

	
	public void OnSuccess(ModbusSuccessResponse response) {
		ReadHoldingRegistersResponse success = (ReadHoldingRegistersResponse) response;
		byte[] data = response.GetData(); 
		   // raw bytes received from the Modbus device as a success response 
		   //use the utility methods to convert the raw bytes into meaningful data 
		   // if the raw bytes to be converted as floats 
		try {
			float[] floatValues = ModbusUtil.ToFloatValue(data, false, true);//raw bytes, byteSwap, wordSwap
			    
			// or if the raw bytes to be converted as integers 
			    int[] intValues = ModbusUtil.ToIntValue(data, false, true);
		} catch (ModbusException e) {
			Debug.WriteLine(e);
			//handle the exception
		}

        Console.WriteLine("received async/scheduled response for  ReadHoldingRegister");
    }
	
	
	public void OnError(ModbusErrorResponse error) {
		int errorCode = error.GetErrorCode();
		string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("ReadHoldingRegister: {0}: {1}", errorCode, errorMessage));
        //handle the error
    }
}
