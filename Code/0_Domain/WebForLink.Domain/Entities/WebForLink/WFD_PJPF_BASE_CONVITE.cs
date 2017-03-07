using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDORBASE_CONVITE
    {
        public int ID { get; set; }
        public int PJPF_BASE_ID { get; set; }
        public DateTime DT_ENVIO { get; set; }
        public int USUARIO_ID { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public virtual FORNECEDORBASE WFD_PJPF_BASE { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
    }
}