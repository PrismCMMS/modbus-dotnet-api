namespace Com.DaacoWorks.Modbus.Model
{
    /// <summary>
    /// DeviceInfo class is a model class to represent object Id and its value. It is used to represent the responses
    /// received for the Modbus request Read Device Identification(function code 0x2B).
    /// </summary>
    public class DeviceInfo
    {

        private byte objectID;
        private byte[] objectValue;

        /// <summary>
        /// Instantiates a new device info.
        /// </summary>
        /// <param name="objectID">the object id</param>
        /// <param name="objectValue">the object value</param>
        public DeviceInfo(byte objectID, byte[] objectValue)
        {
            this.objectID = objectID;
            this.objectValue = objectValue;
        }

        /// <summary>
        /// Gets the object id
        /// </summary>
        /// <returns>the object id</returns>
        public byte GetObjectID()
        {
            return objectID;
        }

        /// <summary>
        /// Gets the object value
        /// </summary>
        /// <returns>the object value</returns>
        public byte[] GetObjectValue()
        {
            return objectValue;
        }

    }
}