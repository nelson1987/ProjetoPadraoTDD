using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface IInformacaoComplementarWebForLinkAppService
    {
        WFD_INFORM_COMPL BuscarPorId(int id);
        WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta);
        WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoId(int idPergunta, int idSolicitacao);
        WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoIdResposta(int idPergunta, int idSolicitacao, string resposta);
        List<WFD_INFORM_COMPL> UpdateAll(List<WFD_INFORM_COMPL> entidade);
        List<WFD_INFORM_COMPL> InserirTodos(List<WFD_INFORM_COMPL> entidade);
        FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdPJPFId(int idPergunta, int idPJPF);
        WFD_INFORM_COMPL ValidaExistente(WFD_INFORM_COMPL entidade);
        bool ValidaDuplicado(WFD_INFORM_COMPL entidade);
        List<WFD_INFORM_COMPL> InsertAll(List<WFD_INFORM_COMPL> entidade);
    }
}
