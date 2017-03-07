using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface ISolicitacaoProrrogacaoPrazoWebForLinkAppService
    {
        void AprovarSolicitacaoProrrogacao(int id, int usuarioId);
        void ReprovarSolicitacaoProrrogacao(int id, string motivo, int usuarioId);
        SOLICITACAO_PRORROGACAO BuscarPorId(int id);
    }
}
