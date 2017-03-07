using System.Web.Mvc;

namespace WebForLink.Web.Areas.PreConfiguracao
{
    public class PreConfiguracaoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PreConfiguracao";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PreConfiguracao_default",
                "PreConfiguracao/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}