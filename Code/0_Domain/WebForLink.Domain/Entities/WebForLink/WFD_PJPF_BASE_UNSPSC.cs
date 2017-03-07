using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDORBASE_UNSPSC
    {
        public int ID { get; set; }
        public int PJPF_BASE_ID { get; set; }
        public int? UNSPSC_ID { get; set; }
        public DateTime? DT_INCLUSAO { get; set; }
        public DateTime? DT_EXCLUSAO { get; set; }
        public virtual TIPO_UNSPSC T_UNSPSC { get; set; }
        public virtual FORNECEDORBASE WFD_PJPF_BASE { get; set; }
    }
}