namespace WebForLink.Domain.Entities.WebForLink
{
    public class WFD_PJPF_CONTRATANTE_ORG_COMPRAS
    {
        public int ID { get; set; }
        public int CONTRAT_ORG_COMPRAS_ID { get; set; }
        public int PJPF_ID { get; set; }
        public int? PJPF_CONTATO_ID { get; set; }
        public virtual CONTRATANTE_ORGANIZACAO_COMPRAS WFD_CONTRATANTE_ORG_COMPRAS { get; set; }
        public virtual Fornecedor WFD_PJPF { get; set; }
    }
}