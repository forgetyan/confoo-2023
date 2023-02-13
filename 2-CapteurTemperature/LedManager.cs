using System.Device.Gpio;
using System.Threading;

namespace CapteurTemp
{
    internal class LedManager
    {
        private static GpioPin _button;
        private GpioPin _led;

        public void Init()
        {
            var gpio = new GpioController();
            _led = gpio.OpenPin(32, PinMode.Output);
            _button = gpio.OpenPin(35, PinMode.Input);

            var thread = new Thread(BlinkLight);
            thread.Start();
        }

        private void BlinkLight()
        {
            while (true)
            {
                if (_button.Read() == PinValue.High)
                {
                    _led.Toggle();
                    Thread.Sleep(125);
                    _led.Toggle();
                    Thread.Sleep(125);
                    _led.Toggle();
                    Thread.Sleep(125);
                    _led.Toggle();
                }

                Thread.Sleep(525);
            }
        }
    }
}