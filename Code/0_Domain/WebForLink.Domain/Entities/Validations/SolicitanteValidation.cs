using WebForLink.Domain.Entities.Specifications;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.Validations
{
    public sealed class SolicitanteValidacao : Validation<Solicitante>
    {
        public SolicitanteValidacao()
        {
            AddRule(new ValidationRule<Solicitante>(new SolicitanteDeveTerLoginPreenchido(),
                ValidationMessages.FornecedorIsRequired));
            AddRule(new ValidationRule<Solicitante>(new SolicitanteDeveTerEmailPreenchido(),
                ValidationMessages.FornecedorIsRequired));
            AddRule(new ValidationRule<Solicitante>(new SolicitanteDeveTerCodigoClientePreenchido(),
                ValidationMessages.FornecedorIsRequired));
        }
    }
}