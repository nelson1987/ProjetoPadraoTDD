using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class DescricaoDeDocumentos
    {
        public DescricaoDeDocumentos()
        {
            DocumentosDeFornecedor = new List<DocumentosDoFornecedor>();
            ListaDeDocumentosDeFornecedor = new List<ListaDeDocumentosDeFornecedor>();
            SolicitacaoDeDocumentos = new List<SolicitacaoDeDocumentos>();
        }

        public int ID { get; set; }
        public int TIPO_DOCUMENTOS_ID { get; set; }
        public string DESCRICAO { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public int? DESCRICAO_DOCUMENTOS_CH_ID { get; set; }
        public bool ATIVO { get; set; }
        public virtual Contratante Contratante { get; set; }
        public virtual WFD_DESCRICAO_DOCUMENTOS_CH WFD_DESCRICAO_DOCUMENTOS_CH { get; set; }
        public virtual TipoDeDocumento TipoDeDocumento { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> DocumentosDeFornecedor { get; set; }
        public virtual ICollection<ListaDeDocumentosDeFornecedor> ListaDeDocumentosDeFornecedor { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> SolicitacaoDeDocumentos { get; set; }
    }
}