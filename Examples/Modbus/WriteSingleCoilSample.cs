
using System;
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;
/// <summary>
/// Sample class to submit the Modbus Request with Function Code 0x05 to write a coil.
/// </summary>
public class WriteSingleCoilSample
{

    public void TestWriteSingleCoil(ModbusClient client)
    {
        try
        {
            ModbusRequest request = WriteSingleCoilRequest();

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
            WriteSingleCoilResponse success = ((WriteSingleCoilResponse)response);

            var value = success.GetCoilState();//value that was sent to Modbus device is return as a success response by the device
            Console.WriteLine("received sync response for  WriteSingleCoil");

        }
        else if (response is ModbusErrorResponse)
        {
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            Console.WriteLine(string.Format("WriteSingleCoil: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new WriteSingleCoilCallback());
    }

    private static void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new WriteSingleCoilCallback());
    }

    private ModbusRequest WriteSingleCoilRequest()
    {
        byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. For other ModbusTypes 
                            //use valid slaveId of the Modbus device.
        ushort startAddress = 10; //address of the start register
        CoilState state = CoilState.ON; //to set coil status as ON
        bool convertToHex = true; //flag to say if the address to be converted to hex value

        return new WriteSingleCoilRequest(slaveId, startAddress, state, convertToHex);
    }

}
