using System.Collections.Generic;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Domain.Entities;

namespace MasterData.Web.Areas.Administrador.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioAppService _contratanteConfiguracaoEmailBp;

        public UsuarioController(IUsuarioAppService configuracaoEmail)
        {
            _contratanteConfiguracaoEmailBp = configuracaoEmail;
        }

        // GET: Administrador/Usuario
        public ActionResult Index()
        {
            var usuarios = new List<Usuario>();
            return View();
        }
    }
}