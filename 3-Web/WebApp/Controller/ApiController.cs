using nanoFramework.WebServer;
using System;

namespace WebApp.Controller
{
    public class ApiController
    {
        [Route("api/temperature")]
        [Method("GET")]
        public void GetTemperature(WebServerEventArgs e)
        {
            e.Context.Response.ContentType = "text/json";
        }
    }
}
