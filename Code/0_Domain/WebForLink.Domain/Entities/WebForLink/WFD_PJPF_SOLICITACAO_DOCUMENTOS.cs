using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_SOLICITACAO_DOCUMENTOS
    {
        public int ID { get; set; }
        public int PJPF_SOLICITACAO_ID { get; set; }
        public int? PJPF_ID { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public int LISTA_DOCUMENTOS_ID { get; set; }
        public DateTime? DATA_UPLOAD { get; set; }
        public int? PJPF_ARQUIVO_ID { get; set; }
        public int? SITUACAO_ID { get; set; }
        public string OBSERVACAO { get; set; }
        public DateTime? DATA_VENCIMENTO { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public string EXTENSAO_ARQUIVO { get; set; }
        public virtual FORNECEDORBASE WFD_PJPF_BASE { get; set; }
        public virtual ListaDeDocumentosDeFornecedor WFD_PJPF_LISTA_DOCUMENTOS { get; set; }
        public virtual FORNECEDOR_SOLICITACAO WFD_PJPF_SOLICITACAO { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
    }
}