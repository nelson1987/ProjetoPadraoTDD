using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebForLink.Data.Context;
using WebForLink.Domain.Infrastructure;
using WebForLink.Repository.Interfaces;

namespace WebForLink.Repository.Infrastructure
{
    public abstract class PadraoRepository<TEntity> : IPadraoRepository<TEntity> where TEntity : class
    {
        public WebForLinkContexto Contexto
        {
            get { return (WebForLinkContexto)Context; }
        }
        #region Constantes
        public const string ErroBuscar = "Erro ao buscar {0}";
        public const string ErroListar = "Erro ao listar {0}";
        public const string ErroIncluir = "Erro ao incluir {0}";
        public const string ErroDeletar = "Erro ao deletar {0}";
        public const string ErroAlterar = "Erro ao alterar {0}";
        #endregion

        public string NomeEntidade { get; set; }

        protected readonly DbContext Context;

        public PadraoRepository(IWebForLinkContexto context)
        {
            Context = (DbContext)context;
            NomeEntidade = typeof(TEntity).FullName;
        }

        /// <summary>
        /// Buscar entidade por Id
        /// </summary>
        /// <param name="id">Id da entidade</param>
        /// <returns>Entidade</returns>
        public TEntity BuscarPorId(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroBuscar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Buscar Entidade por Código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public TEntity BuscarPorCodigo(string codigo)
        {
            try
            {
                return Context.Set<TEntity>().Find(codigo);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroBuscar, NomeEntidade), ex);
            }
        }

        #region Listar
        /// <summary>
        /// Listar todos
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Listar()
        {
            try
            {
                return Context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroListar, NomeEntidade), ex);
            }
        }

        public IQueryable<TEntity> Listar(Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                return Context.Set<TEntity>().OrderBy(ordenacao).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroListar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Buscar todos com filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Listar(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                return Context.Set<TEntity>().AsExpandable().Where(filtro);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroListar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Buscar todos com filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Listar(Expression<Func<TEntity, bool>> filtro, Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                return Context.Set<TEntity>().AsExpandable().Where(filtro).OrderBy(ordenacao).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroListar, NomeEntidade), ex);
            }
        }
        #endregion

        #region Buscar
        /// <summary>
        /// Buscar Primeiro absoluto
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Buscar()
        {
            try
            {
                return Context.Set<TEntity>().AsExpandable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroBuscar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Buscar Primeiro com filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public TEntity Buscar(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                return Context.Set<TEntity>().AsExpandable().FirstOrDefault(filtro);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroBuscar, NomeEntidade), ex);
            }
        }

        public TEntity Buscar(Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                return Context.Set<TEntity>().OrderBy(ordenacao).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroBuscar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Buscar Primeiro com filtro e ordenação
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public TEntity Buscar(Expression<Func<TEntity, bool>> filtro, Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                return Context.Set<TEntity>().AsExpandable().Where(filtro).OrderBy(ordenacao).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroBuscar, NomeEntidade), ex);
            }
        }
        #endregion

        #region Inserir
        /// <summary>
        /// Inserir entidade
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Inserir(TEntity entity)
        {
            try
            {
                return Context.Set<TEntity>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroIncluir, NomeEntidade), ex);
            }
        }

        public void Inserir(List<TEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                    Inserir(item);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroDeletar, NomeEntidade), ex);
            }
        }
        #endregion

        #region Alterar
        /// <summary>
        /// Alterar entidade
        /// </summary>
        /// <param name="entity"></param>
        public void Alterar(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroAlterar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Alterar entidade
        /// </summary>
        /// <param name="entity"></param>
        public void Alterar(List<TEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                    Alterar(item);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroAlterar, NomeEntidade), ex);
            }
        }
        #endregion

        #region Deletar
        public void Deletar(int id)
        {
            try
            {
                Deletar(BuscarPorId(id));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroDeletar, NomeEntidade), ex);
            }
        }

        /// <summary>
        /// Excluir entidade
        /// </summary>
        /// <param name="entity"></param>
        public void Deletar(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                Context.Set<TEntity>().Attach(entity);
                Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroDeletar, NomeEntidade), ex);
            }
        }

        public void Deletar(List<TEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                    Deletar(item);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroDeletar, NomeEntidade), ex);
            }
        }

        public void Deletar(IEnumerable<TEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    Deletar(item);
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroDeletar, NomeEntidade), ex);
            }
        }
        #endregion

        /// <summary>
        /// Buscar todos com filtro e ordenação para Grid
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="pagina"></param>
        /// <param name="ordenacao"></param>
        /// <returns></returns>
        public RetornoPesquisa<TEntity> Pesquisar(Expression<Func<TEntity, bool>> filtros, int tamanhoPagina, int pagina
            , Func<TEntity, IComparable> ordenacao)
        {
            try
            {
                var registros = Listar(filtros);
                var lista = registros.AsQueryable()
                    .Where(filtros)
                    .OrderBy(ordenacao)
                    .Skip(tamanhoPagina * (pagina - 1))
                    .Take(tamanhoPagina)
                    .ToList();
                return new RetornoPesquisa<TEntity>()
                {
                    TotalRegistros = registros.Count(),
                    RegistrosPagina = lista,
                    TotalPaginas = (int)Math.Ceiling(registros.Count() / (double)tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        public void ExecutarQuery(string query)
        {
            try
            {
                Context.Database.ExecuteSqlCommand(query);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format(ErroListar, NomeEntidade), ex);
            }
        }
        
        public TEntity MesclarObjetos(TEntity entidadeBanco, TEntity entidadeSistema)
        {
            //if (entidadeBanco.GetType() != entidadeSistema.GetType())
            //{
            //    throw new Exception("Os parâmetros deve ser do mesmo tipo.");
            //}
            foreach (PropertyInfo p in entidadeBanco.GetType().GetProperties())
            {
                object v1 = entidadeBanco.GetType().GetProperty(p.Name).GetValue(entidadeBanco, null);
                object v2 = entidadeSistema.GetType().GetProperty(p.Name).GetValue(entidadeSistema, null);
                object v3 =  (v2 != null && v2.ToString() != "" && v2.ToString() != "0" && v2!=v1 ? v2 : v1);
                entidadeBanco.GetType().GetProperty(p.Name).SetValue(entidadeBanco, v3, null);
            }
            return entidadeBanco;
        }
    }
}