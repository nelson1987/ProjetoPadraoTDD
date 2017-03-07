namespace WebForLink.Domain.Entities.WebForLink
{
    public class QUESTIONARIO_PAPEL
    {
        public int ID { get; set; }
        public int PERG_ID { get; set; }
        public int PAPEL_ID { get; set; }
        public bool LEITURA { get; set; }
        public bool ESCRITA { get; set; }
        public bool OBRIG { get; set; }
        public virtual QUESTIONARIO_PERGUNTA QIC_QUEST_ABA_PERG { get; set; }
        public virtual Papel Papel { get; set; }
    }
}