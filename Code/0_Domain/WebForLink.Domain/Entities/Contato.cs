using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Contato : ISelfValidation
    {
        public int Id { get; set; }
        public int IdFichaCadastral { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }

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