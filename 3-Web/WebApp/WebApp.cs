using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using Iot.Device.DhcpServer;
using nanoFramework.Networking;
using nanoFramework.Runtime.Native;
using nanoFramework.WebServer;
using WebApp.Controller;
using WebApp.Interfaces;
using WebApp.WifiAP;

namespace WebApp
{
    public class WebApp : IWebApp
    {
        private static IServiceProvider _serviceProvider;
        private readonly IList _processList = new ArrayList();
        private bool _useLed;
        private bool _useThermometer;

        public WebApp(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Run()
        {
            if (_useLed) _processList.Add(_serviceProvider.GetService(typeof(IBlinkerService)));

            if (_useThermometer) _processList.Add(_serviceProvider.GetService(typeof(IThermometerService)));

            foreach (IProcess process in _processList)
            {
                process?.Init();
                process?.Start();
            }

            Debug.WriteLine("Démarrage du serveur Web!");

            Debug.WriteLine("En attente du réseau Wifi et d'une adresse IP...");

            CancellationTokenSource cs = new(5000);
            var success = WifiNetworkHelper.ConnectDhcp(AppSettings.WifiSsid, AppSettings.WifiPassword,
                requiresDateTime: false, token: cs.Token);
            if (!success)
            {
                Debug.WriteLine($"Impossible d'obtenir une adresse IP, Status du Wifi: {WifiNetworkHelper.Status}.");
                if (WifiNetworkHelper.HelperException != null)
                    Debug.WriteLine($"Exception: {WifiNetworkHelper.HelperException}");
                WifiAPManager.StartWifiAP();
            }
            else
            {
                var networkInterface = NetworkInterface.GetAllNetworkInterfaces()[0];
                Debug.WriteLine("Connexion réussie. IP: " + networkInterface.IPv4Address);
            }


            // Création du Serveur Web
            var controllerArray = new[] { typeof(ApiController), typeof(WebPageController), typeof(AuthController) };

            using (var server = new WebServerDi(80, HttpProtocol.Http,
                       controllerArray, _serviceProvider))
            {
                server.CommandReceived += ServerCommandReceived;
                // Démarrage du serveur
                server.Start();
                Debug.WriteLine("Serveur Web Démarré");
                Thread.Sleep(Timeout.Infinite);
            }
        }

        public void UseLed()
        {
            _useLed = true;
        }

        public void UseThermometer()
        {
            _useThermometer = true;
        }

        private void ServerCommandReceived(object obj, WebServerEventArgs e)
        {
            try
            {
                var url = e.Context.Request.RawUrl;
                if (url == "/test") WebServer.OutPutStream(e.Context.Response, "Test de r&eacute;ponse");

                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
            }
        }
    }
}