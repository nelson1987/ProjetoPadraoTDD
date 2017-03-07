using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class ListaDocumento : ISelfValidation
    {
        public int Id { get; set; }
        public int IdListaSolicitante { get; set; }
        public int IdTipoDocumentoCH { get; set; }
        public int IdDescricaoDocumentoCH { get; set; }
        public string Descricao { get; set; }
        public virtual ListasSolicitante ListaSolicitante { get; set; }

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