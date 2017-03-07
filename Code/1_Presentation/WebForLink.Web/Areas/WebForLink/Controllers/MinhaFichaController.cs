using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Areas.WebForLink.ViewModels.MinhaFicha;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Areas.WebForLink.Controllers
{
    public class MinhaFichaController : ControllerPadrao
    {
        private readonly IFichaCadastralWebForLinkAppService _bpFichaCadastral;

        public MinhaFichaController(IFichaCadastralWebForLinkAppService bpFichaCadastral)
        {
            _bpFichaCadastral = bpFichaCadastral;
        }

        // GET: WebForLink/MinhaFicha
        public ActionResult Index()
        {
            int contratanteId = 1;
            Fornecedor contratante = _bpFichaCadastral.BuscarFichaCadastralMeuContratante(contratanteId);
            MinhaFichaCadastralVM modelo = new MinhaFichaCadastralVM();

            return View();
        }
    }
}