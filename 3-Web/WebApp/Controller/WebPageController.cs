using nanoFramework.WebServer;

namespace WebApp.Controller
{
    public class WebPageController
    {
        [Route("default.html"), Route("index.html"), Route("/")]
        public void Index(WebServerEventArgs e)
        {
            e.Context.Response.ContentType = "text/html";
            //WebServer.OutPutStream(e.Context.Response, "<html><body>"
            WebServer.OutPutStream(e.Context.Response, Resources.GetString(Resources.StringResources.index));
        }

        [Route("favicon.ico")]
        public void FavIcon(WebServerEventArgs e)
        {
            WebServer.SendFileOverHTTP(e.Context.Response, "favicon.ico", Resources.GetBytes(Resources.BinaryResources.favicon));
        }


        [Route("style.css")]
        public void StyleCss(WebServerEventArgs e)
        {
            e.Context.Response.ContentType = "text/css";
            WebServer.OutPutStream(e.Context.Response, Resources.GetString(Resources.StringResources.style));
        }

        [Route("script.js")]
        public void ScriptJs(WebServerEventArgs e)
        {
            e.Context.Response.ContentType = "application/javascript";
            WebServer.OutPutStream(e.Context.Response, Resources.GetString(Resources.StringResources.script));
        }
    }
}
