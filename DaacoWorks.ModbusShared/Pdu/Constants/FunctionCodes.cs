namespace Com.DaacoWorks.Modbus.Pdu.Constants {

    /// <summary>
    /// FunctionCodes has the function codes mentioned in Modbus specification v1.1b. 
    /// </summary>
    public class FunctionCodes {

        /// <summary>
        /// Read coils
        /// </summary>
        public const byte READ_COILS = 0x01;
        /// <summary>
        /// Read Discrete inputs
        /// </summary>
        public const byte READ_DISCRETE_INPUTS = 0x02;
        /// <summary>
        /// Read holding registers
        /// </summary>
        public const byte READ_HOLDING_REGISTERS = 0x03;
        /// <summary>
        /// Read input registers
        /// </summary>
        public const byte READ_INPUT_REGISTERS = 0x04;
        /// <summary>
        /// Write single coil
        /// </summary>
        public const byte WRITE_SINGLE_COIL = 0x05;
        /// <summary>
        /// write single register
        /// </summary>
        public const byte WRITE_SINGLE_REGISTER = 0x06;
        /// <summary>
        /// write multiple coils
        /// </summary>
        public const byte WRITE_MULTIPLE_COILS = 0x0F;
        /// <summary>
        /// write multiple registers
        /// </summary>
        public const byte WRITE_MULTIPLE_REGISTERS = 0x10;
        /// <summary>
        /// read file record
        /// </summary>
        public const byte READ_FILE_RECORD = 0x14;
        /// <summary>
        /// write file record
        /// </summary>
        public const byte WRITE_FILE_RECORD = 0x15;
        /// <summary>
        ///  mask write registers
        /// </summary>
        public const byte MASK_WRITE_REGISTER = 0x16;
        /// <summary>
        /// mask read write multiple registers
        /// </summary>
        public const byte READ_WRITE_MULTIPLE_REGISTERS = 0x17;
        /// <summary>
        /// Read FIFO queue
        /// </summary>
        public const byte READ_FIFO_QUEUE = 0x18;
        /// <summary>
        /// Encapsulated Interface Transport
        /// </summary>
        public const byte ENCAPSULATED_INTERFACE_TRANSPORT = 0x2B; 

    }
}