using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Fornecedor : ISelfValidation
    {
        public Fornecedor(int id, string documento, string razaoSocial)
        {
            Id = id;
            Documento = documento;
            RazaoSocial = razaoSocial;
            Contratante = 1;
            UF = "RJ";
            RemoverCaracteresEspeciais();
        }

        public Fornecedor(int id, string documento, string razaoSocial, int contratante, string uF)
        {
            Id = id;
            Documento = documento;
            RazaoSocial = razaoSocial;
            Contratante = contratante;
            UF = uF;
            RemoverCaracteresEspeciais();
        }

        public Fornecedor(string documento, string razaoSocial)
            : this(0, documento, razaoSocial)
        {

        }

        public void RemoverCaracteresEspeciais()
        {
            if (!string.IsNullOrEmpty(Documento))
                Documento = Documento
                     .Replace(".", "")
                     .Replace("-", "")
                     .Replace("/", "");
        }

        public int Contratante { get; private set; }
        public string UF { get; private set; }
        public int Id { get; private set; }
        public string Documento { get; private set; }
        public string RazaoSocial { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new FornecedorValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }
    }
}