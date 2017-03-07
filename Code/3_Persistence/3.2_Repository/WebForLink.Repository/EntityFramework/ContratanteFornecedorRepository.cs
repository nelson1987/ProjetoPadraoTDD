using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class ContratanteFornecedorWebForLinkRepository :
        EntityFrameworkRepository<WFD_CONTRATANTE_PJPF, WebForLinkContexto>,
        IContratanteFornecedorWebForLinkRepository
    {
        public IEnumerable<WFD_CONTRATANTE_PJPF> BuscarCustomizado(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> predicate)
        {
            try
            {
                return DbSet
                    .Include("WFD_PJPF_STATUS")
                    .Include("Contratante")
                    .Include("Fornecedor")
                    .AsQueryable()
                    .Where(predicate);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format("ErroListar, NomeEntidade"), ex);
            }
        }

        public RetornoPesquisa<WFD_CONTRATANTE_PJPF> BuscarPesquisaCostumizada(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtros, int tamanhoPagina, int pagina
            , Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao)
        {
            try
            {
                var registros = BuscarCustomizado(filtros);
                var lista = registros.AsQueryable()
                    .Where(filtros)
                    .OrderBy(d => new {d.PJPF_ID, d.CONTRATANTE_ID})
                    .Skip(tamanhoPagina*(pagina - 1))
                    .Take(tamanhoPagina)
                    .ToList();
                return new RetornoPesquisa<WFD_CONTRATANTE_PJPF>
                {
                    TotalRegistros = registros.Count(),
                    RegistrosPagina = lista,
                    TotalPaginas = (int) Math.Ceiling(registros.Count()/(double) tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }
    }
}