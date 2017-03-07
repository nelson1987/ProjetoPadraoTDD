using WebForLink.Domain.Interfaces.Specification;

namespace WebForLink.Domain.Entities.Specifications
{
    public class ResponsavelDeveTerONomePreenchido : ISpecification<Responsavel>
    {
        public bool IsSatisfiedBy(Responsavel entity)
        {
            return !string.IsNullOrEmpty(entity.Nome);
        }
    }

    public class ResponsavelDeveTerOEmailPreenchido : ISpecification<Responsavel>
    {
        public bool IsSatisfiedBy(Responsavel entity)
        {
            return !string.IsNullOrEmpty(entity.Email);
        }
    }
}