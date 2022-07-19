

using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;

public class WriteMultipleCoilsCallback : IModbusResponseCallback
{


    public void OnSuccess(ModbusSuccessResponse response)
    {
        WriteMultipleCoilsResponse success = ((WriteMultipleCoilsResponse)response);
        var value = success.GetQuantity();//value that was sent to Modbus device is return as a success response by the device
        Console.WriteLine("received async/scheduled response for  WriteMultipleCoils");
    }


    public void OnError(ModbusErrorResponse error)
    {
        int errorCode = error.GetErrorCode();
        string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("WriteMultipleCoils: {0}: {1}", errorCode, errorMessage));
        //handle the error
    }

}
