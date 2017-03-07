using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Interfaces.Validation
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool EhValido { get; }
    }
}