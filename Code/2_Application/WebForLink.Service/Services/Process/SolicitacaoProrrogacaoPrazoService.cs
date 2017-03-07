using System;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoProrrogacaoPrazoWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoProrrogacaoPrazoWebForLinkAppService
    {
        private readonly ISolicitacaoProrrogacaoWebForLinkService _solicitacaoProrrogacaoPrazoService;

        public SolicitacaoProrrogacaoPrazoWebForLinkAppService(
            ISolicitacaoProrrogacaoWebForLinkService solicitacaoProrrogacaoPrazoService)
        {
            try
            {
                _solicitacaoProrrogacaoPrazoService = solicitacaoProrrogacaoPrazoService;
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
                return _solicitacaoProrrogacaoPrazoService.Get(x => x.SOLICITACAO_ID == id && x.APROVADO == null);
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
                var prorrogacao = _solicitacaoProrrogacaoPrazoService.BuscarPorIdIncluindoSolicitacao(id);

                var solicitacao = prorrogacao.WFD_SOLICITACAO;
                solicitacao.DT_PRORROGACAO_PRAZO = prorrogacao.DT_PRORROGACAO_PRAZO;

                prorrogacao.APROVADO = true;
                prorrogacao.DT_AVALIACAO = DateTime.Now;
                prorrogacao.USUARIO_AVALIACAO_ID = usuarioId;
                prorrogacao.WFD_SOLICITACAO = solicitacao;

                _solicitacaoProrrogacaoPrazoService.Update(prorrogacao);
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
                var prorrogacao = _solicitacaoProrrogacaoPrazoService.Get(id);

                prorrogacao.APROVADO = false;
                prorrogacao.DT_AVALIACAO = DateTime.Now;
                prorrogacao.MOTIVO_REPROVACAO = motivo;
                prorrogacao.USUARIO_AVALIACAO_ID = usuarioId;
                _solicitacaoProrrogacaoPrazoService.Update(prorrogacao);
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