using WebApp.Model;

namespace WebApp.Interfaces
{
    public interface IBlinkerService : IProcess, ISwitchable
    {
        int Speed { get; set; }
        LedStatus GetLedStatusModel();
    }
}