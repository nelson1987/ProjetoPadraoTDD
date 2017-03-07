using System.Collections.Generic;

namespace WebForLink.Web.ViewModels.FichaCadastral
{
    public class FichaSolicitacaoVM
    {
        public FichaSolicitacaoVM()
        {
        }
        //public FichaCadastral.SolicitacaoVM Solicitacao { get; set; }
        public DadosGeraisVM DadosGerais { get; set; }
        public List<DadosBancariosVM> DadosBancarios { get; set; }
        public List<DadosContatoVM> DadosContatos { get; set; }
        public List<DadosEnderecosVM> DadosEnderecos { get; set; }
        public List<FornecedorUnspscVM> FornecedoresUnspsc { get; set; }
    }
}