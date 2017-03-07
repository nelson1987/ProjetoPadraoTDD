namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_MODIFICACAO_DADOSGERAIS
    {
        public int ID { get; set; }
        public int VISAO_ID { get; set; }
        public int GRUPO_ID { get; set; }
        public int DESCRICAO_ID { get; set; }
        public string DESCRICAOALTERACAO { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_DESCRICAO WFD_T_DESCRICAO { get; set; }
        public virtual TIPO_GRUPO WFD_T_GRUPO { get; set; }
        public virtual TIPO_VISAO WFD_T_VISAO { get; set; }
    }
}