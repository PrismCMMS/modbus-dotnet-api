using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Protocol.Model;
using Daacoworks.Extension.ResponseHandlers;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DaacoWorks.Extension.ResponseHandlers
{

    public class ResponseHandler
    {
        private const string HANDLER_SHUTDOWN = "ResponseHandler is already shutdown";
        
        private readonly ConcurrentQueue<ResponseWrapper> responseQueue;
        private Task runningTask = null;
        private object syncObject = new Object();
        private static Lazy<ResponseHandler> responseHandler = new Lazy<ResponseHandler>();

        private IResponseProcessor modbusResponseProcessor;
        private bool isShutDown;

        private ResponseHandler()
        {
            responseQueue = new ConcurrentQueue<ResponseWrapper>();
            modbusResponseProcessor = ResponseProcessorFactory.CreateResponseProcessor();
        }

        /// <summary>
        /// Gets a singleton instance of Response handler
        /// </summary>
        /// <returns></returns>
        public static ResponseHandler GetResponseHandler()
        {
            return responseHandler.Value;
        }
        
        public void AddToQueue(Response response)
        {
            if (isShutDown) { throw new Exception(HANDLER_SHUTDOWN); }            

            var responseWrapper = new ResponseWrapper(response, DateTime.Now);
            responseQueue.Enqueue(responseWrapper);
            if (runningTask == null || runningTask.IsCompleted) //this check is to avoid unnecessary locks
            {
                lock (syncObject) //lock when we have to start a new queue processing task
                {
                    if (runningTask == null || runningTask.IsCompleted) //double check to ensure that any parallel thread has not started any task
                        runningTask = Task.Factory.StartNew(() => ProcessQueue()); //Start processing the queue items in a different thread
                }
            }
        }

        private void ProcessQueue()
        {
            ResponseWrapper responseWithTime;
            while (responseQueue.TryDequeue(out responseWithTime))
            {
                try
                {
                    //check whether received response is valid Modbus Response
                    if (responseWithTime.Response is ModbusSuccessResponse || responseWithTime.Response is ModbusErrorResponse)
                    {
                        if(!modbusResponseProcessor.ProcessResponse(responseWithTime))
                        {
                            Console.WriteLine("Response Processor to handle this response is missing");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown response received");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public void Shutdown()
        {
            isShutDown = true;

            if (runningTask != null)
            {
                runningTask.Wait();
                runningTask = null;
            }

        }

    }
}
