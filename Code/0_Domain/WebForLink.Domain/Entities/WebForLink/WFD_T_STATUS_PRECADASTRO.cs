using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_STATUS_PRECADASTRO
    {
        public TIPO_STATUS_PRECADASTRO()
        {
            WFD_PJPF_BASE = new List<FORNECEDORBASE>();
        }

        public int ID { get; set; }
        public string STATUS_NM { get; set; }
        public virtual ICollection<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
    }
}