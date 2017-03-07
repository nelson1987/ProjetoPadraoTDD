namespace WebForLink.Domain.Entities.WebForLink
{
    public class WFD_INFORM_COMPL
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public int PERG_ID { get; set; }
        public string RESPOSTA { get; set; }
        public virtual QUESTIONARIO_PERGUNTA QIC_QUEST_ABA_PERG { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
    }
}