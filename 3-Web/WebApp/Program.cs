using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using Windows.Storage;
using nanoFramework.Networking;
using nanoFramework.WebServer;
using WebApp.Controller;
using nanoFramework.DependencyInjection;
using WebApp.Extensions;
using WebApp.Interfaces;
using WebApp.Services;

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