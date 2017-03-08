using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Interfaces.Service.Common
{
    public interface IService<TEntity>
        where TEntity : class
    {
        TEntity Get(int id, bool @readonly = false);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        TEntity GetAllReferences(int id, bool @readonly = false);
        IEnumerable<TEntity> All(bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        ValidationResult Add(TEntity entity);
        List<ValidationResult> Add(List<TEntity> entity);
        ValidationResult Update(TEntity entity);
        ValidationResult Delete(TEntity entity);
        List<ValidationResult> Delete(List<TEntity> entity);

        //RetornoPesquisa<TEntity> BuscarPesquisa(Expression<Func<TEntity, bool>> filtros, int tamanhoPagina, int pagina,
        //    Func<TEntity, IComparable> ordenacao);
    }
}