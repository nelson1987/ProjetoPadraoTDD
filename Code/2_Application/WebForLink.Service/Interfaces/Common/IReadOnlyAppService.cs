using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebForLink.Application.Interfaces.Common
{
    public interface IReadOnlyAppService<TEntity>
        where TEntity : class
    {
        TEntity Get(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}