using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Usuario : ISelfValidation
    {
        protected Usuario()
        {
        }

        public Usuario(string codigoCliente, string login, string email)
            : this()
        {
            CodigoCliente = codigoCliente;
            Login = login;
            Email = email;
        }

        public int Id { get; set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string CodigoCliente { get; private set; }
        public string NomeEmpresa { get; private set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new UsuarioValidation();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}