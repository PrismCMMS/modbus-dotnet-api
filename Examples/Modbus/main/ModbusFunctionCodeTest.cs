using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

public class ModbusFunctionCodeTest
{

    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage : com.daacoworks.modbus.examplecode.main.ModbusFunctionCodeTest <hostName> <port> <functionCode>");
            return;
        }

        string hostName = args[0];

        if (!int.TryParse(args[1], out int portnumber))
        {
            Console.WriteLine("function code should be integer value");
        }

        if (!int.TryParse(args[2], out int functionCode))
        {
            Console.WriteLine("function code should be integer value");
        }

        Console.WriteLine("Connecting to host:{0} at port:{1} and requesting for function:{2}", hostName, portnumber, functionCode);

        ModbusClient client = null;
        try
        {
            client = GetModbusClient(hostName, portnumber);
            switch (functionCode)
            {
                case FunctionCodes.READ_COILS:
                    ReadCoilsSample readCoils = new ReadCoilsSample();
                    readCoils.TestReadCoils(client);
                    break;
                case FunctionCodes.READ_DISCRETE_INPUTS:
                    ReadDiscreteInputSample readDiscreteInput = new ReadDiscreteInputSample();
                    readDiscreteInput.TestReadDiscreteInput(client);
                    break;
                case FunctionCodes.READ_HOLDING_REGISTERS:
                    ReadHoldingRegisterSample readHoldingReg = new ReadHoldingRegisterSample();
                    readHoldingReg.TestReadHoldingRegister(client);
                    break;
                case FunctionCodes.READ_INPUT_REGISTERS:
                    ReadInputRegistersSample readInputReg = new ReadInputRegistersSample();
                    readInputReg.TestReadInputRegister(client);
                    break;
                case FunctionCodes.WRITE_SINGLE_COIL:
                    WriteSingleCoilSample writeSingleCoil = new WriteSingleCoilSample();
                    writeSingleCoil.TestWriteSingleCoil(client);
                    break;
                case FunctionCodes.WRITE_MULTIPLE_COILS:
                    WriteMultipleCoilsSample writeMultipleCoil = new WriteMultipleCoilsSample();
                    writeMultipleCoil.TestWriteMultipleCoil(client);
                    break;
                case FunctionCodes.WRITE_SINGLE_REGISTER:
                    WriteSingleRegisterSample writeSingleReg = new WriteSingleRegisterSample();
                    writeSingleReg.TestWriteSingleRegister(client);
                    break;
                case FunctionCodes.WRITE_MULTIPLE_REGISTERS:
                    WriteMultipleRegistersSample writeMultipleReg = new WriteMultipleRegistersSample();
                    writeMultipleReg.TestWriteSingleRegister(client);
                    break;
                case FunctionCodes.READ_FILE_RECORD:
                    ReadFileRecordSample readFileRecord = new ReadFileRecordSample();
                    readFileRecord.TestReadFileRecord(client);
                    break;
                case FunctionCodes.WRITE_FILE_RECORD:
                    WriteFileRecordSample writeFileRecord = new WriteFileRecordSample();
                    writeFileRecord.TestWriteFileRecord(client);
                    break;
                case FunctionCodes.MASK_WRITE_REGISTER:
                    MaskWriteRegisterSample maskWriteReg = new MaskWriteRegisterSample();
                    maskWriteReg.TestMaskriteRegister(client);
                    break;
                case FunctionCodes.READ_WRITE_MULTIPLE_REGISTERS:
                    ReadWriteMultipleRegistersSample readWriteMulReg = new ReadWriteMultipleRegistersSample();
                    readWriteMulReg.TestReadWriteMultipleRegisters(client);
                    break;
                case FunctionCodes.READ_FIFO_QUEUE:
                    ReadFIFOQueueSample fifoRequest = new ReadFIFOQueueSample();
                    fifoRequest.TestFIFORequest(client);
                    break;
                case FunctionCodes.ENCAPSULATED_INTERFACE_TRANSPORT:
                    ReadDeviceIndentificationSample readDevId = new ReadDeviceIndentificationSample();
                    readDevId.TestReadDeviceIndentification(client);
                    break;
                default:
                    Console.WriteLine("Invalid or unsupported function code");
                    break;
            }
        }
        catch (ModbusException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            if (client != null)
                client.Shutdown();
        }

        
    }

    private static ModbusClient GetModbusClient(string hostName, int portnumber)
    {

        ModbusSocketParameters connectionParams = new ModbusSocketParameters(hostName, portnumber, ModbusType.TCP);
        ModbusClient client = ModbusClientFactory.GetInstance().Create(connectionParams);
        return client;
    }

}
