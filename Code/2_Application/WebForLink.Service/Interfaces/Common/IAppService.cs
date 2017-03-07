using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebForLink.Application.Interfaces.Common
{
    public interface IAppService<TEntity> : IWriteOnlyAppService<TEntity>, IDisposable
        where TEntity : class
    {
        TEntity Get(int id, bool @readonly = false);
        TEntity Get(string id, bool @readonly = false);
        TEntity GetAllReferences(int id, bool @readonly = false);
        IEnumerable<TEntity> All(bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
    }
}