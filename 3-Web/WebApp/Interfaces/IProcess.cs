namespace WebApp.Interfaces
{
    public interface IProcess
    {
        void Init();
        void Loop();
        void Start();
        void Stop();
    }
}