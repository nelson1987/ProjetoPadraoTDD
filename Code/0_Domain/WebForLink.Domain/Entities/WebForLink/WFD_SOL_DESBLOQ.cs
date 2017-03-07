namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_DESBLOQUEIO
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public bool? BLQ_LANCAMENTO_EMP { get; set; }
        public bool? BLQ_LANCAMENTO_TODAS_EMP { get; set; }
        public bool? BLQ_COMPRAS_TODAS_ORG_COMPRAS { get; set; }
        public int? BLQ_QUALIDADE_FUNCAO_BQL_ID { get; set; }
        public string BLQ_MOTIVO_DSC { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_FUNCAO_BLOQUEIO WFD_T_FUNCAO_BLOQUEIO { get; set; }
    }
}