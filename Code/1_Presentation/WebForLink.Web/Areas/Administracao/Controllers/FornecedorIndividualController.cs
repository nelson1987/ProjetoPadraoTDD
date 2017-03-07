using System.Web.Mvc;

namespace WebForDocs.Areas.ConfiguracaoInicial.Controllers
{
    public class FornecedorIndividualController : Controller
    {
        // GET: ConfiguracaoInicial/FornecedorIndividual
        public ActionResult Index()
        {
            string cnpj = "82.628.434/0001-03";
            string razaoSocial = "Teste Inclusão Contratante Âncora";
            string nomeFantasia = "Contratante âncora";
            string codigoWebformat = "CANCORA";
            string codigoErp = "123456";
            //using (var contratanteAncora = new WebForLink.Service.PreConfiguracao.Contratante.ContratanteBP())
            //{
            //    contratanteAncora.IncluirContratanteIndividual(cnpj, razaoSocial, nomeFantasia, codigoWebformat, codigoErp);
            //}
                return View();
        }
    }
}