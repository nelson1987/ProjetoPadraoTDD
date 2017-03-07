using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class QUESTIONARIO
    {
        public QUESTIONARIO()
        {
            QIC_QUEST_ABA = new List<QUESTIONARIO_ABA>();
            QIC_QUESTIONARIO_CATEGORIA = new List<QUESTIONARIO_CATEGORIA>();
        }

        public int ID { get; set; }
        public string QUEST_NM { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public string QUEST_DSC { get; set; }
        public bool? LE_D_BANCARIO { get; set; }
        public bool? LE_D_CONTATO { get; set; }
        public bool? LE_D_GERAIS { get; set; }
        public bool LE_INFO_COMPL { get; set; }
        public virtual ICollection<QUESTIONARIO_ABA> QIC_QUEST_ABA { get; set; }
        public virtual ICollection<QUESTIONARIO_CATEGORIA> QIC_QUESTIONARIO_CATEGORIA { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
    }
}