using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using nanoFramework.Networking;
using nanoFramework.WebServer;
using WebApp.Controller;

namespace WebApp
{
    public class Program
    {
        private static string _wifiSsid = "Drake";
        //private static string _wifiSsid = "Drake-guest";
        private static string _wifiPassword = "G6kpEhWCpC";
        //private static string _wifiPassword = "Tesla123";
        private static bool _isConnected = false;
        private static GpioController _controller;

        public static void Main()
        {
            Debug.WriteLine("Démarrage du serveur Web!");

             Debug.WriteLine("En attente du réseau Wifi et d'une adresse IP...");

            CancellationTokenSource cs = new(60000);
            var success = WifiNetworkHelper.ConnectDhcp(_wifiSsid, _wifiPassword, requiresDateTime: false, token: cs.Token);
            //var success = WifiNetworkHelper.ConnectDhcp(_wifiSsid, _wifiPassword, requiresDateTime: false, token: cs.Token);
            if (!success)
            {
                Debug.WriteLine($"Impossible d'obtenir une adresse IP, Status du Wifi: {WifiNetworkHelper.Status}.");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"Exception: {WifiNetworkHelper.HelperException.ToString()}");
                }
                return;
            }

            var networkInterface = NetworkInterface.GetAllNetworkInterfaces()[0];
            Debug.WriteLine($"Connexion réussie. IP: " + networkInterface.IPv4Address);

            // Création du Serveur Web
            using (WebServer server = new WebServer(80, HttpProtocol.Http, new Type[] { typeof(ApiController), typeof(WebPageController) }))
            {
                server.Credential = new NetworkCredential("user", "password");
                server.CommandReceived += ServerCommandReceived;
                // Démarrage du serveur
                server.Start();

                Thread.Sleep(Timeout.Infinite);
            }
        }

        private static void ServerCommandReceived(object obj, WebServerEventArgs e)
        {
            var url = e.Context.Request.RawUrl;
        }
    }
}
