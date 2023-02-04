using Iot.Device.Ds18b20;
using System.Threading;
using System;

namespace CapteurTemp
{
    internal class TempReaderSimple : ITempReader
    {
        private readonly Ds18b20 _tempSensor;

        public TempReaderSimple(Ds18b20 tempSensor)
        {
            _tempSensor = tempSensor;
        }

        public void ReadTemp()
        {
            while (true)
            {
                if (!_tempSensor.TryReadTemperature(out var currentTemperature))
                {
                    Console.WriteLine("Can't read!");
                }
                else
                {
                    Console.WriteLine($"Temperature: {currentTemperature.DegreesCelsius.ToString("F")}\u00B0C");
                }
                Thread.Sleep(2000);
            }
        }
    }
}
