using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class ListasSolicitante : ISelfValidation
    {
        public ListasSolicitante()
        {
            ListaDocumento = new List<ListaDocumento>();
        }

        public int Id { get; set; }
        public int IdSolicitante { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<ListaDocumento> ListaDocumento { get; set; }
        public virtual Solicitante Solicitante { get; set; }

        public bool EhValido
        {
            get
            {
                //var validacaoExterna = new SolicitacaoValidacao();
                //ValidationResult = validacaoExterna.Validar(this);
                return true;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}