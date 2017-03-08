using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebForLink.Domain.Interfaces.Repository.Common
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        void Add(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(List<TEntity> entity);
        TEntity Get(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity GetAllReferences(int id);
        IEnumerable<TEntity> All(bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
    }
}