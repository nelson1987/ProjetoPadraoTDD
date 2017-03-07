using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_CATEGORIA_CH
    {
        public FORNECEDOR_CATEGORIA_CH()
        {
            WFD_PJPF_CATEGORIA = new List<FORNECEDOR_CATEGORIA>();
        }

        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public virtual ICollection<FORNECEDOR_CATEGORIA> WFD_PJPF_CATEGORIA { get; set; }
    }
}