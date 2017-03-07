using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class SolicitacaoBancoWebForLinkRepository :
        EntityFrameworkRepository<SolicitacaoModificacaoDadosBancario, WebForLinkContexto>,
        ISolicitacaoBancoWebForLinkRepository
    {
    }

    public class QuestionarioRespostaWebForLinkRepository :
        EntityFrameworkRepository<QUESTIONARIO_RESPOSTA, WebForLinkContexto>,
        IQuestionarioRespostaWebForLinkRepository
    {
    }
}