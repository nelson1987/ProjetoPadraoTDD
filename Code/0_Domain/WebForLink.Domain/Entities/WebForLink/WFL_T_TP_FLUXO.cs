using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TipoDeFluxo
    {
        public TipoDeFluxo()
        {
            Fluxo = new List<Fluxo>();
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Fluxo> Fluxo { get; set; }
    }
}