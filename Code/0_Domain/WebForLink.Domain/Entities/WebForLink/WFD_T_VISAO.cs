using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_VISAO
    {
        public TIPO_VISAO()
        {
            WFD_SOL_MOD_DGERAIS_SEQ = new List<SOLICITACAO_MODIFICACAO_DADOSGERAIS>();
            WFD_T_GRUPO = new List<TIPO_GRUPO>();
        }

        public int ID { get; set; }
        public string VISAO_NM { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_DADOSGERAIS> WFD_SOL_MOD_DGERAIS_SEQ { get; set; }
        public virtual ICollection<TIPO_GRUPO> WFD_T_GRUPO { get; set; }
    }
}