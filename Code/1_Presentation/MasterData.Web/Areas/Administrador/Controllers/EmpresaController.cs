using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace MasterData.Web.Areas.Administrador.Controllers
{
    public class EmpresaController : Controller
    {
        private IEmpresaAppService _empresaApplication;

        public EmpresaController(IEmpresaAppService configuracaoEmail)
        {
            _empresaApplication = configuracaoEmail;
        }

        // GET: Administrador/Empresa
        public ActionResult Index()
        {
            Empresa ch = new Fornecedor("CH", "123.456", new EmpresaPessoaJuridica());
            var validacao = _empresaApplication.Create(ch);
            return View();
        }
    }
}