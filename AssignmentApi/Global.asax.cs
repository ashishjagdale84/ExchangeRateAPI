using AssignmentApi.App_Start;
using FluentValidation.WebApi;
using log4net;
using System.Web.Http;

namespace AssignmentApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            log4net.Config.XmlConfigurator.Configure();

            
            log.Info("Application - Main is invoked");

        }
    }
}
