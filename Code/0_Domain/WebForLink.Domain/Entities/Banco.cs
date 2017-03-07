using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Banco : ISelfValidation
    {
        public int Id { get; set; }
        public int IdFichaCadastral { get; set; }
        public int Numero { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDv { get; set; }
        public string Conta { get; set; }
        public string ContaDv { get; set; }
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