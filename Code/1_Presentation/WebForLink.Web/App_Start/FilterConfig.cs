using System.Web.Mvc;
using WebForLink.Web.Filters;

namespace WebForLink.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new BreadCrumbAttribute());
        }
    }
}