using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_FORNECEDOR
    {
        public TIPO_FORNECEDOR()
        {
            WFD_CONTRATANTE_PJPF = new List<WFD_CONTRATANTE_PJPF>();
        }

        public int ID { get; set; }
        public string TP_CONTRATANTE_NM { get; set; }
        public virtual ICollection<WFD_CONTRATANTE_PJPF> WFD_CONTRATANTE_PJPF { get; set; }
    }
}