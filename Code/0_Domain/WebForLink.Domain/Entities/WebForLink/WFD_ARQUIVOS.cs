using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class ARQUIVOS
    {
        public ARQUIVOS()
        {
            WFD_PJPF_BANCO = new List<BancoDoFornecedor>();
            WFD_PJPF_DOCUMENTOS = new List<DocumentosDoFornecedor>();
            WFD_SOL_DOCUMENTOS = new List<SolicitacaoDeDocumentos>();
            WFD_SOL_MOD_BANCO = new List<SolicitacaoModificacaoDadosBancario>();
        }

        public int ID { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public string TIPO_ARQUIVO { get; set; }
        public DateTime DATA_UPLOAD { get; set; }
        public long TAMANHO { get; set; }
        public string CAMINHO { get; set; }
        public virtual ICollection<BancoDoFornecedor> WFD_PJPF_BANCO { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> WFD_PJPF_DOCUMENTOS { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> WFD_SOL_DOCUMENTOS { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosBancario> WFD_SOL_MOD_BANCO { get; set; }
    }
}