using WebForLink.Domain.Interfaces.Specification;

namespace WebForLink.Domain.Entities.Specifications
{
    public class CategoriaArtUrlLenthMustBeLowerThan1024Spec : ISpecification<Categoria>
    {
        public bool IsSatisfiedBy(Categoria album)
        {
            return !string.IsNullOrEmpty(album.Descricao) ? album.Descricao.Trim().Length < 1024 : false;
        }
    }

    public class CategoriaDescricaoNaoPodeEstarNulaSpec : ISpecification<Categoria>
    {
        public bool IsSatisfiedBy(Categoria entity)
        {
            return !string.IsNullOrEmpty(entity.Descricao);
        }
    }
}