using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_ENDERECO
    {
        public TIPO_ENDERECO()
        {
            WFD_PJPF_BASE_ENDERECO = new List<FORNECEDORBASE_ENDERECO>();
            WFD_PJPF_ENDERECO = new List<FORNECEDOR_ENDERECO>();
            WFD_SOL_MOD_ENDERECO = new List<SOLICITACAO_MODIFICACAO_ENDERECO>();
        }

        public int ID { get; set; }
        public string NM_TP_ENDERECO { get; set; }
        public virtual ICollection<FORNECEDORBASE_ENDERECO> WFD_PJPF_BASE_ENDERECO { get; set; }
        public virtual ICollection<FORNECEDOR_ENDERECO> WFD_PJPF_ENDERECO { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_ENDERECO> WFD_SOL_MOD_ENDERECO { get; set; }
    }
}