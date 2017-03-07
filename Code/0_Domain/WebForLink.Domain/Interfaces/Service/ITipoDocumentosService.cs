using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Interfaces.Service
{
    public interface ITipoDocumentosService : IService<TipoDeDocumento>
    {
        List<TipoDeDocumento> ListarPorIdContratante(int contratanteId);
    }
}