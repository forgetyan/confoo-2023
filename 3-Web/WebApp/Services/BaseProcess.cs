using System.Threading;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public abstract class BaseProcess : IProcess, ISwitchable
    {
        private static Thread _runningThread;
        protected readonly IThreadService _threadService;
        public bool IsOn { get; set; }
        protected BaseProcess(IThreadService threadService)
        {
            _threadService = threadService;
            IsOn = true;
        }

        public void Start()
        {
            Stop();
            _runningThread = new Thread(Process);
            _runningThread.Start();
        }

        public void Stop()
        {
            if (_runningThread is { ThreadState: ThreadState.Running })
            {
                _runningThread.Abort();
            }
        }

        public abstract void Loop();
        public abstract void Init();

        public void TurnOff()
        {
            IsOn = false;
        }

        public void TurnOn()
        {
            IsOn = true;
        }

        private void Process()
        {
            while (true)
            {
                if (IsOn)
                {
                    Loop();
                }
                else
                {
                    _threadService.Sleep(100);
                }
            }
        }
    }
}