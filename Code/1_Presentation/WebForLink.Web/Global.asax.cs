using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;
using log4net;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebForLink.Data.Context;
using WebForLink.Data.Context.Interfaces;
using WebForLink.Web.Mappers;

[assembly: XmlConfigurator(Watch = true)]

namespace WebForLink.Web
{
    public class MvcApplication : HttpApplication
    {
        public static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            try
            {
                Log.ErrorFormat("Aplicação Iniciada");

                GlobalConfiguration.Configure(WebApiConfig.Register);
                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                AutoMapperConfig.RegisterMappings();

                DataAnnotationsModelValidatorProvider.RegisterAdapter(
                    typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
                DataAnnotationsModelValidatorProvider.RegisterAdapter(
                    typeof(AssertThatAttribute), typeof(AssertThatValidator));

                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new RazorViewEngine());
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(ex.Message);
            }
        }
        protected void Application_OnAuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User == null) return;

            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            if (HttpContext.Current.User.Identity is FormsIdentity)
            {
                var id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                string userData = ticket.UserData;
                string[] roles = userData.Split(',');
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
            }
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            //if (authCookie != null)
            //{
            //    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            //    var serializeModel = JsonConvert.DeserializeObject<PrincipalSerializeModel>(authTicket.UserData);

            //    var principal = new TranspetroPrincipal(authTicket.Name)
            //    {
            //        Nome = serializeModel.NomeUsuario,
            //        Email = serializeModel.EmailLogin,
            //        FornecedorIndividual = serializeModel.FornecedorIndividual
            //    };
            //    if (serializeModel.PerfilCorrente != null)
            //    {
            //        principal.PerfilCorrente = new TranspetroPrincipalPerfil
            //        {
            //            IdPerfil = serializeModel.PerfilCorrente.Id,
            //            Descricao = serializeModel.PerfilCorrente.Descricao
            //        };
            //    }
            //    if (serializeModel.PapelCorrente != null)
            //    {
            //        principal.PerfilCorrente = new TranspetroPrincipalPerfil
            //        {
            //            IdPerfil = serializeModel.PerfilCorrente.Id,
            //            Descricao = serializeModel.PerfilCorrente.Descricao
            //        };
            //    }
            //    principal.Perfis = serializeModel.Perfis.Select(p => new TranspetroPrincipalPerfil
            //    {
            //        IdPerfil = p.Id,
            //        Descricao = p.Descricao
            //    }).ToList();

            //    HttpContext.Current.User = principal;
            //}
        }

        protected void Application_End()
        {
            try
            {
                var contextManager =
                    ServiceLocator.Current.GetInstance<IContextManager<ChMasterDataContext>>() as
                        ContextManager<ChMasterDataContext>;
                if (contextManager != null)
                {
                    contextManager.GetContext().Dispose();
                }
                var contextManager2 =
                    ServiceLocator.Current.GetInstance<IContextManager<WebForLinkContexto>>() as
                        ContextManager<WebForLinkContexto>;
                if (contextManager2 != null)
                {
                    contextManager2.GetContext().Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(ex.Message);
            }
        }

        protected void Session_End()
        {
            try
            {
                var contextManager =
                    ServiceLocator.Current.GetInstance<IContextManager<ChMasterDataContext>>() as
                        ContextManager<ChMasterDataContext>;
                if (contextManager != null)
                {
                    contextManager.GetContext().Dispose();
                }
                var contextManager2 =
                    ServiceLocator.Current.GetInstance<IContextManager<WebForLinkContexto>>() as
                        ContextManager<WebForLinkContexto>;
                if (contextManager2 != null)
                {
                    contextManager2.GetContext().Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(ex.Message);
            }
        }

        protected void Application_EndRequest()
        {
            try
            {
                var contextManager =
                    ServiceLocator.Current.GetInstance<IContextManager<ChMasterDataContext>>() as
                        ContextManager<ChMasterDataContext>;
                if (contextManager != null)
                {
                    contextManager.GetContext().Dispose();
                }
                var contextManager2 =
                    ServiceLocator.Current.GetInstance<IContextManager<WebForLinkContexto>>() as
                        ContextManager<WebForLinkContexto>;
                if (contextManager2 != null)
                {
                    contextManager2.GetContext().Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(ex.Message);
            }
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            try
            {
                var ex = Server.GetLastError();
                string uri = null;
                if (Context != null && Context.Request != null)
                {
                    uri = Context.Request.Url.AbsoluteUri;
                }
                var erroGuid = Guid.NewGuid().ToString("N");

                var httpEx = ex as HttpException;
                Session["ErroKey"] = erroGuid;
                Session["httpEx"] = httpEx;

                //Usuario não autenticado
                if ((httpEx != null && httpEx.GetHttpCode() == 401)
                    || (uri != null && Context.Response.StatusCode == 401))
                {
                    logErro("Usuário não autenticado", erroGuid, ex);
                    Server.ClearError();
                    Server.TransferRequest("~/Convite/Index");
                }
                else if ((httpEx != null && httpEx.GetHttpCode() == 403)
                         || (uri != null && Context.Response.StatusCode == 403))
                {
                    logErro("Acesso negado", erroGuid, ex);
                    Server.ClearError();
                    Server.TransferRequest("~/Convite/Index");
                }
                else if ((httpEx != null && httpEx.GetHttpCode() == 404)
                         || (uri != null && Context.Response.StatusCode == 404))
                {
                    logErro("Página não encontrada", erroGuid, ex);
                    Server.ClearError();
                    Server.TransferRequest("~/Convite/Index");
                }
                else if ((httpEx != null && httpEx.GetHttpCode() == 500)
                         || (uri != null && Context.Response.StatusCode == 500))
                {
                    logErro("Erro inesperado", erroGuid, ex);
                    Server.ClearError();
                    Server.TransferRequest("~/Convite/Index");
                    //Server.TransferRequest("~/Home/Error");
                }
                else
                {
                    logErro("Erro inesperado", erroGuid, ex);
                    Server.ClearError();
                    Server.TransferRequest("~/Convite/Index");
                    //Server.TransferRequest("~/Home/Error");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Erro inesperado", ex);
                Server.ClearError();
                Server.TransferRequest("~/Convite/Index");
            }
        }

        private void logErro(string mensagem, string guid, Exception ex)
        {
            var msg = string.Format("Erro na aplicação {0} \n Identificador: {1}", mensagem, guid);
            Log.Error(msg, ex);
        }
    }
}