using WebForLink.Domain.Interfaces.Specification;

namespace WebForLink.Domain.Entities.Specifications
{
    public class SolicitadoDeveTerCnpjPreenchido : ISpecification<Solicitado>
    {
        public bool IsSatisfiedBy(Solicitado entity)
        {
            return !string.IsNullOrEmpty(entity.Cnpj);
        }
    }

    public class SolicitadoDeveterAoMenosUmResponsavel : ISpecification<Solicitado>
    {
        public bool IsSatisfiedBy(Solicitado entity)
        {
            return !(entity.Responsaveis.Count == 0 || entity.Responsaveis == null);
        }
    }

    public class SolicitadoDeveTerResponsaveisValidos : ISpecification<Solicitado>
    {
        public bool IsSatisfiedBy(Solicitado entity)
        {
            foreach (var item in entity.Responsaveis)
            {
                if (!item.EhValido)
                    return false;
            }
            return true;
        }
    }
}