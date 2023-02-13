using WebApp.Model;

namespace WebApp.Interfaces
{
    public interface IBlinker : IProcess, ISwitchable
    {
        int Speed { get; set; }
        LedStatus GetLedStatusModel();
    }
}