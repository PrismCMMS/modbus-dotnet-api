namespace Com.DaacoWorks.Modbus.Client.Exception {

    /// <summary>
    /// ModbusException class used by Modbus stack to throw exception.
    /// </summary>
    public class ModbusException : System.Exception {

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode {
            get;
            private set;
        }

        /// <summary>
        /// Instantiates a new modbus exception.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        public ModbusException(int errorCode, string errorDescription) :base(errorDescription)
        {
            this.ErrorCode = errorCode;           

        }

    }
}