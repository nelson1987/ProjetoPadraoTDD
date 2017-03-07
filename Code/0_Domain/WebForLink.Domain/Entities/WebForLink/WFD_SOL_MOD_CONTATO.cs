namespace WebForLink.Domain.Entities.WebForLink
{
    public class SolicitacaoModificacaoDadosContato
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string CELULAR { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public int? PJPF_ID { get; set; }
        public int? CONTATO_PJPF_ID { get; set; }
        public int? TP_CONTATO_ID { get; set; }
        public virtual FORNECEDOR_CONTATOS WFD_PJPF_CONTATOS { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_CONTATO WFD_T_TP_CONTATO { get; set; }
    }
}