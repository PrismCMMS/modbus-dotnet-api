using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Protocol.Model;
using DaacoWorks.Extension.ResponseHandlers;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace DaacoWorks.Extension
{
    public class ModbusRequestGenerator
    {
        private const string hostName = "Win7-PC";

        public static void SyncCall(ModbusClient client, ModbusRequest request)
        {
            request.RequestIdentifier = new CustomRequestIdentifier();            
            Response response = client.Submit(request).Get();
            ResponseHandler.GetResponseHandler().AddToQueue(response);
            
        }

        public static void AsyncCall(ModbusClient client, ReadCoilsRequest request)
        {
            request.RequestIdentifier = new CustomRequestIdentifier();
            client.SubmitAsync(request, new ModbusResponseCallback());            
        }

        public static void ScheduledCall(ModbusClient client, ReadCoilsRequest request)
        {
            request.RequestIdentifier = new CustomRequestIdentifier();
            client.Schedule(request, new TimeSpan(0, 0, 0, 5), new ModbusResponseCallback());
        }

        public static ReadCoilsRequest CreateReadCoilsRequest()
        {
            byte slaveId = 0xFF;//slaveId value is ignored in the case of Modbus TCP or UDP. For other ModbusTypes 
                                //use valid slaveId of the Modbus device.
            ushort startAddress = 102; //address of the start register
            ushort quantity = 4; // number of registers to read
            bool convertToHex = false;

            return new ReadCoilsRequest(slaveId, startAddress, quantity, convertToHex);
            
        }        

        public static ModbusClient GetModbusClient()
        {
            String hostname = hostName;
            int portnumber = 1601;
            ModbusSocketParameters connectionParams = new ModbusSocketParameters(hostname, portnumber, ModbusType.TCP);
            ModbusClient client = ModbusClientFactory.GetInstance().Create(connectionParams);
            return client;
        }
    }
}
