using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Areas.Externo.Controllers
{
    public class EnderecoPartialController : Controller
    {
        private readonly EnderecoWebForLinkAppService enderecoBP;
        public EnderecoPartialController(EnderecoWebForLinkAppService endereco)
        {
            enderecoBP = endereco;
        }
        // GET: Externo/EnderecoPartial
        public PartialViewResult Index(DadosEnderecosVM endereco)
        {
            ViewBag.TipoEndereco = new SelectList(enderecoBP.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");
            ViewBag.UF = new SelectList(enderecoBP.ListarTodosPorNome(), "UF_SGL", "UF_NM");
            return PartialView("~/Areas/Externo/Views/Shared/_PreCadastro_DadosEndereco_Editavel.cshtml", new DadosEnderecosVM() { });
        }
    }
}