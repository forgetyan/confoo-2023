using WebApp.Model;

namespace WebApp.Interfaces
{
    public interface IThermometerService : IProcess, ISwitchable
    {
        Temperature GetTemperatureModel();
    }
}