

using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using System;

public class ReadWriteMultipleRegistersSample
{

    public void TestReadWriteMultipleRegisters(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateReadWriteMultipleRegistersRequest();

            SyncCall(client, request); //sample code for making synchronized call
                                       //application wait for response after submitting the request

            AsyncCall(client, request); //sample code for making asynchronized call
                                        //application does not wait for response

            ScheduledCall(client, request);
            //application schedules a request. API will poll the device periodically for the same request

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

    private void SyncCall(ModbusClient client, ModbusRequest request)
    {
        Response response = client.Submit(request).Get();
        if (response is ModbusSuccessResponse)
        {
            ModbusSuccessResponse success = (ModbusSuccessResponse)response;
            success.GetData();//expect the values that was sent to the device to set.
            Console.WriteLine("received sync response for  ReadWriteMultipleRegisters");
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("ReadWriteMultipleRegisters: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new ReadWriteMultipleRegistersCallback());
    }

    private static void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new ReadWriteMultipleRegistersCallback());
    }

    private ModbusRequest CreateReadWriteMultipleRegistersRequest()
    {
        byte slaveId = 0xFF;
        ushort raddress = 9;
        ushort rquantity = 1;
        ushort waddress = 9;
        byte wquantity = 1;
        byte[] values = new byte[] { (byte)0x00, (byte)0xFF };
        var readWriteReg = new ReadWriteMultipleRegistersRequest(slaveId, raddress, rquantity, waddress, wquantity, true);
        readWriteReg.WriteValues = values;
        return readWriteReg;
    }
}
