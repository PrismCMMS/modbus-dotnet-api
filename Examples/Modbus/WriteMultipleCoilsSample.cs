

using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using System;

public class WriteMultipleCoilsSample
{

    public void TestWriteMultipleCoil(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateWriteMultipleCoilRequest();

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
            WriteMultipleCoilsResponse success = ((WriteMultipleCoilsResponse)response);
            var value = success.GetQuantity();//value that was sent to Modbus device is return as a success response by the device
            Console.WriteLine("received sync response for  WriteMultipleCoils");
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("WriteMultipleCoils: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new WriteMultipleCoilsCallback());
    }

    private static void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new WriteMultipleCoilsCallback());
    }

    private ModbusRequest CreateWriteMultipleCoilRequest()
    {
        byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. For other ModbusTypes 
                            //use valid slaveId of the Modbus device.
        ushort startAddress = 102; //address of the start register
        ushort quantity = 2; // number of coils to write
        bool convertToHex = false; //flag to say if the address to be converted to hex value

        var request = new WriteMultipleCoilsRequest(slaveId, startAddress, quantity, convertToHex);
        byte[] outputValue = new byte[] { (byte)0xFF };
        request.WriteValues = outputValue;
        return request;
    }

}
