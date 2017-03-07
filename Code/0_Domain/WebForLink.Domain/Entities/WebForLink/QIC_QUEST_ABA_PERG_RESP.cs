using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class QUESTIONARIO_RESPOSTA
    {
        public QUESTIONARIO_RESPOSTA()
        {
            QIC_QUEST_ABA_PERG_RESP1 = new List<QUESTIONARIO_RESPOSTA>();
        }

        public int ID { get; set; }
        public int PERG_ID { get; set; }
        public int? RESP_PAI_ID { get; set; }
        public string RESP_COD { get; set; }
        public string RESP_DSC { get; set; }
        public int? ORDEM { get; set; }
        public virtual QUESTIONARIO_PERGUNTA QIC_QUEST_ABA_PERG { get; set; }
        public virtual ICollection<QUESTIONARIO_RESPOSTA> QIC_QUEST_ABA_PERG_RESP1 { get; set; }
        public virtual QUESTIONARIO_RESPOSTA QIC_QUEST_ABA_PERG_RESP2 { get; set; }
    }
}