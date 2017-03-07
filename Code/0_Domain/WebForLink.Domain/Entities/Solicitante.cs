using System.Collections.Generic;
using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Solicitante : ISelfValidation
    {
        public Solicitante()
        {
            ListasDocumentosSolicitante = new List<ListasSolicitante>();
            Solicitacao = new List<Solicitacao>();
        }

        public Solicitante(string codigoCliente, string login, string email)
            : this()
        {
            CodigoCliente = codigoCliente;
            Login = login;
            Email = email;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string CodigoCliente { get; set; }
        public string NomeEmpresa { get; set; }
        public virtual ICollection<ListasSolicitante> ListasDocumentosSolicitante { get; set; }
        public virtual ICollection<Solicitacao> Solicitacao { get; set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new SolicitanteValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}