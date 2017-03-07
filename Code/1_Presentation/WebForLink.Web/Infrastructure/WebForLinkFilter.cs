using log4net;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace WebForLink.Web.Infrastructure
{
    public class WebForLinkFilter : FilterAttribute, IExceptionFilter, IResultFilter, IActionFilter
    {
        public ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (filterContext.HttpContext.Session["UserID"] != null)
            //    filterContext.Result = new RedirectResult("/Home/Contact");
            //else
            //    filterContext.Result = new RedirectResult("/Login/Login");
            Log.Error("filterContext", new Exception("OnActionExecuting(ActionExecutingContext filterContext)"));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.MensagemSucesso = filterContext.Controller.TempData["MensagemSucesso"] ?? "";
            filterContext.Controller.ViewBag.MensagemError = filterContext.Controller.TempData["MensagemError"] ?? "";

            //if (filterContext.HttpContext.Session["UserID"] != null)
            //    filterContext.Result = new RedirectResult("/Home/Contact");
            //else
            //    filterContext.Result = new RedirectResult("/Login/Login");
            Log.Error("filterContext", new Exception("OnActionExecuted(ActionExecutedContext filterContext)"));
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //if (filterContext.HttpContext.Session["UserID"] != null)
            //    filterContext.Result = new RedirectResult("/Home/Contact");
            //else
            //    filterContext.Result = new RedirectResult("/Login/Login");
            Log.Error("filterContext", new Exception("OnResultExecuting(ResultExecutingContext filterContext)"));
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //if (filterContext.HttpContext.Session["UserID"] != null)
            //    filterContext.Result = new RedirectResult("/Home/Contact");
            //else
            //    filterContext.Result = new RedirectResult("/Login/Login");
            Log.Error("filterContext", new Exception("OnResultExecuted(ResultExecutedContext filterContext)"));
        }

        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewBag.onExceptionError = "ExceptionFilter filter called";
            filterContext.HttpContext.Response.Write("ExceptionFilter filter called");
            Log.Error("filterContext", new Exception("OnException(ExceptionContext filterContext)"));
        }
    }
}