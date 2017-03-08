using WebForLink.Domain.Interfaces.Specification;

namespace WebForLink.Domain.Entities.Specifications
{
    public class UsuarioDeveTerLoginPreenchido : ISpecification<Usuario>
    {
        public bool IsSatisfiedBy(Usuario entity)
        {
            return !string.IsNullOrEmpty(entity.Login);
        }
    }
}