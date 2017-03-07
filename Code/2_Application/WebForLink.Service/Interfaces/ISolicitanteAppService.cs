using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface ISolicitanteAppService : IAppService<ListaDocumento>
    {
        ValidationResult CriarSolicitante(Solicitante solicitante);
    }
}