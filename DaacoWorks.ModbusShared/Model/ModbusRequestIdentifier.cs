using System;
using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Modbus.Model
{
    /// <summary>
    /// ModbusRequestIdentifier class represents unique identifier to identify a modbus request.
    /// </summary>
    public class ModbusRequestIdentifier : RequestIdentifier
    {

        private int requestId;
        private string host;
        private int functionCode;

        /// <summary>
        /// ModbusRequestIdentifier class represents unique identifier to identify a modbus request.
        /// </summary>
        /// <param name="host">the ost</param>
        /// <param name="functionCode">function code</param>
        /// <param name="requestId">request id</param>
        public ModbusRequestIdentifier(string host, int functionCode, int requestId)
        {
            this.host = host;
            this.functionCode = functionCode;
            this.requestId = requestId;
        }

        /// <summary>
        /// Gets the request Id
        /// </summary>
        /// <returns>request id</returns>
        public int GetRequestId()
        {
            return requestId;
        }        

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return requestId.GetHashCode();
        }

        /// <summary>
        /// Gets the host name
        /// </summary>
        /// <returns>host name</returns>
        public string GetHost()
        {
            return host;
        }        

        /// <summary>
        /// Checks if the objects are equal
        /// </summary>
        /// <param name="obj">any object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is ModbusRequestIdentifier) 
                && (this.requestId == ((ModbusRequestIdentifier)obj).GetRequestId())
                               && (this.GetFunctionCode() == ((ModbusRequestIdentifier)obj).GetFunctionCode())

                && (this.host.Equals(((ModbusRequestIdentifier)obj).GetHost()));

        }

        /// <summary>
        /// Serialize the object to a formatted string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}_{1}_{2}", host, functionCode, requestId);
        }

        /// <summary>
        /// Gets the function code
        /// </summary>
        /// <returns></returns>
        public int GetFunctionCode()
        {
            return functionCode;
        }
        
    }
}