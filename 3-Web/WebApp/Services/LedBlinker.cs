using System.Device.Gpio;
using System.Reflection;
using WebApp.Interfaces;
using WebApp.Model;

namespace WebApp.Services
{
    public class LedBlinker : BaseProcess, IBlinker
    {
        private readonly IGpioService _gpioService;

        public LedBlinker(IThreadService threadService, IGpioService gpioService) : base(threadService)
        {
            _gpioService = gpioService;
        }

        public int Speed { get; set; } = 50;
        public LedStatus GetLedStatusModel()
        {
            return new LedStatus
            {
                IsActive = IsOn,
                Speed = Speed
            };
        }

        public override void Init()
        {
            _gpioService.OpenPin(AppSettings.LedPinNumber, PinMode.Output);
        }

        public override void Loop()
        {
            for (var i = 0; i < 4; i++)
            {
                _gpioService.Write(AppSettings.LedPinNumber, i % 2 == 0 ? PinValue.High : PinValue.Low);
                _threadService.Sleep(Speed * 3);
            }

            _threadService.Sleep(Speed * 12);
        }
    }
}