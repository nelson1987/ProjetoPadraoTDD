using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class CONTRATANTE_ORGANIZACAO_COMPRAS
    {
        public CONTRATANTE_ORGANIZACAO_COMPRAS()
        {
            WFD_PJPF_CONTRATANTE_ORG_COMPRAS = new List<WFD_PJPF_CONTRATANTE_ORG_COMPRAS>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public string ORG_COMPRAS_COD { get; set; }
        public string ORG_COMPRAS_DSC { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual ICollection<WFD_PJPF_CONTRATANTE_ORG_COMPRAS> WFD_PJPF_CONTRATANTE_ORG_COMPRAS { get; set; }
    }
}