using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class GRUPO
    {
        public GRUPO()
        {
            WFD_CONTRATANTE = new List<Contratante>();
        }

        public int ID { get; set; }
        public string GRUPO_NM { get; set; }
        public virtual ICollection<Contratante> WFD_CONTRATANTE { get; set; }
    }
}