using System.Web.Mvc;
using WebForLink.Web.ViewModels.Adesao;

namespace WebForLink.Web.Interfaces
{
    public interface IAdesaoController
    {
        ActionResult Index();
        ActionResult Index(AdesaoCriacaoVM modelo);
        PartialViewResult CriarAdesao(string chaveUrl);
        ActionResult CriarAdesao(AdesaoCriacaoFormularioVM modelo);
        ActionResult PreCadastro(string chaveUrl);
        ActionResult PreCadastro(PreCadastroAdesaoVM modelo);
        PartialViewResult ValidateUserNameRoute(PreCadastroAdesaoEmpressaVM Empresa);
        ActionResult CriarUsuarios(string chaveUrl);
    }
}