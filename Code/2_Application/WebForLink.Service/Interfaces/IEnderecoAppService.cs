using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface IEnderecoAppService : IAppService<Endereco>//, IReadOnlyAppService<Endereco>
    {
        ValidationResult UpdateOrCreate(List<Endereco> entity);
        ValidationResult Remove(ICollection<Endereco> endereco);
    }
}