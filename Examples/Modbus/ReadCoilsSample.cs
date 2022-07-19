
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

/// <summary>
/// Sample class to submit the Modbus Request with Function Code 0x01 to read the coil status. 
/// </summary>
public class ReadCoilsSample
{

    public void TestReadCoils(ModbusClient client)
    {
        try
        {
            ReadCoilsRequest request = CreateReadCoilsRequest();

            SyncCall(client, request); //sample code for making synchronized call
                                       //application wait for response after submitting the request

            AsyncCall(client, request); //sample code for making asynchronized call
                                        //application does not wait for response

            ScheduledCall(client, request); //application schedules a request. API will poll the device periodically for the same request		
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
        //submit the modbus request
        Response response = client.Submit(request).Get();

        if (response is ModbusSuccessResponse)
        {
            ReadCoilsResponse success = (ReadCoilsResponse)response;
            CoilState[] coilStatus = success.GetCoilStatus(); // status of each coil requested
            Console.WriteLine("received sync response for  ReadCoil");
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("ReadCoil: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ReadCoilsRequest request)
    {
        client.SubmitAsync(request, new ReadCoilResponseCallback());
    }

    private void ScheduledCall(ModbusClient client, ReadCoilsRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new ReadCoilResponseCallback());
    }

    private ReadCoilsRequest CreateReadCoilsRequest()
    {
        byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. For other ModbusTypes 
                            //use valid slaveId of the Modbus device.
        ushort startAddress = 102; //address of the start register
        ushort quantity = 2; // number of coils to read
        bool convertToHex = false;

        return new ReadCoilsRequest(slaveId, startAddress, quantity, convertToHex);
    }
}
