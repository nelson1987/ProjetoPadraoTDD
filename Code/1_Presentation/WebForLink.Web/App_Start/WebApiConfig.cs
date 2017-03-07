using System.Web.Http;

namespace WebForLink.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("Default_Api", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}