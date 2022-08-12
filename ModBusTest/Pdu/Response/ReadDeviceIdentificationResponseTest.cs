

using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace ModBusTest.Pdu.Response
{

    [TestClass]
    public class ReadDeviceIdentificationResponseTest
    {

        [TestMethod]
        [TestCategory("Modbus\\ReadDeviceIdentification")]
        [Timeout(1000)]
        public void ReadDeviceIdentificationResponse_AddObjects()
        {

            var request = new ReadDeviceIdentificationRequest(1, DeviceID.BASIC_DEVICE_IDENTIFICATION, 1);
            var response = new ReadDeviceIdentificationResponse(request);

            List<DeviceInfo> devices = new List<DeviceInfo>();

            devices.Add(new DeviceInfo(1, new byte[1]));
            devices.Add(new DeviceInfo(1, new byte[2]));
            response.AddObjects(devices.ToArray());

            var actualDevices = response.GetDeviceInformation();

            for (var index = 0; index<devices.Count; index++)
            {
                Assert.AreSame(devices[index], actualDevices[index]);
            }

        }

    }
}
