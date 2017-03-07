using System;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Application.Interfaces
{
    public interface IMeusDocumentosWebForLinkAppService : IAppService<Compartilhamentos>
    {
        Compartilhamentos BuscarPorID(int id);

        RetornoPesquisa<Compartilhamentos> BuscarPesquisa(Expression<Func<Compartilhamentos, bool>> filtros,
            int tamanhoPagina, int pagina, Func<Compartilhamentos, IComparable> ordenacao);

        RetornoPesquisa<Compartilhamentos> BuscarPesquisaInvertido(Expression<Func<Compartilhamentos, bool>> filtros,
            int tamanhoPagina, int pagina, Func<Compartilhamentos, IComparable> ordenacao);
    }
}
