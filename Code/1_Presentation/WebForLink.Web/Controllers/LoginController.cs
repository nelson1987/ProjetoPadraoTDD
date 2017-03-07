using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebForLink.Application.Interfaces;
using WebForLink.CrossCutting.InversionControl;
using WebForLink.Domain.Entities;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ICategoriaAppService _fornecedorAppService;

        public LoginController(ICategoriaAppService categoriaAppService)
        {
            _fornecedorAppService = categoriaAppService;
        }

        //public LoginController()
        //{
        //    var ioc = new IoC();
        //    _fornecedorAppService = ioc.Kernel.Get<ICategoriaAppService>();
        //}

        // GET: Login
        [OutputCache(Duration = 30000)]
        public ActionResult Index()
        {
            return PartialView("_Logar", new LoginVM());
        }
        // GET: Login
        [HttpPost]
        public async Task<ActionResult> Logar(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Autenticar", model);
                    //var fornecedores = await _fornecedorAppService.AllAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ModelState.AddModelError("ErroLogin", "Erro ao tentar logar o usuário.");
            //ViewData["modelo"] = model;
            //return RedirectToAction("Index", "Home");
            return View("_Logar", model);
        }
        // GET: Login

        //[HttpPost]
        public ActionResult Autenticar(LoginVM model, string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.Login, model.Senha))
                    {
                        /*
                        var usuario = ca.BuscarDadosUsuarioLogado();
                        var bpForcaTrabalho = new BpForcaTrabalho();
                        var perfisUsuario = ca.ListarPerfisUsuarioLogado();
                        bool empregadoProprio = bpForcaTrabalho.EmpregadoProprio(login.Usuario);

                        //Caso o usuário não seja empregado e não possuir perfil no usuário o acesso é negado.
                        if ((!empregadoProprio) && (perfisUsuario == null || perfisUsuario.Count() <= 0))
                        {
                            this.FinalizarAcesso();
                            throw new BINTBusinessException("Acesso permitido somente para empregados");
                        }
*/

                        CreateAuthenticationTicket(
                            new UsuarioSistema { Chave = "KYUH", Email = "nelson.neto@chconsultoria.com.br", Nome = "Nelson Neto" },
                            true,
                            new List<PerfilSistema> {
                                new PerfilSistema { Id = "ADM", Nome = "ADMINISTRADOR" },
                            });

                        return Redirect(FormsAuthentication.GetRedirectUrl(model.Login, false));

                    }
                    else
                    {
                        ModelState.AddModelError("", "Login ou senha  inválido");
                    }

                }
                else
                    ModelState.AddModelError("", "Login ou senha inválido");

                return View(model);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                ModelState.AddModelError("", "Erro na autenticação");
                this.FinalizarAcesso();
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Logoff()
        {
            this.FinalizarAcesso();
            return RedirectToAction("Autenticar", "Login");

        }

        private void FinalizarAcesso()
        {
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-1);
            FormsAuthentication.SignOut();
            Session.Abandon();
        }

        public void CreateAuthenticationTicket(UsuarioSistema usuario, bool proprio, IList<PerfilSistema> perfis)
        {
            PrincipalSerializeModel serializeModel = new PrincipalSerializeModel();
            serializeModel.Cnpj = usuario.Chave;
            serializeModel.NomeUsuario = usuario.Nome;
            serializeModel.EmailLogin = usuario.Email;
            serializeModel.FornecedorIndividual = proprio;

            if (perfis != null && perfis.Count() > 0)
            {
                serializeModel.Perfis = perfis.Select(p => new
                PrincipalPerfilSerializeModel()
                {
                    Id = p.Id,
                    Descricao = p.Nome
                }).ToArray();
            }
            else
            {
                //Usuário genérico
                //var perf = new BpPerfil().PerfilUsuario();
                //var perfisUsuario = new List<TranspetroPrincipalPerfilSerializeModel>()
                //    { new TranspetroPrincipalPerfilSerializeModel() {
                //        IdCa = perf.IdCa,
                //        Descricao = perf.DescricaoPerfil
                //} };

                //serializeModel.Perfis = perfisUsuario.ToArray();
            }

            //this.perfil = serializeModel.Perfis[0].Descricao;
            //this.Usuario = serializeModel.Nome;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
              1, serializeModel.Cnpj, DateTime.Now, DateTime.Now.AddHours(8), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }
    }
}