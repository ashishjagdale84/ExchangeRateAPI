using AssignmentApi.App_Start;
using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace AssignmentApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add Custom validation filters  
            config.Filters.Add(new ValidateModelStateFilter());
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
