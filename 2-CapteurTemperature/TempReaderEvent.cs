using System;
using Iot.Device.Ds18b20;

namespace CapteurTemp
{
    internal class TempReaderEvent : ITempReader
    {
        private readonly Ds18b20 _tempSensor;

        public TempReaderEvent(Ds18b20 tempSensor)
        {
            _tempSensor = tempSensor;
        }

        public void ReadTemp()
        {
            _tempSensor.SensorValueChanged += currentTemperature =>
            {
                Console.WriteLine($"Temperature: {currentTemperature.DegreesCelsius.ToString("F")}\u00B0C");
            };
            _tempSensor.BeginTrackChanges(TimeSpan.FromMilliseconds(500));
        }
    }
}