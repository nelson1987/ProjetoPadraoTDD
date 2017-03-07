using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_STATUS
    {
        public SOLICITACAO_STATUS()
        {
            WFD_SOLICITACAO = new List<SOLICITACAO>();
            WFD_SOLICITACAO_TRAMITE = new List<SOLICITACAO_TRAMITE>();
        }

        public int ID { get; set; }
        public string NOME { get; set; }
        public virtual ICollection<SOLICITACAO> WFD_SOLICITACAO { get; set; }
        public virtual ICollection<SOLICITACAO_TRAMITE> WFD_SOLICITACAO_TRAMITE { get; set; }
    }
}