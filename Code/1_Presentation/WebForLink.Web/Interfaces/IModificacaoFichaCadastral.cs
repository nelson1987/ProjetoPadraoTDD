using System.Web.Mvc;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Interfaces
{
    public interface IModificacaoFichaCadastral
    {
        ActionResult Incluir();
        ActionResult Editar(int contratanteFornecedorID);
        ActionResult Salvar(FichaCadastralWebForLinkVM model);
        ActionResult Cancelar(int contratanteFornecedorID);
    }
}
