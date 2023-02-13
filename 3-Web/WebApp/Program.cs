using System.Diagnostics;
using nanoFramework.DependencyInjection;
using WebApp.Extensions;
using WebApp.Interfaces;

namespace WebApp
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Démarrage de l'application!");

            var services = new ServiceCollection();
            services.AddLedManagerDI();
            services.AddTemperatureDI();
            services.AddWebDI();
            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetService(typeof(IWebApp)) as IWebApp;
            app.UseLed();
            app.UseThermometer();
            app.Run();
        }
    }
}