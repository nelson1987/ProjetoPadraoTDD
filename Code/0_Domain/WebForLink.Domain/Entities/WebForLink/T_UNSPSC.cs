using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_UNSPSC
    {
        public TIPO_UNSPSC()
        {
            WFD_PJPF_BASE_UNSPSC = new List<FORNECEDORBASE_UNSPSC>();
            WFD_PJPF_UNSPSC = new List<FORNECEDOR_UNSPSC>();
            WFD_SOL_UNSPSC = new List<SOLICITACAO_UNSPSC>();
            WFD_SOL_UNSPSC1 = new List<SOLICITACAO_UNSPSC>();
        }

        public int ID { get; set; }
        public int UNSPSC_COD { get; set; }
        public string UNSPSC_DSC { get; set; }
        public int? NIV { get; set; }
        public virtual ICollection<FORNECEDORBASE_UNSPSC> WFD_PJPF_BASE_UNSPSC { get; set; }
        public virtual ICollection<FORNECEDOR_UNSPSC> WFD_PJPF_UNSPSC { get; set; }
        public virtual ICollection<SOLICITACAO_UNSPSC> WFD_SOL_UNSPSC { get; set; }
        public virtual ICollection<SOLICITACAO_UNSPSC> WFD_SOL_UNSPSC1 { get; set; }
    }
}