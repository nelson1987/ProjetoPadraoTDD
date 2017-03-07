using System.Reflection;
using System.Web.Mvc;
using log4net;
using Ninject;
using WebForLink.Application.Interfaces;
using WebForLink.CrossCutting.InversionControl;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Filters
{
    public sealed class BreadCrumbAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly ISolicitacaoAppService _conviteAppService;
        public ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BreadCrumbAttribute()
        {
            var ioc = new IoC();
            _conviteAppService = ioc.Kernel.Get<ISolicitacaoAppService>();
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.NomeController =
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            filterContext.Controller.ViewBag.NomeAcao = filterContext.ActionDescriptor.ActionName;

            int usuarioId = 0;

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            Log.Error(string.Format("Controller: {0}, Action: {1}, UsuarioId: {2}",
                filterContext.Controller.ViewBag.NomeController,
                filterContext.Controller.ViewBag.NomeAcao,
                usuarioId));
            OnActionExecuting(filterContext);
        }
    }
}