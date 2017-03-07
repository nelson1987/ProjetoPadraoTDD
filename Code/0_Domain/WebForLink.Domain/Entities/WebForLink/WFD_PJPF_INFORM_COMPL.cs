namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_INFORM_COMPL
    {
        public int ID { get; set; }
        public int CONTRATANTE_PJPF_ID { get; set; }
        public int PERG_ID { get; set; }
        public string RESPOSTA { get; set; }
        public virtual QUESTIONARIO_PERGUNTA QIC_QUEST_ABA_PERG { get; set; }
        public virtual WFD_CONTRATANTE_PJPF WFD_CONTRATANTE_PJPF { get; set; }
    }
}