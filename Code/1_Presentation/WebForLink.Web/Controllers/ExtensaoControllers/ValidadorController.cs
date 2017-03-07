using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.Controllers.ExtensaoControllers
{
    public class ValidadorController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IUsuarioWebForLinkAppService _usuarioService;
        public ValidadorController(IUsuarioWebForLinkAppService usuario) : base()
        {
            _usuarioService = usuario;
        }
        #endregion

        public JsonResult ValidarNomeLogin(string login)
        {
            bool pesquisa = _usuarioService.BuscarPorLogin(login) == null;
            return Json(pesquisa, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarCpf(string cpf, int idContratante)
        {
            bool retorno = false;
            bool valido = Validacao.ValidaCPF(cpf);
            bool pesquisa = _usuarioService.BuscarPorCpf(cpf) == null;
            if (valido && pesquisa) retorno = true;
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarCnpj(string cnpj, int idContratante)
        {
            bool retorno = false;
            bool valido = Validacao.ValidaCPF(cnpj);
            bool pesquisa = _usuarioService.BuscarPorCpf(cnpj) == null;
            if (valido && pesquisa) retorno = true;
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}