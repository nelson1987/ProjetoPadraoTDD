using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class CategoriaWebForLinkRepository : EntityFrameworkRepository<FORNECEDOR_CATEGORIA, WebForLinkContexto>,
        ICategoriaWebForLinkRepository
    {
    }
}