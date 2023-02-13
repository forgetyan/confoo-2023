using Iot.Device.DhcpServer;
using nanoFramework.Runtime.Native;
using System.Diagnostics;
using System.Net;

namespace WebApp.WifiAP
{
    internal static class WifiAPManager
    {
        public static void StartWifiAP()
        {
            Wireless80211.Disable();
            if (WirelessAP.Setup() == false)
            {
                // Reboot device to Activate Access Point on restart
                Debug.WriteLine($"Setup Soft AP, Rebooting device");
                Power.RebootDevice();
            }

            var dhcpserver = new DhcpServer
            {
                CaptivePortalUrl = $"http://{WirelessAP.SoftApIP}"
            };
            var dhcpInitResult = dhcpserver.Start(IPAddress.Parse(WirelessAP.SoftApIP), new IPAddress(new byte[] { 255, 255, 255, 0 }));
            if (!dhcpInitResult)
            {
                Debug.WriteLine($"Error initializing DHCP server.");
            }

            Debug.WriteLine($"Running Soft AP, waiting for client to connect");
            Debug.WriteLine($"Soft AP IP address :{WirelessAP.GetIP()}");
        }
    }
}