using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_MENSAGEM
    {
        public SOLICITACAO_MENSAGEM()
        {
            WFD_SOL_DOCUMENTOS = new List<SolicitacaoDeDocumentos>();
        }

        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public string ASSUNTO { get; set; }
        public string MENSAGEM { get; set; }
        public DateTime? DT_ENVIO { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> WFD_SOL_DOCUMENTOS { get; set; }
    }
}