using nanoFramework.WebServer;
using System.Net;

namespace WebApp.Controller
{
    [Authentication("Basic:user password")]
    public class AuthController
    {
        [Route("authbasic")]
        public void Basic(WebServerEventArgs e)
        {
            WebServer.OutPutStream(e.Context.Response, "Vous avez accès à cette page sécurisée");
        }
    }
}