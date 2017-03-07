using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TiposDeBanco
    {
        public TiposDeBanco()
        {
            SolicitacoesModificacaoDadosBancario = new List<SolicitacaoModificacaoDadosBancario>();
            BancosDoFornecedor = new List<BancoDoFornecedor>();
        }

        public int ID { get; set; }
        public string BANCO_COD { get; set; }
        public string BANCO_NM { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosBancario> SolicitacoesModificacaoDadosBancario { get; set; }
        public virtual ICollection<BancoDoFornecedor> BancosDoFornecedor { get; set; }
    }
}