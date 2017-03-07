namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDORBASE_ENDERECO
    {
        public int ID { get; set; }
        public int TP_ENDERECO_ID { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string UF { get; set; }
        public string PAIS { get; set; }
        public int PJPF_BASE_ID { get; set; }
        public virtual TiposDeEstado T_UF { get; set; }
        public virtual FORNECEDORBASE WFD_PJPF_BASE { get; set; }
        public virtual TIPO_ENDERECO WFD_T_TP_ENDERECO { get; set; }
    }
}