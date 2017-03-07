using System.Data.Entity;
using WebForLink.Data.Context.Interfaces;

namespace WebForLink.Data.Context.Config
{
    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(string connectionStringName, int? currentUserId = null)
            : base(connectionStringName)
        {
            CurrentUserId = currentUserId;
        }

        public int? CurrentUserId { get; private set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}