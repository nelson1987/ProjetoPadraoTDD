using System;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class FornecedorBaseWebForLinkRepository : EntityFrameworkRepository<FORNECEDORBASE, WebForLinkContexto>,
        IFornecedorBaseWebForLinkRepository
    {
        public void AlterarFornecedorbase(FORNECEDORBASE atual)
        {
            var fornecedorBanco = BuscarFornecedorbase(x => x.ID == atual.ID);
            //var mescla = MesclarObjetos(fornecedorBanco, atual);
        }

        public FORNECEDORBASE BuscarFornecedorbase(Expression<Func<FORNECEDORBASE, bool>> filtro,
            bool lazyloading = false)
        {
            try
            {
                //Context.Configuration.LazyLoadingEnabled = lazyloading;
                return Context.Set<FORNECEDORBASE>().AsQueryable().FirstOrDefault(filtro);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format("ErroBuscar", "FornecedorBaseRepository"), ex);
            }
        }
    }
}