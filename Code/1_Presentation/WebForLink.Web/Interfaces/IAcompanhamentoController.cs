using System.Collections.Generic;
using System.Web.Mvc;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Interfaces
{
    public interface IAcompanhamentoController
    {
        PartialViewResult _FichaCadastral_DadosBancario(List<DadosBancariosVM> modelo);
                
        void PreparaModal(FichaCadastralWebForLinkVM model);

        ActionResult ReenviarFicha(string TipoFuncionalidade, int idSolicitacao, string CNPJ, string EmailContato,
            FichaCadastralWebForLinkVM ficha);

        JsonResult AprovarProrrogacao(int idProrrogacao);

        JsonResult ReprovarProrrogacao(int idProrrogacao, string motivo);
    }
}