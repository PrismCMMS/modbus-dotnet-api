using Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers
{
    public static class ResponseProcessorFactory
    {
        public static IResponseProcessor CreateResponseProcessor()
        {
            var responseProcessor = new GenericModbusResponseProcessor();
            responseProcessor.AddResponseProcessor(new MaskWriteRegisterProcessor());
            responseProcessor.AddResponseProcessor(new ReadDeviceIdentificationProcessor());
            responseProcessor.AddResponseProcessor(new ReadDiscreteInputsProcessor());
            responseProcessor.AddResponseProcessor(new ReadFileRecordProcessor());
            responseProcessor.AddResponseProcessor(new ReadWriteMultipleRegistersProcessor());
            responseProcessor.AddResponseProcessor(new WriteFileRecordProcessor());
            responseProcessor.AddResponseProcessor(new WriteMultipleCoilsProcessor());
            responseProcessor.AddResponseProcessor(new WriteMultipleRegistersProcessor());
            responseProcessor.AddResponseProcessor(new WriteSingleCoilProcessor());
            responseProcessor.AddResponseProcessor(new WriteSingleRegisterProcessor());
            return responseProcessor;
        }
    }
}
