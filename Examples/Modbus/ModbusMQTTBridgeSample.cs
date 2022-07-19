
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using System;

public class ModbusMQTTBridgeSample
{
    public void TestReadHoldingRegister(ModbusClient client)
    {
        try
        {
            ModbusRequest request = CreateReadHoldingRegisterRequest();

            AsyncCall(client, request); //sample code for making asynchronized call
                                        //application does not wait for response


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

    private void AsyncCall(ModbusClient client, ModbusRequest request)
    {
        client.SubmitAsync(request, new ModbusMQTTCallback("TestDaacoworks", "tcp://test.mosquitto.org:1883"));
    }


    private ModbusRequest CreateReadHoldingRegisterRequest()
    {
        byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. For other ModbusTypes 
                            //use valid slaveId of the Modbus device.
        ushort startAddress = 102; //address of the start register
        ushort quantity = 2; // number of registers to read
        bool convertToHex = false;

        return new ReadHoldingRegistersRequest(slaveId, startAddress, quantity, convertToHex);
    }
}
