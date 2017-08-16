using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace Lightbulbs.Hosting
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:8080");

            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                "WebAPIDefault", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            WebApiLoader loader = new WebApiLoader();
            config.Services.Replace(typeof(IAssembliesResolver), loader);

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }
    }   //c
}
