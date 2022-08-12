

using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Response.Callback;
using System;
using System.Diagnostics;

public class WriteMultipleRegistersCallback : IModbusResponseCallback
{


    public void OnSuccess(ModbusSuccessResponse response)
    {
        WriteMultipleRegistersResponse success = ((WriteMultipleRegistersResponse)response);
        try
        {
            int[] values = ModbusUtil.ToIntValue(success.GetData(), true, true);
        }
        catch (ModbusException e)
        {
            // TODO Auto-generated catch block
            Debug.WriteLine(e);
        }
        Console.WriteLine("received async/scheduled response for  WriteMultipleRegisters");
    }


    public void OnError(ModbusErrorResponse error)
    {
        int errorCode = error.GetErrorCode();
        string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("WriteMultipleRegisters: {0}: {1}", errorCode, errorMessage));
        //handle the error
    }

}
