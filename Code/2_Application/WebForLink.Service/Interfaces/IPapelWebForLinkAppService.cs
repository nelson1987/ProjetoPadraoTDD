using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;

namespace WebForLink.Application.Interfaces
{
    public interface IPapelWebForLinkAppService
    {
        Papel BuscarPorID(int id);
        Papel InserirPapel(Papel entidade);
        List<Papel> ListarTodos(int contratanteId);
        List<Papel> ListarTodos(int[] papeis);
        Papel BuscarPorContratanteETipoPapel(int contratanteId, int tipo);
        int[] EmpilharPorUsuarioId(int usuarioId);
        Papel AlterarPapel(Papel entidade);
        Papel BuscarPorSigla(string sigla);
        RetornoPesquisa<Papel> PesquisarPapel(PesquisaPapelFiltrosDTO filtros, int pagina, int tamanhoPagina);
        void ExcluirPapel(int id);
    }
}
