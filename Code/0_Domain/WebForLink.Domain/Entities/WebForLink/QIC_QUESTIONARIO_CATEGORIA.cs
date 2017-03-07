namespace WebForLink.Domain.Entities.WebForLink
{
    public class QUESTIONARIO_CATEGORIA
    {
        public int ID { get; set; }
        public int QUESTIONARIO_ID { get; set; }
        public int CATEGORIA_ID { get; set; }
        public virtual QUESTIONARIO QIC_QUESTIONARIO { get; set; }
        public virtual FORNECEDOR_CATEGORIA WFD_PJPF_CATEGORIA { get; set; }
    }
}