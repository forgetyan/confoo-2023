using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using nanoFramework.Json;
using nanoFramework.WebServer;
using WebApp.Interfaces;
using WebApp.Model;

namespace WebApp.Controller
{
    public class ApiController
    {
        private readonly IBlinker _blinker;
        private readonly IThermometerService _thermometerService;

        public ApiController(IBlinker blinker, IThermometerService thermometerService)
        {
            _blinker = blinker;
            _thermometerService = thermometerService;
        }

        [Route("api/status")]
        [Method("GET")]
        public void GetControllerStatus(WebServerEventArgs e)
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces()[0];
            var controllerStatus = new ControllerStatus()
            {
                Ip = networkInterface.IPv4Address,
            };
            var result = JsonConvert.SerializeObject(controllerStatus);
            WebServer.OutPutStream(e.Context.Response, result);
        }

        [Route("api/led")]
        [Method("GET")]
        public void GetLedStatus(WebServerEventArgs e)
        {
            var result = JsonConvert.SerializeObject(_blinker.GetLedStatusModel());
            e.Context.Response.ContentType = "text/json";
            e.Context.Response.ContentLength64 = result.Length;
            WebServer.OutPutStream(e.Context.Response, result);
        }

        [Route("api/temperature")]
        [Method("GET")]
        public void GetTemperature(WebServerEventArgs e)
        {
            Temperature temp = _thermometerService.GetTemperatureModel();
            var result = JsonConvert.SerializeObject(temp);
            e.Context.Response.ContentType = "text/json";
            e.Context.Response.ContentLength64 = result.Length;
            WebServer.OutPutStream(e.Context.Response, result);
        }

        [Route("api/led/active")]
        [Method("POST")]
        public void SetLedActive(WebServerEventArgs e)
        {
            var rawData = GetBody(e);
            _blinker.IsOn = rawData == "1";
            WebServer.OutPutStream(e.Context.Response, "OK");
        }

        [Route("api/led/speed")]
        [Method("POST")]
        public void SetLedSpeed(WebServerEventArgs e)
        {
            var rawData = GetBody(e);
            _blinker.Speed = int.Parse(rawData);
            WebServer.OutPutStream(e.Context.Response, "OK");
        }

        [Route("api/temperature/active")]
        [Method("POST")]
        public void SetTemperatureActive(WebServerEventArgs e)
        {
            var rawData = GetBody(e);
            _thermometerService.IsOn = rawData == "1";
            WebServer.OutPutStream(e.Context.Response, "OK");
        }

        private static string GetBody(WebServerEventArgs e)
        {
            var buff = new byte[e.Context.Request.ContentLength64];
            e.Context.Request.InputStream.Read(buff, 0, buff.Length);
            var rawData = new string(Encoding.UTF8.GetChars(buff));
            return rawData;
        }
    }
}