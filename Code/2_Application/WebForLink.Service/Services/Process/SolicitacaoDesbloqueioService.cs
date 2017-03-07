using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoDesbloqueioWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoDesbloqueioWebForLinkService, ISolicitacaoDesbloqueioWebForLinkAppService
    {
        private readonly IFluxoWebForLinkService _fluxoBP;
        private readonly IPapelWebForLinkService _papelBP;
        private readonly ISolicitacaoDesbloqueioWebForLinkService _solicitacaoDesbloqueioService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public SolicitacaoDesbloqueioWebForLinkAppService(
            ISolicitacaoWebForLinkService solicitacaoService,
            ISolicitacaoDesbloqueioWebForLinkService solicitacaoDesbloqueioService,
            IFluxoWebForLinkService fluxoBP,
            IPapelWebForLinkService papelBP)
        {
            try
            {
                _solicitacaoService = solicitacaoService;
                _solicitacaoDesbloqueioService = solicitacaoDesbloqueioService;
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
                _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.DesbloqueioFornecedor, contratanteId).ID;
            var solicitacao = new SOLICITACAO
            {
                FLUXO_ID = fluxoId, // Bloqueio
                SOLICITACAO_DT_CRIA = DateTime.Now,
                SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao, // EM APROVACAO
                USUARIO_ID = usuarioId,
                PJPF_ID = fornecedorId,
                CONTRATANTE_ID = contratanteId != 0 ? contratanteId : 0
            };
            _solicitacaoService.Add(solicitacao);

            var desbloqueio = new SOLICITACAO_DESBLOQUEIO
            {
                BLQ_LANCAMENTO_TODAS_EMP = rdLancamento == "1",
                BLQ_LANCAMENTO_EMP = rdLancamento == "2",
                BLQ_COMPRAS_TODAS_ORG_COMPRAS = !string.IsNullOrEmpty(rdCompras),
                BLQ_QUALIDADE_FUNCAO_BQL_ID = bloqueioMotivoQualidade,
                BLQ_MOTIVO_DSC = txtAreaMotivoDesbloqueio,
                WFD_SOLICITACAO = solicitacao
            };
            _solicitacaoDesbloqueioService.Add(desbloqueio);

            var papelAtual = _papelBP.BuscarPorContratanteETipoPapel(contratanteId, (int)EnumTiposPapel.Solicitante).ID;
            return desbloqueio;
        }

        public SOLICITACAO_DESBLOQUEIO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_DESBLOQUEIO Get(Expression<Func<SOLICITACAO_DESBLOQUEIO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_DESBLOQUEIO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_DESBLOQUEIO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_DESBLOQUEIO> Find(Expression<Func<SOLICITACAO_DESBLOQUEIO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(SOLICITACAO_DESBLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public List<ValidationResult> Add(List<SOLICITACAO_DESBLOQUEIO> entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SOLICITACAO_DESBLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(SOLICITACAO_DESBLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public List<ValidationResult> Delete(List<SOLICITACAO_DESBLOQUEIO> entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_DESBLOQUEIO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO_DESBLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO_DESBLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_DESBLOQUEIO Get(int id)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_DESBLOQUEIO Get(Expression<Func<SOLICITACAO_DESBLOQUEIO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_DESBLOQUEIO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_DESBLOQUEIO> Find(Expression<Func<SOLICITACAO_DESBLOQUEIO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<SOLICITACAO_DESBLOQUEIO> BuscarPesquisa(Expression<Func<SOLICITACAO_DESBLOQUEIO, bool>> filtros, int tamanhoPagina, int pagina, Func<SOLICITACAO_DESBLOQUEIO, IComparable> ordenacao)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Modificar(SOLICITACAO_DESBLOQUEIO entity)
        {
            throw new NotImplementedException();
        }
    }
}