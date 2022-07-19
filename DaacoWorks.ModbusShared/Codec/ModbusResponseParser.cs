using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// ModbusResponseParser class is used to parse the input buffer and converts it into ModbusResponse.
    /// </summary>
    internal class ModbusResponseParser
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusResponseParser).FullName);

        /// <summary>
        /// Parses the modbus response PDU.
        /// </summary>
        /// <param name="requestId">the request id</param>
        /// <param name="pduLength">pdu length</param>
        /// <param name="functionCode">the function code</param>
        /// <param name="input">the input</param>
        /// <returns>the response</returns>
        internal Protocol.Model.Response ParseModbusResponsePDU(RequestIdentifier requestId, int pduLength, int functionCode, MemoryStream input)
        {


            using (var binaryReader = new BinaryReader(input))
            {
                var requestPDU = RequestMap<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse>.GetInstance().GetRequestPDU(requestId);

                if (functionCode <= 127) // success response
                {

                    requestPDU.RequestIdentifier = requestId;

                    if (functionCode <= FunctionCodes.READ_COILS)
                    {
                        return SetReadResponse((byte)functionCode, pduLength, requestId, input, new ReadCoilsResponse(requestPDU));
                    }
                    else if (functionCode <= FunctionCodes.READ_DISCRETE_INPUTS)
                    {
                        return SetReadResponse((byte)functionCode, pduLength, requestId, input, new ReadDiscreteInputsResponse(requestPDU));
                    }
                    else if (functionCode <= FunctionCodes.READ_HOLDING_REGISTERS)
                    {
                        return SetReadResponse((byte)functionCode, pduLength, requestId, input, new ReadHoldingRegistersResponse(requestPDU));
                    }
                    else if (functionCode <= FunctionCodes.READ_INPUT_REGISTERS)
                    {
                        return SetReadResponse((byte)functionCode, pduLength, requestId, input, new ReadInputRegistersResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.WRITE_SINGLE_COIL)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new WriteSingleCoilResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.WRITE_MULTIPLE_COILS)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new WriteMultipleCoilsResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.WRITE_SINGLE_REGISTER)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new WriteSingleRegisterResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.WRITE_MULTIPLE_REGISTERS)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new WriteMultipleRegistersResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.READ_FILE_RECORD)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new ReadFileRecordResponse(requestPDU));
                    }
                    else if(functionCode == FunctionCodes.WRITE_FILE_RECORD)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new WriteFileRecordResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.MASK_WRITE_REGISTER)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new MaskWriteRegisterResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.READ_WRITE_MULTIPLE_REGISTERS)
                    {
                        return SetReadResponse((byte)functionCode, pduLength, requestId, input, new ReadWriteMultipleRegistersResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.READ_FIFO_QUEUE)
                    {
                        binaryReader.ReadInt16(); //ignore byte count field
                        binaryReader.ReadInt16(); //reading FIFO count value
                        return SetWriteResponse((byte)functionCode, new byte[pduLength - 4], requestId, input, new ReadFIFOQueueResponse(requestPDU));
                    }
                    else if (functionCode == FunctionCodes.ENCAPSULATED_INTERFACE_TRANSPORT)
                    {
                        return SetWriteResponse((byte)functionCode, new byte[pduLength], requestId, input, new ModbusSuccessResponse(requestPDU));
                    }

                }
                else
                { //error response because error response will have function code + 128 (0x80) as function code

                    byte[] data = new byte[] { (byte)input.ReadByte() };

                    ModbusResponse pdu = new ModbusResponse((byte)functionCode, data);
                    ModbusErrorResponse response = new ModbusErrorResponse();
                    response.Request = requestPDU;
                    response.SetResponsePDU(pdu);
                    return response;
                }

                return null;
            }
        }

        private Protocol.Model.Response SetReadResponse(byte functionCode, int length, RequestIdentifier requestId, MemoryStream input, Protocol.Model.Response response)
        {
            int byteCount = input.ReadByte() & 0xFF;
            logger.Info("Client Decode byteCount " + byteCount);
            byte[] data = new byte[length - 1];
            input.Read(data);
            ModbusResponse pdu = new ModbusResponse(functionCode, data);
            ((ModbusSuccessResponse)response).SetResponsePDU(pdu);
            return response;
        }

        private Protocol.Model.Response SetWriteResponse(byte functionCode, byte[] data, RequestIdentifier requestId, MemoryStream input, Protocol.Model.Response response)
        {
            input.Read(data);
            ModbusResponse pdu = new ModbusResponse(functionCode, data);
            ((ModbusSuccessResponse)response).SetResponsePDU(pdu);

            return response;
        }

    }

}