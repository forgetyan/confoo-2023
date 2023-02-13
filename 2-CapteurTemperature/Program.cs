using System.Threading;
using Iot.Device.Ds18b20;
using nanoFramework.Device.OneWire;
using nanoFramework.Hardware.Esp32;

namespace CapteurTemp
{
    public class Program
    {
        public static void Main()
        {
            // Doc sur le thermomètre Ds18b20
            // https://docs.nanoframework.net/devicesdetails/Ds18b20/README.html
            var ledManager = new LedManager();
            ledManager.Init();
            Configuration.SetPinFunction(16, DeviceFunction.COM3_RX);
            Configuration.SetPinFunction(17, DeviceFunction.COM3_TX);

            OneWireHost oneWire = new OneWireHost();
            Ds18b20 tempSensor = new Ds18b20(oneWire);

            ITempReader tempReader = new TempReaderSimple(tempSensor);
            //ITempReader tempReader = new TempReaderEvent(tempSensor);

            
            if (tempSensor.Initialize())
            {
                tempReader.ReadTemp();
                Thread.Sleep(Timeout.Infinite);
            }
            
            oneWire.Dispose();
        }
    }
}
