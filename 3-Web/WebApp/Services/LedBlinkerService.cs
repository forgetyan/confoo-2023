using System.Device.Gpio;
using WebApp.Interfaces;
using WebApp.Model;

namespace WebApp.Services
{
    public class LedBlinkerService : BaseProcess, IBlinkerService
    {
        private readonly IGpioService _gpioService;

        public LedBlinkerService(IThreadService threadService, IGpioService gpioService) : base(threadService)
        {
            _gpioService = gpioService;
        }

        public LedStatus GetLedStatusModel()
        {
            return new LedStatus
            {
                IsActive = IsOn,
                Speed = Speed
            };
        }

        public int Speed { get; set; } = 50;

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