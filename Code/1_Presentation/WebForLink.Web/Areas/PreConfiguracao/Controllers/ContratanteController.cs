using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Web.Areas.PreConfiguracao.Models;

namespace WebForLink.Web.Areas.PreConfiguracao.Controllers
{
    public class ContratanteController : Controller
    {
        public readonly IContratanteWebForLinkAppService _contratante;
        public ContratanteController(IContratanteWebForLinkAppService contratante)
        {
            _contratante = contratante;
        }
        // GET: PreConfiguracao/Contratante
        public ActionResult Index()
        {
            List<PesquisaContratanteVM> listaContratante = new List<PesquisaContratanteVM>();
            listaContratante = _contratante.Listar(x => x.TIPO_CONTRATANTE_ID == 1)
                    .Select(x => new PesquisaContratanteVM
                    {
                        Id = x.ID,
                        Cnpj = x.CNPJ,
                        RazaoSocial = x.RAZAO_SOCIAL
                    }).ToList();
            return View(listaContratante);
        }
    }
}