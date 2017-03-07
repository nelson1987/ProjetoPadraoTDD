using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoProrrogacaoPrazoWebForLinkService : IService<SOLICITACAO_PRORROGACAO>
    {
        void AprovarSolicitacaoProrrogacao(int id, int usuarioId);
        void ReprovarSolicitacaoProrrogacao(int id, string motivo, int usuarioId);
        SOLICITACAO_PRORROGACAO BuscarPorId(int id);
    }

    public class SolicitacaoProrrogacaoPrazoWebForLinkService : Service<SOLICITACAO_PRORROGACAO>,
        ISolicitacaoProrrogacaoPrazoWebForLinkService
    {
        private readonly ISolicitacao_prorrogacaoWebForLinkRepository _solicitacaoProrrogacaoPrazoRepository;

        public SolicitacaoProrrogacaoPrazoWebForLinkService(
            ISolicitacao_prorrogacaoWebForLinkRepository solicitacaoProrrogacaoPrazoRepository)
            : base(solicitacaoProrrogacaoPrazoRepository)
        {
            try
            {
                _solicitacaoProrrogacaoPrazoRepository = solicitacaoProrrogacaoPrazoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SOLICITACAO_PRORROGACAO BuscarPorId(int id)
        {
            try
            {
                return
                    _solicitacaoProrrogacaoPrazoRepository.Find(x => x.SOLICITACAO_ID == id && x.APROVADO == null)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao importar com convite", ex);
            }
        }

        public void AprovarSolicitacaoProrrogacao(int id, int usuarioId)
        {
            try
            {
                var prorrogacao = _solicitacaoProrrogacaoPrazoRepository.BuscarPorIdIncluindoSolicitacao(id);

                var solicitacao = prorrogacao.WFD_SOLICITACAO;
                solicitacao.DT_PRORROGACAO_PRAZO = prorrogacao.DT_PRORROGACAO_PRAZO;

                prorrogacao.APROVADO = true;
                prorrogacao.DT_AVALIACAO = DateTime.Now;
                prorrogacao.USUARIO_AVALIACAO_ID = usuarioId;
                prorrogacao.WFD_SOLICITACAO = solicitacao;

                _solicitacaoProrrogacaoPrazoRepository.Update(prorrogacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao salvar a reprovação da prorrogação", ex);
            }
        }

        public void ReprovarSolicitacaoProrrogacao(int id, string motivo, int usuarioId)
        {
            try
            {
                var prorrogacao = _solicitacaoProrrogacaoPrazoRepository.Get(id);

                prorrogacao.APROVADO = false;
                prorrogacao.DT_AVALIACAO = DateTime.Now;
                prorrogacao.MOTIVO_REPROVACAO = motivo;
                prorrogacao.USUARIO_AVALIACAO_ID = usuarioId;
                _solicitacaoProrrogacaoPrazoRepository.Update(prorrogacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao salvar a reprovação da prorrogação", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}