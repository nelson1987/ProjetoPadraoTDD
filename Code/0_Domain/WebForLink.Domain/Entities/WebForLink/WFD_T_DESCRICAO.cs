using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_DESCRICAO
    {
        public TIPO_DESCRICAO()
        {
            WFD_SOL_MOD_DGERAIS_SEQ = new List<SOLICITACAO_MODIFICACAO_DADOSGERAIS>();
        }

        public int ID { get; set; }
        public string DESCRICAO_NM { get; set; }
        public int GRUPO_ID { get; set; }
        public virtual TIPO_GRUPO WFD_T_GRUPO { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_DADOSGERAIS> WFD_SOL_MOD_DGERAIS_SEQ { get; set; }
    }
}