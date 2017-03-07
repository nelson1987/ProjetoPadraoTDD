using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_PERIODICIDADE
    {
        public TIPO_PERIODICIDADE()
        {
            WFD_PJPF_DOCUMENTOS = new List<DocumentosDoFornecedor>();
            WFD_PJPF_LISTA_DOCUMENTOS = new List<ListaDeDocumentosDeFornecedor>();
            WFD_SOL_DOCUMENTOS = new List<SolicitacaoDeDocumentos>();
        }

        public int ID { get; set; }
        public string PERIODICIDADE_NM { get; set; }
        public int DIAS { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> WFD_PJPF_DOCUMENTOS { get; set; }
        public virtual ICollection<ListaDeDocumentosDeFornecedor> WFD_PJPF_LISTA_DOCUMENTOS { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> WFD_SOL_DOCUMENTOS { get; set; }
    }
}