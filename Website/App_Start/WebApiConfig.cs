﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Harcourts.Face.WebsiteCommon;
using Harcourts.Face.WebsiteCommon.Authorization.Http;
using Newtonsoft.Json.Serialization;

namespace Harcourts.Face.Website
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CROS on all domains.
            config.EnableCors();
            // Enable attribute routing.
            config.MapHttpAttributeRoutes();
            // Default routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            // All requests must come from trusted sources.
            config.Filters.Add(new UniformErrorAttribute());
            config.Filters.Add(new WebsiteCommon.Authorization.Http.AuthorizeAttribute());
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());

            // This is to make result shown as Json in Chrome as per:
            // http://stackoverflow.com/questions/9847564/how-do-i-get-asp-net-web-api-to-return-json-instead-of-xml-using-chrome
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // This is to make Json result with camel cased properties as per:
            // https://frankapi.wordpress.com/2012/09/09/going-camelcase-in-asp-net-mvc-web-api/
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
