namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_MODIFICACAO_ENDERECO
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
        public int? SOLICITACAO_ID { get; set; }
        public int? PJPF_ID { get; set; }
        public int? PJPF_ENDERECO_ID { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public virtual TiposDeEstado T_UF { get; set; }
        public virtual Fornecedor WFD_PJPF { get; set; }
        public virtual FORNECEDOR_ENDERECO WFD_PJPF_ENDERECO { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_ENDERECO WFD_T_TP_ENDERECO { get; set; }
    }
}