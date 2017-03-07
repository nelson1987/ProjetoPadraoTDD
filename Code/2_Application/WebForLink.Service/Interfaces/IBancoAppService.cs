using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface IBancoAppService : IAppService<Banco>
    {
        ValidationResult UpdateOrCreate(List<Banco> entity);
        ValidationResult Remove(List<Banco> endereco);
    }
}