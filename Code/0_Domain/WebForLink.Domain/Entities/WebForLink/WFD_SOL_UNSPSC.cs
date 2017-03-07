namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_UNSPSC
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public int UNSPSC_ID { get; set; }
        public virtual TIPO_UNSPSC T_UNSPSC { get; set; }
        public virtual TIPO_UNSPSC T_UNSPSC1 { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
    }
}