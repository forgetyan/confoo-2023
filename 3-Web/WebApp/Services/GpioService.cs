using System;
using System.Collections;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using Iot.Device.Ds18b20;
using nanoFramework.Device.OneWire;
using nanoFramework.Hardware.Esp32;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class GpioService : IGpioService
    {
        private static GpioController _gpio;
        private static Hashtable pins = new Hashtable();

        private bool _tempSensorInit = false;
        private static OneWireHost _oneWireHost;
        Ds18b20 _tempSensor;

        public GpioService()
        {
            if (_gpio == null)
            {
                _gpio = new GpioController();
            }
        }

        public void InitTemperatureSensor()
        {
            if (!_tempSensorInit)
            {
                Configuration.SetPinFunction(16, DeviceFunction.COM3_RX);
                Configuration.SetPinFunction(17, DeviceFunction.COM3_TX);
                _oneWireHost = new OneWireHost();
                _tempSensor = new Ds18b20(_oneWireHost);
                _tempSensor.Initialize();
                _tempSensorInit = true;
            }
        }

        public bool TryReadTemperature(out UnitsNet.Temperature temperature)
        {
            var result = _tempSensor.TryReadTemperature(out var currentTemperature);
            temperature = currentTemperature;
            return result;
        }

        public void OpenPin(int pinNumber, PinMode mode)
        {
            if (pins.Contains(pinNumber))
            {
                return;
            }

            var pin = _gpio.OpenPin(pinNumber, mode);
            pins.Add(pinNumber, pin);
        }

        public void Write(int pinNumber, PinValue value)
        {
            if (pins.Contains(pinNumber))
            {
                var pin = pins[pinNumber] as GpioPin;
                pin?.Write(value);
            }
        }

    }
}