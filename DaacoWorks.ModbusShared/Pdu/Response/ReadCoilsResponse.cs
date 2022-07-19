using Com.DaacoWorks.Modbus.Pdu.Request;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Response
{
    /// <summary>
    /// ReadCoilResponse is a success response for the request <see cref="ReadCoilsRequest"/> 
    /// </summary>
    public class ReadCoilsResponse : ModbusSuccessResponse {

        /// <summary>
        /// Instantiates a new modbus read coil response.
        /// </summary>
        /// <param name="requestPDU">request pdu</param>
        public ReadCoilsResponse(ModbusRequest requestPDU) : base(requestPDU)
        {
            
        }
        
        /// <summary>
        /// Gets the coil status.
        /// </summary>
        /// <returns>coil statuses</returns>
        public CoilState[] GetCoilStatus() {

            byte[] dataArr = responsePDU.GetDataInBytes();
            int quantity = requestPDU.Quantity;
            CoilState[] flags = new CoilState[quantity];
            int counter = 0;
            foreach (var data in dataArr) {
                byte temp = data;
                for (int i = 0; i < 8; i++) {
                    byte local = (byte)(temp & 0x01);
                    var flag = local == 0 ? CoilState.OFF : CoilState.ON;
                    flags[counter++] = flag;
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