using System.Web.Mvc;

namespace WebForLink.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminDefault",
                "Admin/{controller}/{action}/{id}",
                new {controller = "Admin", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}