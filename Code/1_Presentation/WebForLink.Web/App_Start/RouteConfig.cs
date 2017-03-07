using System.Web.Mvc;
using System.Web.Routing;

namespace WebForLink.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;
            //routes.MapRoute("Convite", "Convite/{chaveUrl}",
            //    new { controller = "Plano", action = "Convite", chaveUrl = UrlParameter.Optional }
            //    );
            //routes.MapRoute("PagSeguroTransacao", "PagSeguroTransacao/{id_pagseguro}",
            //    new { controller = "Plano", action = "PagSeguro", chaveUrl = UrlParameter.Optional }
            //    );
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Convite", action = "Index", id = UrlParameter.Optional}
                );
            routes.MapRoute("Convite", "Convite/{chave}",
                new { controller = "Convite", action = "Index", chave = UrlParameter.Optional }
                );
        }
    }
}