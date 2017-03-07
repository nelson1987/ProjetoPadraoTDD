using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class QUESTIONARIO_ABA
    {
        public QUESTIONARIO_ABA()
        {
            QIC_QUEST_ABA_PERG = new List<QUESTIONARIO_PERGUNTA>();
        }

        public int ID { get; set; }
        public string ABA_NM { get; set; }
        public int QUESTIONARIO_ID { get; set; }
        public string ABA_DSC { get; set; }
        public int ORDEM { get; set; }
        public virtual ICollection<QUESTIONARIO_PERGUNTA> QIC_QUEST_ABA_PERG { get; set; }
        public virtual QUESTIONARIO QIC_QUESTIONARIO { get; set; }
    }
}