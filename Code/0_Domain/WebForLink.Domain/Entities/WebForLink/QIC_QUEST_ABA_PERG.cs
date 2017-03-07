using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class QUESTIONARIO_PERGUNTA
    {
        public QUESTIONARIO_PERGUNTA()
        {
            QIC_QUEST_ABA_PERG_PAPEL = new List<QUESTIONARIO_PAPEL>();
            QIC_QUEST_ABA_PERG_RESP = new List<QUESTIONARIO_RESPOSTA>();
            WFD_INFORM_COMPL = new List<WFD_INFORM_COMPL>();
            WFD_PJPF_INFORM_COMPL = new List<FORNECEDOR_INFORM_COMPL>();
        }

        public int ID { get; set; }
        public int QUEST_ABA_ID { get; set; }
        public string PERG_NM { get; set; }
        public string TP_DADO { get; set; }
        public string EXIBE_NM { get; set; }
        public bool? DOMINIO { get; set; }
        public int ORDEM { get; set; }
        public int? RESP_TAMANHO { get; set; }
        public bool E_PAI { get; set; }
        public int? PERG_PAI { get; set; }
        public virtual QUESTIONARIO_ABA QIC_QUEST_ABA { get; set; }
        public virtual ICollection<QUESTIONARIO_PAPEL> QIC_QUEST_ABA_PERG_PAPEL { get; set; }
        public virtual ICollection<QUESTIONARIO_RESPOSTA> QIC_QUEST_ABA_PERG_RESP { get; set; }
        public virtual ICollection<WFD_INFORM_COMPL> WFD_INFORM_COMPL { get; set; }
        public virtual ICollection<FORNECEDOR_INFORM_COMPL> WFD_PJPF_INFORM_COMPL { get; set; }
    }
}