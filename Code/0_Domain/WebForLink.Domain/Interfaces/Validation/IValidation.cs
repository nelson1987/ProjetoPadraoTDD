using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Interfaces.Validation
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Validar(TEntity entity);
    }
}