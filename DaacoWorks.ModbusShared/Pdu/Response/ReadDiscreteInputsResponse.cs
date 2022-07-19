using Com.DaacoWorks.Modbus.Pdu.Request;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{

    /// <summary>
    /// ReadDiscreteInputResponse is a success response for the request <see cref="ReadDiscreteInputsRequest"/> 
    /// </summary>
    public class ReadDiscreteInputsResponse : ModbusSuccessResponse {

        /// <summary>
        /// Instantiates a new modbus read discrete input response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public ReadDiscreteInputsResponse(ModbusRequest requestPDU): base(requestPDU)
        {
            
        }

        /// <summary>
        /// Gets the discrete input status.
        /// </summary>
        /// <returns>the discrete input status</returns>
        public CoilState[] GetDiscreteInputStatus() {

            byte[] dataArr = responsePDU.GetDataInBytes();
            int quantity = requestPDU.Quantity;
            var flags = new CoilState[quantity];
            int counter = 0;            
            foreach ( var data in dataArr) {
                byte temp = data;
                for (int i = 0; i < 8; i++) {
                    byte local = (byte)(temp & 0x01);
                    var flag = local == 0 ? CoilState.OFF : CoilState.ON;
                    flags[counter++]=flag;
                    temp = (byte)(temp >> 0x01);
                    if (counter == quantity) {
                        return flags;
                    }
                }
            }
            return flags;
        }

    }
}