using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RotamaticApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;

            var settings = jsonFormatter.SerializerSettings;
            //settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "ALL_WITHOUT_includeProperties",
                routeTemplate: "api/{controller}/{action}/all"
            );

            config.Routes.MapHttpRoute(
                name: "SINGLE_WITHOUT_includeProperties",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ALL_WITH_includeProperties",
                routeTemplate: "api/{controller}/{action}/all/{includeProperties}",
                defaults: new { includeProperties = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SINGLE_WITH_includeProperties",
                routeTemplate: "api/{controller}/{action}/{id}/{includeProperties}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
