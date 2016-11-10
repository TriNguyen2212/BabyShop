using Newtonsoft.Json;
using BabyShop.Web.Mappings;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;

namespace BabyShop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
            //fix bug serialize for web API
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            JsonSerializerSettings jSettings = new JsonSerializerSettings();
            jSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
        }
    }
}