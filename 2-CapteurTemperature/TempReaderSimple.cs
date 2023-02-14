using Iot.Device.Ds18b20;
using System.Threading;
using System;
using System.Diagnostics;

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
                    Debug.WriteLine("Can't read!");
                }
                else
                {
                    Debug.WriteLine($"Temperature: {currentTemperature.DegreesCelsius.ToString("F")}\u00B0C");
                }
                Thread.Sleep(2000);
            }
        }
    }
}
