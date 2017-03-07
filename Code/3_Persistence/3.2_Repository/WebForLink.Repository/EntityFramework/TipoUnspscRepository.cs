using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class TipoUnspscWebForLinkRepository : EntityFrameworkRepository<TIPO_UNSPSC, WebForLinkContexto>,
        ITipoUnspscWebForLinkRepository
    {
    }

    public class TipoFuncaoBloqueioWebForLinkRepository :
        EntityFrameworkRepository<TIPO_FUNCAO_BLOQUEIO, WebForLinkContexto>, ITipoFuncaoBloqueioWebForLinkRepository
    {
    }
}