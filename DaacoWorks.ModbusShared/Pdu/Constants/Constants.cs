namespace Com.DaacoWorks.Modbus.Pdu.Constants
{
    /// <summary>
    /// Constants class to provide constant values for other Modbus classes.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Represents all modbus types
        /// </summary>
        public enum ModbusType
        {
            /// <summary>
            /// unknown 
            /// </summary>
            UNKNOWN = 0,
            /// <summary>
            /// TCP
            /// </summary>
            TCP = 1,
            /// <summary>
            /// UDP
            /// </summary>
            UDP = 2,
            /// <summary>
            /// RTU over TCP to send over serial network such as RS-485
            /// </summary>
            RTU_OVER_TCP = 3,
            /// <summary>
            /// RTU over UDP to send over serial network such as RS-485
            /// </summary>
            RTU_OVER_UDP = 4,
            /// <summary>
            /// ASCII data over TCP to 
            /// </summary>
            ASCII_OVER_TCP = 5,
            /// <summary>
            /// ASCII data over UDP
            /// </summary>
            ASCII_OVER_UDP = 6
            //ENRON("5"),
            //LUFKIN("6");	

        }

        /// <summary>
        /// Represents coil state
        /// </summary>
        public enum CoilState
        {
            /// <summary>
            /// Coil Off state
            /// </summary>
            OFF = 0x0000,
            /// <summary>
            /// Coil On state
            /// </summary>
            ON = 0xFF00
        }

        /// <summary>
        /// Represents device id
        /// </summary>
        public enum DeviceID
        {
            /// <summary>
            /// Basic device identification
            /// </summary>
            BASIC_DEVICE_IDENTIFICATION = 1,
            /// <summary>
            /// Regular device identification
            /// </summary>
            REGULAR_DEVICE_IDENTIFICATION = 2,
            /// <summary>
            /// Extended device identification
            /// </summary>
            EXTENDED_DEVICE_IDENTIFICATION = 3,
            /// <summary>
            /// Specific device identification
            /// </summary>
            SPECIFIC_DEVICE_IDENTIFICATION = 4
        }

    }
}