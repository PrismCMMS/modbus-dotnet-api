
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.Collections.Generic;

public class ReadFileRecordSample
{

    public void TestReadFileRecord(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateReadFileRecordRequest();

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
            ReadFileRecordResponse success = (ReadFileRecordResponse)response;
            FileRecordData[] fileRecs = success.GetFileRecords();
            Console.WriteLine("received sync response for  ReadFileRecord");
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
        client.SubmitAsync(request, new ReadFileRecordCallback());
    }

    private static void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new ReadFileRecordCallback());
    }

    private ModbusRequest CreateReadFileRecordRequest()
    {
        ushort numberOfFiles = 2; //recommended range is between 1 to 10 however modbus supports between 1 to 65535
        ushort numberOfFileRecords = 3; //1 to 10000 are valid range
        ushort sizeOfRecord = 4; //1 to 253 is valid range
        //prepare request to read 3 records (each of size 4 bytes) from 2 files 
        FileRecordData[] fileRecords = GetFileRecords(numberOfFiles, numberOfFileRecords, sizeOfRecord);
        var requestPDU = new ReadFileRecordRequest(0xFF, fileRecords);
        return requestPDU;
    }

    
    private FileRecordData[] GetFileRecords(ushort numberOfFiles, ushort numberOfFileRecords, ushort sizeOfRecord)
    {
        
        List<FileRecordData> fileRecords = new List<FileRecordData>();
        for (ushort fileNum = 0; fileNum < numberOfFiles; fileNum++)
        {
            for (ushort recNum = 0; recNum < numberOfFileRecords; recNum++)
            {
                FileRecordData fileRecord = new FileRecordData(fileNum, recNum, sizeOfRecord);
                fileRecords.Add(fileRecord);
            }
        }
        return fileRecords.ToArray();
    }
}
