using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AndrK.ZavPostav.REST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //routeTemplate: "api/{controller}/{id}",
                routeTemplate: "api/{controller}/{mode}",
                defaults: new { mode = RouteParameter.Optional }
                //defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
