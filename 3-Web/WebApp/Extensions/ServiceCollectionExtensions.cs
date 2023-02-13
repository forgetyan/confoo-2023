using nanoFramework.DependencyInjection;
using WebApp.Interfaces;
using WebApp.Services;

namespace WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddLedManagerDI(this ServiceCollection services)
        {
            services.AddSingleton(typeof(IBlinkerService), typeof(LedBlinkerService));
            services.AddSingleton(typeof(IGpioService), typeof(GpioService));
            return services;
        }

        public static ServiceCollection AddTemperatureDI(this ServiceCollection services)
        {
            services.AddSingleton(typeof(IThermometerService), typeof(ThermometerService));
            return services;
        }

        public static ServiceCollection AddWebDI(this ServiceCollection services)
        {
            services.AddSingleton(typeof(IWebApp), typeof(WebApp));
            services.AddSingleton(typeof(IThreadService), typeof(ThreadService));
            
            return services;
        }
    }
}