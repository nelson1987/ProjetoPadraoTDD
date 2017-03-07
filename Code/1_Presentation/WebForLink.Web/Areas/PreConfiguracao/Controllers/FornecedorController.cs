using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Web.Areas.PreConfiguracao.Models.Fornecedor;

namespace WebForLink.Web.Areas.PreConfiguracao.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorWebForLinkAppService _contratantebp;
        public FornecedorController(IFornecedorWebForLinkAppService contratante)
        {
            _contratantebp = contratante;
        }
        // GET: PreConfiguracao/Fornecedor
        public ActionResult Index()
        {
            int contratanteId = 1;
            List<PesquisaFornecedorVM> listaContratante = new List<PesquisaFornecedorVM>();
            listaContratante = _contratantebp.ListarFornecedoresIndividuaisEConvencionaisDeContratante(contratanteId)
                    .Select(x => new PesquisaFornecedorVM
                    {
                        Id = x.ID,
                        Cnpj = x.CNPJ,
                        RazaoSocial = x.RAZAO_SOCIAL,
                        IdContratante = x.CONTRATANTE_ID
                    }).ToList();
            return View(listaContratante);
        }
    }
}