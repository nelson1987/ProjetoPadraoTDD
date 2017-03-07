namespace WebForLink.Domain.Entities.WebForLink
{
    public class FLUXO_SEQUENCIA_PRE_REQUIS
    {
        public int FLUXO_SEQ_ID { get; set; }
        public int PAPEL_ID { get; set; }
        public virtual Papel Papel { get; set; }
    }
}