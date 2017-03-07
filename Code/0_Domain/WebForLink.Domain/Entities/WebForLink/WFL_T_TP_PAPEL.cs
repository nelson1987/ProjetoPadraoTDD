using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TipoDePapel : ISelfValidation
    {
        public TipoDePapel()
        {
            WFL_PAPEL = new List<Papel>();
        }

        public int ID { get; set; }
        public string TP_PAPEL_NM { get; set; }
        public virtual ICollection<Papel> WFL_PAPEL { get; set; }

        public bool EhValido
        {
            get { return true; }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}