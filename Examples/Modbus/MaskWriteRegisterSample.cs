
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using System;

public class MaskWriteRegisterSample
{

    public void TestMaskriteRegister(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateMaskWriteRegisterRequest();

            SyncCall(client, request); //sample code for making synchronized call
                                       //application wait for response after submitting the request

            AsyncCall(client, request); //sample code for making asynchronized call
                                        //application does not wait for response

            ScheduledCall(client, request);
            //application schedules a request. API will poll the device periodically for the same request
            client.Shutdown();
        }
        catch (ModbusException e)
        {
            Console.WriteLine(e);
            //handle the exception
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //handle the exception
        }
    }

    private void WriteSingleRegister(ModbusClient client, ushort address, ushort value)
    {
        try
        {
            client.Submit(new WriteSingleRegisterRequest(0xFF, address, value, true)).Get();
        }
        catch (Exception e)
        {
            Console.WriteLine("unexcepted exception " + e);
        }
    }

    private void SyncCall(ModbusClient client, ModbusRequest request)
    {

        WriteSingleRegister(client, 10, 624);
        Response response = client.Submit(request).Get();
        if (response is ModbusSuccessResponse)
        {
            MaskWriteRegisterResponse success = (MaskWriteRegisterResponse)response;
            success.GetAndMask();
            success.GetOrMask();
            Console.WriteLine("received sync response for  MaskWriteRegisters");
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("MaskWriteRegisters: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new MaskWriteRegisterCallback());
    }

    private static void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new MaskWriteRegisterCallback());
    }

    private ModbusRequest CreateMaskWriteRegisterRequest()
    {
        return new MaskWriteRegisterRequest(0xFF, 10, 242, 37, true);
    }
}
