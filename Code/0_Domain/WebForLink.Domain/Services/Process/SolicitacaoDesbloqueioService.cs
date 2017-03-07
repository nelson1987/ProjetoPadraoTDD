using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class SolicitacaoDesbloqueioService : Service<SOLICITACAO_DESBLOQUEIO>
    {
        private readonly IFluxoWebForLinkService _fluxoBP;
        private readonly IPapelWebForLinkService _papelBP;
        private readonly ISolicitacaoDesbloqueioWebForLinkRepository _solicitacaoDesbloqueioRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public SolicitacaoDesbloqueioService(
            ISolicitacaoWebForLinkRepository solicitacaoRepository,
            ISolicitacaoDesbloqueioWebForLinkRepository solicitacaoDesbloqueioRepository,
            IFluxoWebForLinkService fluxoBP,
            IPapelWebForLinkService papelBP) : base(solicitacaoDesbloqueioRepository)
        {
            try
            {
                _solicitacaoRepository = solicitacaoRepository;
                _solicitacaoDesbloqueioRepository = solicitacaoDesbloqueioRepository;
                _fluxoBP = fluxoBP;
                _papelBP = papelBP;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public SOLICITACAO_DESBLOQUEIO criarSolicitacaoDesbloqueio(int contratanteId, int usuarioId, int fornecedorId,
            string rdLancamento,
            string rdCompras, int? bloqueioMotivoQualidade, string txtAreaMotivoDesbloqueio)
        {
            var fluxoId =
                _fluxoBP.BuscarPorTipoEContratante((int) EnumTiposFluxo.DesbloqueioFornecedor, contratanteId).ID;
            var solicitacao = new SOLICITACAO
            {
                FLUXO_ID = fluxoId, // Bloqueio
                SOLICITACAO_DT_CRIA = DateTime.Now,
                SOLICITACAO_STATUS_ID = (int) EnumStatusTramite.EmAprovacao, // EM APROVACAO
                USUARIO_ID = usuarioId,
                PJPF_ID = fornecedorId,
                CONTRATANTE_ID = contratanteId != 0 ? contratanteId : 0
            };
            _solicitacaoRepository.Add(solicitacao);

            var desbloqueio = new SOLICITACAO_DESBLOQUEIO
            {
                BLQ_LANCAMENTO_TODAS_EMP = rdLancamento == "1",
                BLQ_LANCAMENTO_EMP = rdLancamento == "2",
                BLQ_COMPRAS_TODAS_ORG_COMPRAS = !string.IsNullOrEmpty(rdCompras),
                BLQ_QUALIDADE_FUNCAO_BQL_ID = bloqueioMotivoQualidade,
                BLQ_MOTIVO_DSC = txtAreaMotivoDesbloqueio,
                WFD_SOLICITACAO = solicitacao
            };
            _solicitacaoDesbloqueioRepository.Add(desbloqueio);

            var papelAtual = _papelBP.BuscarPorContratanteETipoPapel(contratanteId, (int) EnumTiposPapel.Solicitante).ID;
            return desbloqueio;
        }
    }
}