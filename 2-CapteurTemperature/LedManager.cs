using nanoFramework.Hardware.Esp32;
using System.Device.Gpio;
using System.Threading;

namespace CapteurTemp
{
    internal class LedManager
    {
        private GpioPin _led;
        public void Init()
        {
            var gpio = new GpioController();
            _led = gpio.OpenPin(5, PinMode.Output);
            var thread = new Thread(BlinkLight);
            thread.Start();
        }

        private void BlinkLight()
        {
            while (true)
            {
                _led.Toggle();
                Thread.Sleep(125);
                _led.Toggle();
                Thread.Sleep(125);
                _led.Toggle();
                Thread.Sleep(125);
                _led.Toggle();
                Thread.Sleep(525);
            }
        }
    }
}
