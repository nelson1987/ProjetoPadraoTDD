using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IContratanteOrganizacaoCompraWebForLinkRepository : IRepository<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
        List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante);
    }
}