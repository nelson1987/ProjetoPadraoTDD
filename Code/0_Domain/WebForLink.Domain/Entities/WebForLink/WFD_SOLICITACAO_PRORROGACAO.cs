using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_PRORROGACAO
    {
        public int ID { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public DateTime DT_PRORROGACAO_PRAZO { get; set; }
        public string MOTIVO_PRORROGACAO { get; set; }
        public DateTime DT_SOL_PRORROGACAO { get; set; }
        public int USUARIO_SOL_ID { get; set; }
        public bool? APROVADO { get; set; }
        public string MOTIVO_REPROVACAO { get; set; }
        public DateTime? DT_AVALIACAO { get; set; }
        public int? USUARIO_AVALIACAO_ID { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
    }
}