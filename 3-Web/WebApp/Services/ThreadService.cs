using System.Threading;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class ThreadService : IThreadService
    {
        public void Sleep(int interval)
        {
            Thread.Sleep(interval);
        }
    }
}