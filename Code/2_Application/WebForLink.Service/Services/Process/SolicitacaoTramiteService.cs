using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoTramiteWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoTramiteWebForLinkAppService
    {
        private readonly ISolicitacaoTramiteWebForLinkService _solicitacaoTramiteService;

        public SolicitacaoTramiteWebForLinkAppService(ISolicitacaoTramiteWebForLinkService solicitacaoTramiteService)
        {
            try
            {
                _solicitacaoTramiteService = solicitacaoTramiteService;
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
                return _solicitacaoTramiteService.BuscarTramitePorSolicitacaoIdPapelId(solicitacaoId, papelId);
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
                return _solicitacaoTramiteService.SolicitacaoAprovadaPorUmAprovador(solicitacao);
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
                return _solicitacaoTramiteService.SolicitacaoFornecedorFinalizou(solicitacao);
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

        public SOLICITACAO_TRAMITE Get(int id, bool @readonly = false)
        {
            return _solicitacaoTramiteService.Get(id, @readonly);
        }

        public SOLICITACAO_TRAMITE Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_TRAMITE GetAllReferences(int id, bool @readonly = false)
        {
            return _solicitacaoTramiteService.GetAllReferences(id, @readonly);
        }

        public IEnumerable<SOLICITACAO_TRAMITE> All(bool @readonly = false)
        {
            return _solicitacaoTramiteService.All(@readonly);
        }

        public IEnumerable<SOLICITACAO_TRAMITE> Find(Expression<Func<SOLICITACAO_TRAMITE, bool>> predicate,
            bool @readonly = false)
        {
            return _solicitacaoTramiteService.Find(predicate, @readonly);
        }

        public ValidationResult Create(SOLICITACAO_TRAMITE entity)
        {
            BeginTransaction();
            var validacao = _solicitacaoTramiteService.Add(entity);
            Commit();
            return validacao;
        }

        public ValidationResult Update(SOLICITACAO_TRAMITE entity)
        {
            return _solicitacaoTramiteService.Update(entity);
        }

        public ValidationResult Remove(SOLICITACAO_TRAMITE entity)
        {
            return _solicitacaoTramiteService.Delete(entity);
        }
    }
}