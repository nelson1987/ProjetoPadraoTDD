using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface ISolicitadoAppService : IAppService<Solicitado>
    {
        ValidationResult CriarSolicitado(Solicitado solicitado, List<Responsavel> responsaveis);
    }
}