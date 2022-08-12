
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using System;

/// <summary>
/// Sample class to submit the Modbus Request with Function Code 0x04 to read the input register.
/// </summary>
public class ReadInputRegistersSample
{

    public void TestReadInputRegister(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateReadInputRegisterRequest();

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
            byte[] data = ((ModbusSuccessResponse)response).GetData();// raw bytes received from the Modbus device as a success response 
            Console.WriteLine("received sync response for  ReadInputRegisters");
            try
            {
                //use the utility methods to convert the raw bytes into meaningful data 
                // if the raw bytes to be converted as floats 
                float[] floatValues = ModbusUtil.ToFloatValue(data, false, true); //raw bytes, byteSwap, wordSwap 
                                                                                  // or if the raw bytes to be converted as integers 
                int[] intValues = ModbusUtil.ToIntValue(data, false, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else if (response is ModbusErrorResponse)
        {
            int errorCode = ((ModbusErrorResponse)response).GetErrorCode();
            string errorMessage = ((ModbusErrorResponse)response).GetErrorMessage();
            Console.WriteLine(string.Format("ReadInputRegisters: {0}: {1}", errorCode, errorMessage));
        }
    }

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new ReadInputRegisterCallback());
    }

    private void ScheduledCall(ModbusClient client, ModbusRequest request)
    {
        client.Schedule(request, new TimeSpan(0, 0, 5), new ReadInputRegisterCallback());
    }

    private ModbusRequest CreateReadInputRegisterRequest()
    {
        byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. For other ModbusTypes 
                            //use valid slaveId of the Modbus device.
        ushort startAddress = 102; //address of the start register
        ushort quantity = 2; // number of registers to read
        bool convertToHex = false;

        return new ReadInputRegistersRequest(slaveId, startAddress, quantity, convertToHex);
    }

}
