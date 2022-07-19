using System;
using Com.DaacoWorks.Modbus.Codec;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Executor
{
    /// <summary>
    /// ModbusConnection class represents the socket connection established with a Modbus Server.
    /// </summary>
    public class ModbusConnection : Connection<ModbusPDUWrapper, Protocol.Model.Response>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusConnection).FullName);

        private int requestId = 0;
        private ModbusType type;
        private IResponseHandler respHandler;
        private IEncoder<ModbusPDUWrapper> encoder;
        private IDecoder<Protocol.Model.Response> decoder;

        /// <summary>
        /// Instantiates a new modbus connection.
        /// </summary>
        /// <param name="parameters">connection parameters</param>
        public ModbusConnection(ConnectionParameters parameters) : base(parameters)
        {
            this.type = GetModbusType(parameters);
            decoder = ModbusCodecFactory.GetDecoder(type);
            respHandler = new ChannelInboundHandler<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse>(parameters, decoder);
            encoder = ModbusCodecFactory.GetEncoder(type);
        }

        private ModbusType GetModbusType(ConnectionParameters parameters)
        {
            if (parameters is ModbusSocketParameters)
            {
                ModbusSocketParameters sockParam = (ModbusSocketParameters)parameters;
                return sockParam.GetModbusType();
            }
            else
            {
                switch (parameters.GetConnectionType())
                {
                    case ConnectionType.TCP:
                        return ModbusType.TCP;
                    case ConnectionType.UDP:
                        return ModbusType.UDP;
                    default:
                        return ModbusType.UNKNOWN;
                }
            }
        }

        internal ModbusRequestIdentifier GetNextRequestId(byte functionCode, byte slaveId)
        {
            return GetId(functionCode, slaveId);
        }

        private ModbusRequestIdentifier GetId(byte functionCode, byte slaveId)
        {
            switch (type)
            {
                case ModbusType.TCP:
                case ModbusType.UDP:
                    return requestId > 0x0000FFFF ? new ModbusRequestIdentifier(GetConnectionParameters().GetHost(), functionCode, 0) : new ModbusRequestIdentifier(GetConnectionParameters().GetHost(), functionCode, ++requestId);
                case ModbusType.RTU_OVER_TCP:
                case ModbusType.RTU_OVER_UDP:
                case ModbusType.ASCII_OVER_TCP:
                case ModbusType.ASCII_OVER_UDP:
                    return new ModbusRequestIdentifier(GetConnectionParameters().GetHost(), functionCode, slaveId);
            }
            return null;
        }

        /// <summary>
        /// Gets response handler
        /// </summary>
        /// <returns></returns>
        protected override IResponseHandler ResponseHandler()
        {
            return respHandler;
        }

        /// <summary>
        /// Gets Encoder
        /// </summary>
        /// <returns></returns>
        protected override IEncoder<ModbusPDUWrapper> GetEncoder()
        {
            return encoder;
        }

        /// <summary>
        /// Gets read buffer size
        /// </summary>
        /// <returns></returns>
        protected override int GetReadBufferSize()
        {
            return 260;
        }

        /// <summary>
        /// Gets write buffer size
        /// </summary>
        /// <returns></returns>
        protected override int GetWriteBufferSize()
        {
            return 260;
        }

        /// <summary>
        /// Gets Modbus type
        /// </summary>
        /// <returns></returns>
        public ModbusType GetModbusType()
        {
            return type;
        }
    }
}