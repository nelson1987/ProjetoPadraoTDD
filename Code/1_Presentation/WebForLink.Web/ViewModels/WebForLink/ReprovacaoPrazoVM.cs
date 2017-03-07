using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class ReprovacaoPrazoVM
    {
        public string Motivo { get; set; }

        public SolicitacaoFornecedoresVM Fornecedor { get; set; }

        public List<SolicitacaoFornecedoresVM> Fornecedores { get; set; }
    }
}
