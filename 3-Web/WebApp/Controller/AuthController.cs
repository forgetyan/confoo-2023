using nanoFramework.WebServer;

namespace WebApp.Controller
{
    [Authentication("Basic:user password")]
    public class AuthController
    {
        [Route("authbasic")]
        public void Basic(WebServerEventArgs e)
        {
            WebServer.OutPutStream(e.Context.Response, "Vous avez acc&egrave;s &agrave; cette page s&eacute;curis&eacute;e");
        }
    }
}