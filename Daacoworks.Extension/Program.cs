
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Pdu.Request;
using DaacoWorks.Extension.ResponseHandlers;
using System;

namespace DaacoWorks.Extension
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ModbusClient client = ModbusRequestGenerator.GetModbusClient();

                ReadCoilsRequest request = ModbusRequestGenerator.CreateReadCoilsRequest();
               

                ModbusRequestGenerator.SyncCall(client, request); //sample code for making synchronized call
                                                                  //application wait for response after submitting the request

                ModbusRequestGenerator.AsyncCall(client, request); //sample code for making asynchronized call
                                                                   //application does not wait for response

                ModbusRequestGenerator.ScheduledCall(client, request);

                Console.ReadKey();

                client.Shutdown();
                ResponseHandler.GetResponseHandler().Shutdown();

            }
            catch (ModbusException e)
            {
                //handle the exception
                Console.Write(e);
            }
            catch (Exception e)
            {
                //handle the exception
                Console.Write(e);
            }

            
        }
    }
}
