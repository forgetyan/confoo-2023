using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace HelloWorld
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Bienvenue dans le nanoFramework!");
            var gpio = new GpioController();
            GpioPin led = gpio.OpenPin(32, PinMode.Output);
            while (true)
            {
                led.Toggle();
                Thread.Sleep(125);
                led.Toggle();
                Thread.Sleep(125);
                led.Toggle();
                Thread.Sleep(125);
                led.Toggle();
                Thread.Sleep(525);
            }
        }
    }
}
