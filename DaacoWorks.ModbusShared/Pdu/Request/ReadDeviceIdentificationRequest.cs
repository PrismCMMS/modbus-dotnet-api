using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using System.IO;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Request
{

    /// <summary>
    /// ReadDeviceIdentification class is used to read the identification and additional information relative to the 
    /// physical and functional description of a remote device.
    /// </summary>
    public class ReadDeviceIdentificationRequest : ModbusRequest
    {

        /// <summary>
        /// ReadDeviceIdentification class is used to read the identification and additional information relative to the 
        /// physical and functional description of a remote device.
        /// </summary>
        /// <param name="slaveId">slave id</param>
        /// <param name="deviceId">device id</param>
        /// <param name="objectId">object id</param>
        public ReadDeviceIdentificationRequest(byte slaveId, DeviceID deviceId, byte objectId) : base(slaveId, 0, 0, true)
        {

            this.DeviceId = deviceId;
            this.ObjectId = objectId;
        }

        /// <summary>
        /// Gets function code
        /// </summary>
        /// <returns>function code</returns>
        public override byte GetFunctionCode()
        {
            return FunctionCodes.ENCAPSULATED_INTERFACE_TRANSPORT;
        }

        /// <summary>
        /// Gets data in bytes
        /// </summary>
        /// <returns>data in bytes</returns>
        public override byte[] GetDataInBytes()
        {
            using (var buffer = new MemoryStream(4))
            {
                using (var binaryWrite = new BinaryWriter(buffer))
                {
                    binaryWrite.Write(GetFunctionCode());
                    binaryWrite.Write(GetHexByteArray(0x0E, 1));
                    binaryWrite.Write(GetHexByteArray((int)DeviceId, 1));
                    binaryWrite.Write(GetHexByteArray(ObjectId, 1));
                    return buffer.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets/sets object id
        /// </summary>
        public byte ObjectId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets device id
        /// </summary>
        public DeviceID DeviceId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets length
        /// </summary>
        /// <returns>length</returns>
        public override int GetLength()
        {
            return 4;
        }

        /// <summary>
        /// Validate request
        /// </summary>
        public override void Validate()
        {
            if (ObjectId > 255)
            {
                throw new ModbusException(ModbusErrorCodes.INVALID_INPUT, ModbusErrorCodes.INVALID_OBJECTID);
            }
        }

    }
}