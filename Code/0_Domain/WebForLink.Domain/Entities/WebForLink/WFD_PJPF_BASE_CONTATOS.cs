namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDORBASE_CONTATOS
    {
        public int ID { get; set; }
        public int PJPF_BASE_ID { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string CELULAR { get; set; }
        public virtual FORNECEDORBASE WFD_PJPF_BASE { get; set; }
    }
}