using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IFluxoPreRequisitoRepository : IRepository<FLUXO_SEQUENCIA_PRE_REQUIS>
    {
    }

    public class FluxoPreRequisitoRepository : EntityFrameworkRepository<FLUXO_SEQUENCIA_PRE_REQUIS, WebForLinkContexto>,
        IFluxoPreRequisitoRepository
    {
    }
}