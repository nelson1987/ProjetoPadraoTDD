using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SolicitacaoDeDocumentos
    {
        public int ID { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public int DESCRICAO_DOCUMENTO_ID { get; set; }
        public int? LISTA_DOCUMENTO_ID { get; set; }
        public DateTime? DATA_UPLOAD { get; set; }
        public int? ARQUIVO_ID { get; set; }
        public int? SITUACAO_ID { get; set; }
        public string OBSERVACAO { get; set; }
        public DateTime? DATA_VENCIMENTO { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public string EXTENSAO_ARQUIVO { get; set; }
        public int? MENSAGEM_ID { get; set; }
        public int? PJPF_DOCUMENTO_ID { get; set; }
        public bool? EXIGE_VALIDADE { get; set; }
        public int? PERIODICIDADE_ID { get; set; }
        public bool? OBRIGATORIO { get; set; }
        public virtual ARQUIVOS WFD_ARQUIVOS { get; set; }
        public virtual DescricaoDeDocumentos DescricaoDeDocumentos { get; set; }
        public virtual DocumentosDoFornecedor DocumentosDoFornecedor { get; set; }
        public virtual ListaDeDocumentosDeFornecedor ListaDeDocumentosDeFornecedor { get; set; }
        public virtual SOLICITACAO_MENSAGEM WFD_SOL_MENSAGEM { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_PERIODICIDADE WFD_T_PERIODICIDADE { get; set; }
    }
}