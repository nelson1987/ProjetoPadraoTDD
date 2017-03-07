using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoTramiteWebForLinkService : IService<SOLICITACAO_TRAMITE>
    {
        SOLICITACAO_TRAMITE BuscarTramitePorSolicitacaoIdPapelId(int solicitacaoId, int papelId);
        bool SolicitacaoAprovadaPorUmAprovador(int solicitacao);
        bool SolicitacaoFornecedorFinalizou(int solicitacao);
        bool AlterarTramite();
    }

    public class SolicitacaoTramiteWebForLinkService : Service<SOLICITACAO_TRAMITE>,
        ISolicitacaoTramiteWebForLinkService
    {
        private readonly ISolicitacaoTramiteWebForLinkRepository _solicitacaoTramiteRepository;

        public SolicitacaoTramiteWebForLinkService(ISolicitacaoTramiteWebForLinkRepository solicitacaoTramiteRepository)
            : base(solicitacaoTramiteRepository)
        {
            try
            {
                _solicitacaoTramiteRepository = solicitacaoTramiteRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SOLICITACAO_TRAMITE BuscarTramitePorSolicitacaoIdPapelId(int solicitacaoId, int papelId)
        {
            try
            {
                return _solicitacaoTramiteRepository.BuscarTramitePorSolicitacaoIdPapelId(solicitacaoId, papelId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        public bool SolicitacaoAprovadaPorUmAprovador(int solicitacao)
        {
            try
            {
                return _solicitacaoTramiteRepository.SolicitacaoAprovadaPorUmAprovador(solicitacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de Solicitacao Tramite", ex);
            }
        }

        public bool SolicitacaoFornecedorFinalizou(int solicitacao)
        {
            try
            {
                return _solicitacaoTramiteRepository.SolicitacaoFornecedorFinalizou(solicitacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de Solicitacao Tramite", ex);
            }
        }

        public bool AlterarTramite()
        {
            return false;
        }

        public void Dispose()
        {
        }
    }
}