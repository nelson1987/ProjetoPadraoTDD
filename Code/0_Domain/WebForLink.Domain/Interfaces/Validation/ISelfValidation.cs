using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Interfaces.Validation
{
    public interface ISelfValidation
    {
        int Id { get; set; }
        ValidationResult ValidationResult { get; }
        bool EhValido { get; }
    }
}