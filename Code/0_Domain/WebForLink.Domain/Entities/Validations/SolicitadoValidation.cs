using WebForLink.Domain.Entities.Specifications;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.Validations
{
    public sealed class SolicitadoValidacao : Validation<Solicitado>
    {
        public SolicitadoValidacao()
        {
            AddRule(new ValidationRule<Solicitado>(new SolicitadoDeveTerCnpjPreenchido(),
                ValidationMessages.FornecedorIsRequired));
            AddRule(new ValidationRule<Solicitado>(new SolicitadoDeveterAoMenosUmResponsavel(),
                ValidationMessages.FornecedorIsRequired));
            AddRule(new ValidationRule<Solicitado>(new SolicitadoDeveTerResponsaveisValidos(),
                ValidationMessages.FornecedorIsRequired));
        }
    }
}