using WebForLink.Data.Context.Interfaces;

namespace WebForLink.Application.Interfaces.Common
{
    public interface ITransactionAppService<TContext>
        where TContext : IDbContext, new()
    {
        void BeginTransaction();
        void Commit();
    }
}