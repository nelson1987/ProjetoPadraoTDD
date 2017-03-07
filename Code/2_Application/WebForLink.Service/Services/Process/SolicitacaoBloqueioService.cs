using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoBloqueioWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoBloqueioWebForLinkAppService
    {
        
        private readonly ISolicitacaoBloqueioWebForLinkService _solicitacaoBloqueioService;

        public SolicitacaoBloqueioWebForLinkAppService(ISolicitacaoBloqueioWebForLinkService solicitacaoBloqueioService)
        {
            try
            {
                _solicitacaoBloqueioService = solicitacaoBloqueioService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public SOLICITACAO_BLOQUEIO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_BLOQUEIO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_BLOQUEIO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_BLOQUEIO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_BLOQUEIO> Find(Expression<Func<SOLICITACAO_BLOQUEIO, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO_BLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SOLICITACAO_BLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO_BLOQUEIO entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_BLOQUEIO Get(int id)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_BLOQUEIO Get(Expression<Func<SOLICITACAO_BLOQUEIO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_BLOQUEIO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_BLOQUEIO> Find(Expression<Func<SOLICITACAO_BLOQUEIO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO_BLOQUEIO BuscarPorID(int id)
        {
            try
            {
                return _solicitacaoBloqueioService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}