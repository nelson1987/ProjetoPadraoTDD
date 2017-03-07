using System.Web.Mvc;

namespace WebForLink.Web.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // GET: Admin/Blog
        public ActionResult Index()
        {
            return View();
        }
    }
}