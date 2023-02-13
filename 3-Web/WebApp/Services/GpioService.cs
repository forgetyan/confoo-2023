using System.Collections;
using System.Device.Gpio;
using Iot.Device.Ds18b20;
using nanoFramework.Device.OneWire;
using nanoFramework.Hardware.Esp32;
using UnitsNet;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class GpioService : IGpioService
    {
        private static GpioController _gpio;
        private static readonly Hashtable pins = new();
        private static OneWireHost _oneWireHost;
        private Ds18b20 _tempSensor;

        private bool _tempSensorInit;

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

        public void OpenPin(int pinNumber, PinMode mode)
        {
            if (pins.Contains(pinNumber))
            {
                return;
            }

            var pin = _gpio.OpenPin(pinNumber, mode);
            pins.Add(pinNumber, pin);
        }

        public bool TryReadTemperature(out Temperature temperature)
        {
            var result = _tempSensor.TryReadTemperature(out var currentTemperature);
            temperature = currentTemperature;
            return result;
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