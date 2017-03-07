namespace WebForLink.Domain.Entities.WebForLink
{
    public class DocumentosCompartilhados
    {
        public int CONTRATANTE_ID { get; set; }
        public int PJPF_DOCUMENTO_ID { get; set; }
        public int COMPARTILHAMENTO_ID { get; set; }
        public virtual Compartilhamentos Compartilhamentos { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual DocumentosDoFornecedor DocumentosDoFornecedor { get; set; }
    }
}