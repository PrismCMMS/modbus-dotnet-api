using Com.DaacoWorks.Modbus.Model;
using System.Collections.Generic;
using Com.DaacoWorks.Modbus.Pdu.Request;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadDeviceIdentificationResponse is a success response for the request <see cref="ReadDeviceIdentificationRequest"/> 
    /// </summary>
    public class ReadDeviceIdentificationResponse : ModbusSuccessResponse
    {

        private List<DeviceInfo> deviceInfos = new List<DeviceInfo>(10);

        /// <summary>
        /// Creates instance of ModbusReadDeviceIdentificationResponse
        /// </summary>
        /// <param name="requestPDU"></param>
        public ReadDeviceIdentificationResponse(ModbusRequest requestPDU) : base(requestPDU)
        {
        }

        /// <summary>
        /// Adds the objects.
        /// </summary>
        /// <param name="objects">objects</param>
        public void AddObjects(DeviceInfo[] objects)
        {
            deviceInfos.AddRange(objects);
        }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <returns>objects</returns>
        public DeviceInfo[] GetDeviceInformation()
        {
            return deviceInfos.ToArray();
        }

    }
}