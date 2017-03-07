using System.Web.Mvc;

namespace WebForLink.Web.Areas.Externo
{
    public class ExternoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Externo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Externo_default",
                "Externo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "PreCadastro_default",
                "PreCadastro/{action}/{id}",
                new { Controller = "PreCadastro", action = "LinkLst", id = UrlParameter.Optional }
            );
        }
    }
}