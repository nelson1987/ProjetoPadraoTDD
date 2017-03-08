using WebForLink.Domain.Entities.Specifications;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.Validations
{
    public sealed class UsuarioValidation : Validation<Usuario>
    {
        public UsuarioValidation()
        {
            AddRule(new ValidationRule<Usuario>(new UsuarioDeveTerLoginPreenchido(),
                ValidationMessages.FornecedorIsRequired));
        }
    }
}