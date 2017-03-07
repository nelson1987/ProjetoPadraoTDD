using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Application.Interfaces
{
    public interface IFuncaoWebForLinkAppService : IService<FUNCAO>
    {
        FUNCAO BuscarPorID(int id);
        FUNCAO BuscarPorCodigo(string codigo);

        RetornoPesquisa<FUNCAO> PesquisarFuncao(PesquisaFuncaoFiltrosDTO filtros, int pagina, int tamanhoPagina,
            int contratanteId);

        List<FUNCAO> ListarTodos(int ContratanteId);
        List<FUNCAO> ListarTodosPorPerfil(int idPerfil);
        FUNCAO InserirFuncao(FUNCAO entidade);
        FUNCAO AlterarFuncao(FUNCAO entidade);
    }
}
