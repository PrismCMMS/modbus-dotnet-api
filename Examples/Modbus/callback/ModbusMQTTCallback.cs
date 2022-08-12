
using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Modbus.Response.Callback;
using Paho.MqttDotnet;
using System;

public class ModbusMQTTCallback : IModbusResponseCallback
{
    /// <summary>
    /// Get the Paho Mqtt libraries by building the project from https://github.com/xljiulang/Paho.MqttDotnet
    /// </summary>
    MqttClient mqttClient = null;
    private string topic;

    public ModbusMQTTCallback(string topic, string brokerUrl)
    {
        this.topic = topic;
        try
        {
            mqttClient = new MqttClient(brokerUrl, "DotNetSample");
            var connOpts = new ConnectOption();
            connOpts.CleanSession = true;
            Console.WriteLine("Connecting to broker: " + brokerUrl);
            mqttClient.Connect(connOpts);
            Console.WriteLine("Connected");
        }
        catch (MqttException me)
        {
            Console.WriteLine(me);
        }
    }

    
    public void OnSuccess(ModbusSuccessResponse success)
    {
        try
        {
            float voltage = ModbusUtil.ToFloatValue(success.GetData(), false, false)[0];
            Console.WriteLine(" call :: VOLTAGE ------------> :: " + voltage);
            mqttClient.SendMessageAsync(topic, new MqttMessage(MqttQoS.ExactlyOnce, ("Voltage : " + voltage)));
        }
        catch (ModbusException e)
        {
            Console.WriteLine(e);
        }
        catch (MqttException e)
        {
            Console.WriteLine(e);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

   
    public void OnError(ModbusErrorResponse error)
    {
        int errorCode = error.GetErrorCode();
        string errorMessage = error.GetErrorMessage();
        Console.WriteLine(string.Format("MaskWriteRegister: {0}: {1}", errorCode, errorMessage));
    }
    
}
