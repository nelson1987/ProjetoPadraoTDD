using WebForLink.Domain.Entities.Specifications;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.Validations
{
    public sealed class ResponsavelValidacao : Validation<Responsavel>
    {
        public ResponsavelValidacao()
        {
            AddRule(new ValidationRule<Responsavel>(new ResponsavelDeveTerONomePreenchido(),
                ValidationMessages.FornecedorIsRequired));
            AddRule(new ValidationRule<Responsavel>(new ResponsavelDeveTerOEmailPreenchido(),
                ValidationMessages.FornecedorIsRequired));
        }
    }
}