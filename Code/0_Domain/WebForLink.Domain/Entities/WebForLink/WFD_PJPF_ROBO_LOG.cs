using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class ROBO_LOG
    {
        public int ID { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public DateTime? DATA { get; set; }
        public string ROBO { get; set; }
        public int? COD_RETORNO { get; set; }
        public string MENSAGEM { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public int? PJPF_BASE_ID { get; set; }
        public int? PJPF_ID { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual Fornecedor WFD_PJPF { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
    }
}