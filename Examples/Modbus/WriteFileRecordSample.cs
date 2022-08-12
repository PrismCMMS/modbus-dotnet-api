

using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.Collections.Generic;

public class WriteFileRecordSample
{


    public void TestWriteFileRecord(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateWriteFileRecordRequest();

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
            var success = (WriteFileRecordResponse)response;
            FileRecordData[] fileRecs = success.GetFileRecords();
            Console.WriteLine("received sync response for  WriteFileRecord");
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("ReadFileRecord: {0}: {1}", errorCode, errorMessage));
            //handle the error
        }
    }

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new WriteFileRecordCallback());
    }

    private static void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new WriteFileRecordCallback());
    }

    private ModbusRequest CreateWriteFileRecordRequest()
    {
        FileRecordData[] fileRecords = GetFileRecords(2);
        WriteFileRecordRequest requestPDU = new WriteFileRecordRequest(0xFF, fileRecords);
        return requestPDU;
    }

    private FileRecordData[] GetFileRecords(int size)
    {
        List<FileRecordData> fileRecords = new List<FileRecordData>();
        for (ushort fileNum = 0; fileNum < size; fileNum++)
        {
            for (ushort recNum = 0; recNum < size * size; recNum++)
            {
                FileRecordData fileRecord = new FileRecordData(fileNum, recNum, new byte[] { 1, 1 });
                fileRecords.Add(fileRecord);
            }
        }
        return fileRecords.ToArray();
    }

}
