using System.Web.Mvc;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Areas.Externo.Controllers
{
    public class ContatoPartialController : Controller
    {
        // GET: Externo/ContatoPartial
        public PartialViewResult Index(DadosContatoVM contato)
        {
            return PartialView("~/Areas/Externo/Views/Shared/_PreCadastro_DadosContato_Editavel.cshtml", contato);
        }
    }
}