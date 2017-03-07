using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_FUNCAO_BLOQUEIO
    {
        public TIPO_FUNCAO_BLOQUEIO()
        {
            WFD_SOL_BLOQ = new List<SOLICITACAO_BLOQUEIO>();
            WFD_SOL_DESBLOQ = new List<SOLICITACAO_DESBLOQUEIO>();
        }

        public int ID { get; set; }
        public string FUNCAO_BLOQ_COD { get; set; }
        public string FUNCAO_BLOQ_DSC { get; set; }
        public virtual ICollection<SOLICITACAO_BLOQUEIO> WFD_SOL_BLOQ { get; set; }
        public virtual ICollection<SOLICITACAO_DESBLOQUEIO> WFD_SOL_DESBLOQ { get; set; }
    }
}