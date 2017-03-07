using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class ListaDeDocumentosDeFornecedor
    {
        public ListaDeDocumentosDeFornecedor()
        {
            WFD_PJPF_DOCUMENTOS = new List<DocumentosDoFornecedor>();
            WFD_PJPF_SOLICITACAO_DOCUMENTOS = new List<FORNECEDOR_SOLICITACAO_DOCUMENTOS>();
            WFD_SOL_DOCUMENTOS = new List<SolicitacaoDeDocumentos>();
            WFD_PJPF_CATEGORIA = new List<FORNECEDOR_CATEGORIA>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public int DESCRICAO_DOCUMENTO_ID { get; set; }
        public bool EXIGE_VALIDADE { get; set; }
        public int? PERIODICIDADE_ID { get; set; }
        public bool ATIVO { get; set; }
        public bool OBRIGATORIO { get; set; }
        public virtual DescricaoDeDocumentos DescricaoDeDocumentos { get; set; }
        public virtual TIPO_PERIODICIDADE WFD_T_PERIODICIDADE { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> WFD_PJPF_DOCUMENTOS { get; set; }
        public virtual ICollection<FORNECEDOR_SOLICITACAO_DOCUMENTOS> WFD_PJPF_SOLICITACAO_DOCUMENTOS { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> WFD_SOL_DOCUMENTOS { get; set; }
        public virtual ICollection<FORNECEDOR_CATEGORIA> WFD_PJPF_CATEGORIA { get; set; }
    }
}