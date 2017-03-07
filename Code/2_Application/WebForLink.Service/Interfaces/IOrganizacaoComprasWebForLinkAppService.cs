using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface IOrganizacaoComprasWebForLinkAppService : IAppService<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
        CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorId(int id);
        List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante);
    }
}
