using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class BancoDoFornecedor
    {
        public BancoDoFornecedor()
        {
            WFD_SOL_MOD_BANCO = new List<SolicitacaoModificacaoDadosBancario>();
            MEU_COMPARTILHAMENTOS = new List<Compartilhamentos>();
        }

        public int ID { get; set; }
        public int BANCO_ID { get; set; }
        public string AGENCIA { get; set; }
        public string AG_DV { get; set; }
        public string CONTA { get; set; }
        public string CONTA_DV { get; set; }
        public bool ATIVO { get; set; }
        public int? CONTRATANTE_PJPF_ID { get; set; }
        public int? ARQUIVO_ID { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public DateTime? DATA_UPLOAD { get; set; }
        public virtual TiposDeBanco T_BANCO { get; set; }
        public virtual ARQUIVOS WFD_ARQUIVOS { get; set; }
        public virtual WFD_CONTRATANTE_PJPF WFD_CONTRATANTE_PJPF { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosBancario> WFD_SOL_MOD_BANCO { get; set; }
        public virtual ICollection<Compartilhamentos> MEU_COMPARTILHAMENTOS { get; set; }
    }
}