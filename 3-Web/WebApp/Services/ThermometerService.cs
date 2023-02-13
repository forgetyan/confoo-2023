using System.Diagnostics;
using System.Threading;
using WebApp.Interfaces;
using WebApp.Model;

namespace WebApp.Services
{
    public class ThermometerService : BaseProcess, IThermometerService
    {
        private readonly IGpioService _gpioService;

        public ThermometerService(IThreadService threadService, IGpioService gpioService) : base(threadService)
        {
            _gpioService = gpioService;
        }

        public double Temperature { get; set; }

        public override void Init()
        {
            _gpioService.InitTemperatureSensor();
        }

        public override void Loop()
        {
            if (!_gpioService.TryReadTemperature(out var currentTemperature))
            {
                Debug.WriteLine("Can't read!");
            }
            else
            {
                Temperature = currentTemperature.DegreesCelsius;
                Debug.WriteLine($"Temperature: {Temperature.ToString("F")}\u00B0C");
            }

            Thread.Sleep(1000);
        }

        public Temperature GetTemperatureModel()
        {
            return new Temperature
            {
                IsActive = IsOn,
                TemperatureCelcius = Temperature,
                AlarmHigh = Temperature > AppSettings.AlarmTempHigh,
                AlarmLow = Temperature < AppSettings.AlarmTempLow
            };
        }
    }
}