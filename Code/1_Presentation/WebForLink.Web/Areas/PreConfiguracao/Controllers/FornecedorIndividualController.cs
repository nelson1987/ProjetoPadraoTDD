using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Web.Areas.PreConfiguracao.Models.FornecedorIndividual;

namespace WebForLink.Web.Areas.PreConfiguracao.Controllers
{
    public class FornecedorIndividualController : Controller
    {
        private readonly IFornecedorWebForLinkAppService _contratantebp;

        public FornecedorIndividualController(IFornecedorWebForLinkAppService contratantebp)
        {
            this._contratantebp = contratantebp;
        }

        // GET: PreConfiguracao/FornecedorIndividual
        public ActionResult Index()
        {
            List<PesquisaFornecedorIndividualVM> listaContratante = new List<PesquisaFornecedorIndividualVM>();

                listaContratante = _contratantebp.ListarFornecedoresIndividuais()
                        .Select(x => new PesquisaFornecedorIndividualVM
                        {
                            Id = x.ID,
                            Cnpj = x.CNPJ,
                            RazaoSocial = x.RAZAO_SOCIAL,
                        }).ToList();
            
            return View(listaContratante);
        }
    }
}