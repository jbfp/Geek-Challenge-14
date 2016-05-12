using SGC14.Web.Mapping;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGC14.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapMvcAttributeRoutes();        
            AutoMapperConfig.Configure();
        }
    }
}