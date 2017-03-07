using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class SolicitacaoFornecedoresVM
    {
        public SolicitacaoFornecedoresVM()
        {
            Emails = new List<SolicitacaoFornecedoresContatosVM>();
        }

        public int ID { get; set; }

        public string NomeFornecedor { get; set; }

        public string CNPJ { get; set; }

        public SolicitacaoFornecedoresContatosVM Contato { get; set; }
        
        public List<SolicitacaoFornecedoresContatosVM> Emails { get; set; }
    }
}