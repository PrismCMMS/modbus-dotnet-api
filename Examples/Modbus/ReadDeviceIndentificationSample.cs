
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

public class ReadDeviceIndentificationSample
{


    public void TestReadDeviceIndentification(ModbusClient client)
    {
        try
        {
            ReadDeviceIdentificationRequest request = CreateReadDeviceIndentificationRequest();

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

    private void SyncCall(ModbusClient client, ReadDeviceIdentificationRequest request)
    {
        //submit the modbus request
        Response response = client.Submit(request).Get();

        if (response is ModbusSuccessResponse)
        {
            ReadDeviceIdentificationResponse success = (ReadDeviceIdentificationResponse)response;
            DeviceInfo[] deviceInfo = success.GetDeviceInformation();
            Console.WriteLine("received sync response for  ReadDeviceIdentification");
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("ReadDeviceIdentification: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ReadDeviceIdentificationRequest request)
    {
        client.SubmitAsync(request, new ReadDeviceIndentificationCallback());
    }

    private void ScheduledCall(ModbusClient client, ReadDeviceIdentificationRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new ReadDeviceIndentificationCallback());
    }

    private ReadDeviceIdentificationRequest CreateReadDeviceIndentificationRequest()
    {
        byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. 
                           //For other ModbusTypes use valid slaveId of the Modbus device.

        return new ReadDeviceIdentificationRequest(slaveId, DeviceID.BASIC_DEVICE_IDENTIFICATION, 0);
    }


}
