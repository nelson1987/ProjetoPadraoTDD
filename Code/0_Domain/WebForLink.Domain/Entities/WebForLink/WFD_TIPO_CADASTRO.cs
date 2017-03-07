using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_CADASTRO_FORNECEDOR
    {
        public TIPO_CADASTRO_FORNECEDOR()
        {
            WFD_CONTRATANTE = new List<Contratante>();
        }

        public int ID { get; set; }
        public string NOME { get; set; }
        public virtual ICollection<Contratante> WFD_CONTRATANTE { get; set; }
    }
}