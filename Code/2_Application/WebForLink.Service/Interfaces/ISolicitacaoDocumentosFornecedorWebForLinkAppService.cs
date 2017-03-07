using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface ISolicitacaoDocumentosFornecedorWebForLinkAppService
    {
        SolicitacaoDeDocumentos BuscarPorId(int id);
        List<SolicitacaoDeDocumentos> ListarPorSolicitacaoId(int solicitacaoId);
        void InserirSolicitacoes(List<SolicitacaoDeDocumentos> solicitacoes);
        void AtualizarSolicitacao(SolicitacaoDeDocumentos solicitacaoDeDocumentos);
        int AdicionaDocumentosSolicitacao(int SolicitacaoId, int DescricaoDocumentoId);
        bool DocumentoDuplicado(int SolicitacaoId, int DescricaoDocumentoId);
        int RemoverDocumentosSolicitacao(int SolicitacaoId, int DescricaoDocumentoId);
    }
}
