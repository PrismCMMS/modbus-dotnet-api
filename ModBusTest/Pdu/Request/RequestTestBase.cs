using Com.DaacoWorks.Modbus.Clients;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu;
using System;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.DaacoWorks.Modbus.Pdu.Util;

namespace ModBusTest.Pdu.Request
{
    [TestClass]
    public class RequestTestBase
    {

        protected IModbusClient client;
        protected ModbusRequest requestPDU;

        [TestInitialize]
        public void BeforeEachTestMethod()
        {
            client = ModbusClientFactory.GetInstance().Create(new ModbusSocketParameters("win7-PC", 1601, ModbusType.TCP)); 
            //client = ModbusClientFactory.GetInstance().Create(new ModbusSocketParameters("192.168.0.7", 1502, ModbusType.TCP));
        }

        [TestCleanup]
        public void AfterEachTestMethod()
        {
            client.Shutdown();
        }

        public int GetInt(byte[] data)
        {
            return ModbusUtil.ToInt32(data,0);
        }



    }
}