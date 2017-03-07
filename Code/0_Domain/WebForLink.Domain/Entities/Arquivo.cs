using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Arquivo : ISelfValidation
    {
        public int Id { get; set; }
        public int IdDocumentoAnexado { get; set; }
        public string NomeOriginal { get; set; }
        public bool JaBaixado { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string LocalArquivo { get; set; }
        public virtual DocumentoAnexado DocumentoAnexado { get; set; }

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