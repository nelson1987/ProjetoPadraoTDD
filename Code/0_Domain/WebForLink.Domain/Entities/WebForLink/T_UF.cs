using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TiposDeEstado
    {
        public TiposDeEstado()
        {
            WFD_PJPF_BASE_ENDERECO = new List<FORNECEDORBASE_ENDERECO>();
            WFD_PJPF_BASE = new List<FORNECEDORBASE>();
            WFD_PJPF_ENDERECO = new List<FORNECEDOR_ENDERECO>();
            WFD_PJPF = new List<Fornecedor>();
            WFD_SOL_MOD_ENDERECO = new List<SOLICITACAO_MODIFICACAO_ENDERECO>();
        }

        public string UF_SGL { get; set; }
        public string UF_NM { get; set; }
        public virtual ICollection<FORNECEDORBASE_ENDERECO> WFD_PJPF_BASE_ENDERECO { get; set; }
        public virtual ICollection<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
        public virtual ICollection<FORNECEDOR_ENDERECO> WFD_PJPF_ENDERECO { get; set; }
        public virtual ICollection<Fornecedor> WFD_PJPF { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_ENDERECO> WFD_SOL_MOD_ENDERECO { get; set; }
    }
}