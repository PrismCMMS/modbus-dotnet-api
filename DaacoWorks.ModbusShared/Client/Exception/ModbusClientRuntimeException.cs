namespace Com.DaacoWorks.Modbus.Client.Exception
{
    /// <summary>
    /// ModbusClientRuntimeException is Exception thrown by Modbus stack.
    /// </summary>
    public class ModbusClientRuntimeException : System.Exception
    {
        /// <summary>
        /// Instantiates a new modbus client runtime exception.
        /// </summary>
        /// <param name="message">error message</param>
        public ModbusClientRuntimeException(string message):base(message)
        {
            
        }

    }
}
