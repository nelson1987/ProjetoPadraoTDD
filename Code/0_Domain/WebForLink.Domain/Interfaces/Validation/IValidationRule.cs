namespace WebForLink.Domain.Interfaces.Validation
{
    public interface IValidationRule<TEntity>
    {
        string ErrorMessage { get; }
        bool Valid(TEntity entity);
    }
}