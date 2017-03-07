using WebForLink.Domain.Entities.Specifications;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.Validations
{
    public sealed class CategoriaValidacao : Validation<Categoria>
    {
        public CategoriaValidacao()
        {
            //if (string.IsNullOrEmpty(Descricao))
            //    throw new Exception("Descrição não pode estar vazia.");
            AddRule(new ValidationRule<Categoria>(new CategoriaDescricaoNaoPodeEstarNulaSpec(),
                ValidationMessages.TitleIsRequired));
            //if (string.IsNullOrEmpty(Codigo))
            //    throw new Exception("Código não pode estar vazio.");
            //base.AddRule(new ValidationRule<Categoria>(new CategoriaPriceIsRequiredSpec(), ValidationMessages.PriceIsRequired));
            //base.AddRule(new ValidationRule<Categoria>(new CategoriaPriceMustBeLowerThan100Spec(), ValidationMessages.PriceMustBeBetween001And100));
            //base.AddRule(new ValidationRule<Categoria>(new CategoriaTitleLenthMustBeLowerThan160Spec(), ValidationMessages.AlbumTitleLenthMustBeLowerThan160));
            AddRule(new ValidationRule<Categoria>(new CategoriaArtUrlLenthMustBeLowerThan1024Spec(),
                ValidationMessages.AlbumArtUrlLengthMustBeLowerThan1024));
        }
    }
}