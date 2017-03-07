using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_CONTATO
    {
        public TIPO_CONTATO()
        {
            WFD_PJPF_CONTATOS = new List<FORNECEDOR_CONTATOS>();
            WFD_SOL_MOD_CONTATO = new List<SolicitacaoModificacaoDadosContato>();
        }

        public int ID { get; set; }
        public string NM_TP_CONTATO { get; set; }
        public virtual ICollection<FORNECEDOR_CONTATOS> WFD_PJPF_CONTATOS { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosContato> WFD_SOL_MOD_CONTATO { get; set; }
    }
}