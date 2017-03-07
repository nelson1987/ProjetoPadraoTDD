using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface IContatoAppService : IAppService<Contato>
    {
        ValidationResult UpdateOrCreate(List<Contato> entity);
        ValidationResult Remove(List<Contato> endereco);
    }
}