using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_STATUS
    {
        public FORNECEDOR_STATUS()
        {
            WFD_CONTRATANTE_PJPF = new List<WFD_CONTRATANTE_PJPF>();
        }

        public int ID { get; set; }
        public string STATUS_NM { get; set; }
        public virtual ICollection<WFD_CONTRATANTE_PJPF> WFD_CONTRATANTE_PJPF { get; set; }
    }
}