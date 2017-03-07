using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_CONTATOS
    {
        public FORNECEDOR_CONTATOS()
        {
            WFD_SOL_MOD_CONTATO = new List<SolicitacaoModificacaoDadosContato>();
            MEU_COMPARTILHAMENTOS = new List<Compartilhamentos>();
        }

        public int ID { get; set; }
        public int? CONTRAT_ORG_COMPRAS_ID { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string CELULAR { get; set; }
        public int? CONTRATANTE_PJPF_ID { get; set; }
        public int? TP_CONTATO_ID { get; set; }
        public virtual WFD_CONTRATANTE_PJPF WFD_CONTRATANTE_PJPF { get; set; }
        public virtual TIPO_CONTATO WFD_T_TP_CONTATO { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosContato> WFD_SOL_MOD_CONTATO { get; set; }
        public virtual ICollection<Compartilhamentos> MEU_COMPARTILHAMENTOS { get; set; }
    }
}