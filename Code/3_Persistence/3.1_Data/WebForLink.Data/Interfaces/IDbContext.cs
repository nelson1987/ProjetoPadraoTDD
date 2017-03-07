using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebForLink.Data.Context.Interfaces
{
    public interface IDbContext
    {
        int? CurrentUserId { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}