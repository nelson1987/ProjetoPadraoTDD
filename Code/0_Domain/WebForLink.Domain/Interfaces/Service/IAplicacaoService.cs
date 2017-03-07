using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Interfaces.Service
{
    public interface IAplicacaoWebForLinkService : IService<APLICACAO>
    {
        APLICACAO BuscarPorID(int id);
        APLICACAO BuscarPorIdNomeAplicacao(int id, string nomeAplicacao);
        APLICACAO BuscarPorNome(string nomeAplicacao);
        APLICACAO AlterarAplicacao(APLICACAO entidade);
        void ExcluirAplicacao(int id);
        APLICACAO InserirAplicacao(APLICACAO entidade);
        List<APLICACAO> ListarTodos();
        RetornoPesquisa<APLICACAO> PesquisarAplicacao(PesquisaAplicacaoFiltrosDTO filtros, int pagina, int tamanhoPagina);
    }
}