using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_GRUPO
    {
        public TIPO_GRUPO()
        {
            WFD_SOL_MOD_DGERAIS_SEQ = new List<SOLICITACAO_MODIFICACAO_DADOSGERAIS>();
            WFD_T_DESCRICAO = new List<TIPO_DESCRICAO>();
        }

        public int ID { get; set; }
        public string GRUPO_NM { get; set; }
        public int VISAO_ID { get; set; }
        public virtual TIPO_VISAO WFD_T_VISAO { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_DADOSGERAIS> WFD_SOL_MOD_DGERAIS_SEQ { get; set; }
        public virtual ICollection<TIPO_DESCRICAO> WFD_T_DESCRICAO { get; set; }
    }
}