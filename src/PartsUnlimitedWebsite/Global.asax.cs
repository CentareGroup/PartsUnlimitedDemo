using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace PartsUnlimited
{
    public class Global : HttpApplication
    {
        internal static IUnityContainer UnityContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            UnityContainer = UnityConfig.BuildContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityContainer));

            ToggleConfig.Setup(UnityContainer);

            UnityConfig.ConfigureToggleDependencies(UnityContainer);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.RegisterWebApi(GlobalConfiguration.Configuration, UnityContainer);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}