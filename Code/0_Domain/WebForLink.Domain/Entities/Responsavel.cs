using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Responsavel : ISelfValidation
    {
        private Responsavel()
        {
        }

        public Responsavel(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int IdSolicitado { get; set; }
        public virtual Solicitado Solicitado { get; set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new ResponsavelValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}