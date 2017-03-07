using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class TipoFuncaoBloqueioRepository : EntityFrameworkRepository<TIPO_FUNCAO_BLOQUEIO, WebForLinkContexto>,
        ITipoFuncaoBloqueioWebForLinkRepository
    {
    }

    public class FornecedorUnspscWebForLinkRepository : EntityFrameworkRepository<FORNECEDOR_UNSPSC, WebForLinkContexto>,
        IFornecedorUnspscWebForLinkRepository
    {
    }
}