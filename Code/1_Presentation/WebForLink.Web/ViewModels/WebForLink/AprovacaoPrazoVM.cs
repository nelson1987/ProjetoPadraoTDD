using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class AprovacaoPrazoVM
    {
        public AprovacaoPrazoVM() { }
        public AprovacaoPrazoVM(SolicitacaoFornecedoresVM fornecedor) 
        {
            Fornecedor = fornecedor;
        }
        public string Motivo { get; set; }
        public bool AprovaPrazo { get; set; }

        public SolicitacaoFornecedoresVM Fornecedor { get; set; }

        public List<SolicitacaoFornecedoresVM> Fornecedores { get; set; }
    }
}
