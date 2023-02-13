using System.Device.Gpio;

namespace WebApp.Interfaces
{
    public interface IGpioService
    {
        void InitTemperatureSensor();
        bool TryReadTemperature(out UnitsNet.Temperature temperature);
        void OpenPin(int pinNumber, PinMode mode);
        void Write(int pinNumber, PinValue value);
    }
}