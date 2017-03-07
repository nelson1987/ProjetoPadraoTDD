using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_SOLICITACAO
    {
        public FORNECEDOR_SOLICITACAO()
        {
            WFD_PJPF_SOLICITACAO_DOCUMENTOS = new List<FORNECEDOR_SOLICITACAO_DOCUMENTOS>();
        }

        public int ID { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public DateTime? DATA_SOLICITACAO { get; set; }
        public string ASSUNTO { get; set; }
        public string DESCRICAO_SOLICITACAO { get; set; }
        public DateTime? DT_NASCIMENTO { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual ICollection<FORNECEDOR_SOLICITACAO_DOCUMENTOS> WFD_PJPF_SOLICITACAO_DOCUMENTOS { get; set; }
    }
}