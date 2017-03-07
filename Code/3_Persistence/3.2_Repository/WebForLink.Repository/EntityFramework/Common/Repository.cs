using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.ServiceLocation;
using WebForLink.Data.Context;
using WebForLink.Data.Context.Config;
using WebForLink.Data.Context.Interfaces;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework.Common
{
    public class EntityFrameworkRepository<TEntity, T>
        : IRepository<TEntity>, IDisposable
        where TEntity : class
        where T : BaseDbContext
    {
        private readonly IDbContext _dbContext;
        private readonly IDbSet<TEntity> _dbSet;

        public EntityFrameworkRepository()
        {
            var typeofT = typeof (T);

            if (typeofT.Name == typeof (ChMasterDataContext).Name)
            {
                var contextManager =
                    ServiceLocator.Current.GetInstance<IContextManager<ChMasterDataContext>>()
                        as ContextManager<ChMasterDataContext>;

                _dbContext = contextManager.GetContext();
                _dbSet = _dbContext.Set<TEntity>();
            }
            else if (typeofT.Name == typeof (WebForLinkContexto).Name)
            {
                var contextManager = ServiceLocator.Current.GetInstance<IContextManager<WebForLinkContexto>>()
                    as ContextManager<WebForLinkContexto>;

                _dbContext = contextManager.GetContext();
                _dbSet = _dbContext.Set<TEntity>();
            }
        }

        private string NomeEntidade
        {
            get { return typeof (TEntity).FullName; }
        }

        protected IDbContext Context
        {
            get { return _dbContext; }
        }

        protected IDbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format("ErroIncluir{0}", NomeEntidade), ex);
            }
        }

        //public virtual void Delete(int id)
        //{
        //    try
        //    {
        //        var entity = Get(id);
        //        var entry = Context.Entry(entity);
        //        DbSet.Attach(entity);
        //        entry.State = EntityState.Deleted;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RepositoryWebForLinkException(string.Format("ErroDeletar{0}", NomeEntidade), ex);
        //    }
        //}
        public virtual void Delete(TEntity entity)
        {
            try
            {
                var entry = Context.Entry(entity);
                DbSet.Attach(entity);
                entry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format("ErroDeletar{0}", NomeEntidade), ex);
            }
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> All(bool @readonly = false)
        {
            try
            {
                return @readonly
                    ? DbSet.AsNoTracking().ToList()
                    : DbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format("ErroListar", "NomeEntidade"), ex);
            }
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? DbSet.Where(predicate).AsNoTracking()
                : DbSet.Where(predicate);
        }

        public RetornoPesquisa<TEntity> Pesquisar(Expression<Func<TEntity, bool>> filtros, int tamanhoPagina, int pagina,
            Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                IQueryable<TEntity> registros = All()
                    .AsQueryable()
                    .Where(filtros);
                IQueryable<TEntity> lista = registros
                    .OrderBy(ordenacao)
                    .Skip(tamanhoPagina * (pagina - 1))
                    .Take(tamanhoPagina)
                    .AsQueryable();
                return new RetornoPesquisa<TEntity>
                {
                    TotalRegistros = registros.Count(),
                    RegistrosPagina = lista.ToList(),
                    TotalPaginas = (int)Math.Ceiling(registros.Count() / (double)tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }
        public RetornoPesquisa<TEntity> PesquisarInvertido(Expression<Func<TEntity, bool>> filtros, int tamanhoPagina, int pagina,
            Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                IQueryable<TEntity> registros = All()
                    .AsQueryable()
                    .Where(filtros);
                IQueryable<TEntity> lista = registros
                    .OrderByDescending(ordenacao)
                    .Skip(tamanhoPagina * (pagina - 1))
                    .Take(tamanhoPagina)
                    .AsQueryable();
                return new RetornoPesquisa<TEntity>
                {
                    TotalRegistros = registros.Count(),
                    RegistrosPagina = lista.ToList(),
                    TotalPaginas = (int)Math.Ceiling(registros.Count() / (double)tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public RetornoPesquisa<TEntity> Pesquisar(Expression<Func<TEntity, bool>> filtros, int tamanhoPagina, int pagina,
        //    Func<TEntity, IComparable> ordenacao, bool @readonly = false)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (Context == null) return;
            Context.Dispose();
        }

        public TEntity GetAllReferences(int id)
        {
            return DbSet.Find(id);
        }

        public void Delete(List<TEntity> entity)
        {
            if (entity.Any())
                foreach (var item in entity)
                {
                    Delete(item);
                }
        }

        public RetornoPesquisa<TEntity> Pesquisar(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Add(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

    }
}