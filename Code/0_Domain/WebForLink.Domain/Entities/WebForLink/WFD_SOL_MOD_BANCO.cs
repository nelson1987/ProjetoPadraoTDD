using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SolicitacaoModificacaoDadosBancario
    {
        public int ID { get; set; }
        public int BANCO_ID { get; set; }
        public string AGENCIA { get; set; }
        public string AG_DV { get; set; }
        public string CONTA { get; set; }
        public string CONTA_DV { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public int? PJPF_ID { get; set; }
        public int? BANCO_PJPF_ID { get; set; }
        public int? ARQUIVO_ID { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public DateTime? DATA_UPLOAD { get; set; }
        public virtual TiposDeBanco T_BANCO { get; set; }
        public virtual ARQUIVOS WFD_ARQUIVOS { get; set; }
        public virtual BancoDoFornecedor BancoDoFornecedor { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
    }
}