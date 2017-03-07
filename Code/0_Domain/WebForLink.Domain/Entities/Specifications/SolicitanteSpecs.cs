using WebForLink.Domain.Interfaces.Specification;

namespace WebForLink.Domain.Entities.Specifications
{
    public class SolicitanteDeveTerLoginPreenchido : ISpecification<Solicitante>
    {
        public bool IsSatisfiedBy(Solicitante entity)
        {
            return !string.IsNullOrEmpty(entity.Login);
        }
    }

    public class SolicitanteDeveTerEmailPreenchido : ISpecification<Solicitante>
    {
        public bool IsSatisfiedBy(Solicitante entity)
        {
            return !string.IsNullOrEmpty(entity.Email);
        }
    }

    public class SolicitanteDeveTerCodigoClientePreenchido : ISpecification<Solicitante>
    {
        public bool IsSatisfiedBy(Solicitante entity)
        {
            return !string.IsNullOrEmpty(entity.CodigoCliente);
        }
    }
}