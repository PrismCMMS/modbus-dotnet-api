using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Executor;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Clients;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.Threading.Tasks;

namespace Com.DaacoWorks.Modbus.Clients
{
    /// <summary>
    /// ModbusClient has the implementation of ModbusClient interface.
    /// </summary>
    public class ModbusClient : Protocol.Clients.Client, IModbusClient
    {
        private ConnectionParameters parameters;

        /// <summary>
        /// Instantiates ModbusClient
        /// </summary>
        /// <param name="parameters">connection parameters</param>
        public ModbusClient(ConnectionParameters parameters)
        {
            this.parameters = parameters;
        }

        ConnectionParameters GetModbusParams()
        {
            return parameters;
        }

        /// <summary>
        /// Initializes the ModbusClient by establishing the ModbusConnection instance
        /// </summary>
        public override void Init()
        {
            this.connection = new ModbusConnection(parameters);
        }

        private void ValidateRequest(ModbusRequest requestObject)
        {
            requestObject.Validate();
        }

        /// <summary>
        /// Submits a Modbus Request synchronously and returns a TaskFuture that allows to access respective response
        /// </summary>
        /// <param name="requestObject">modbus request</param>
        /// <returns>the TaskFuture</returns>
        public TaskFuture<ModbusRequest, Protocol.Model.Response, ModbusSuccessResponse, ModbusErrorResponse> Submit(ModbusRequest requestObject)
        {
            ValidateRequest(requestObject);
            TaskFuture<ModbusRequest, Protocol.Model.Response, ModbusSuccessResponse, ModbusErrorResponse> future = (new ModbusTaskFactory()).GetTaskFuture<Protocol.Model.Response>(requestObject, new TaskFutureCallBack<ModbusSuccessResponse, ModbusErrorResponse>() { }, connection);
            ExecutorFactory.GetGlobalRequestExecutor().Submit(future.GetTask());
            return future;
        }

        /// <summary>
        /// Submits Modbus Request asynchronously and calls the callback object's success or error methods when respective response is received
        /// </summary>
        /// <param name="requestObject">modbus request</param>
        /// <param name="callBack">IResponseCallback instance that can receive uccess/failure message</param>
        public void SubmitAsync(ModbusRequest requestObject, IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack)
        {
            ValidateRequest(requestObject);
            ExecutorFactory.GetGlobalRequestExecutor().Submit((new ModbusTaskFactory()).GetTask(requestObject, callBack, connection));
        }

        /// <summary>
        /// Submits Modbus Request asynchronously but on scheduled time. It submits the same request again and again on provided interval.
        /// It calls the callback object's success or error methods when respective response is received
        /// </summary>
        /// <param name="requestObject">modbus request</param>
        /// <param name="interval">periodic interval the request has to be submitted</param>
        /// <param name="callBack">IResponseCallback instance that can receive uccess/failure message</param>
        public void Schedule(ModbusRequest requestObject, TimeSpan interval, IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack)
        {
            ValidateRequest(requestObject);
            requestObject.IsScheduledRequest = true;
            var task = (new ModbusTaskFactory()).GetTask(requestObject, callBack, connection);
            ExecutorFactory.GetGlobalRequestExecutor().Schedule(task, TimeSpan.Zero, interval);
        }

        /// <summary>
        /// Shuts the connection established with peer
        /// </summary>
        public void Shutdown()
        {
            ClientFactory<ModbusClient, ModbusException>.RemoveClient(this);
            connection.Shutdown();
        }

        /// <summary>
        /// Gets hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return this.parameters.GetHashCode();
        }

        /// <summary>
        /// Checks if is equal
        /// </summary>
        /// <param name="obj">any object</param>
        /// <returns>true if object is same ModbusClient</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ModbusClient)) return false;
            ModbusClient client = (ModbusClient)obj;
            return client.GetModbusParams().Equals(this.parameters);
        }

    }
}