using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoMaterialEServicoWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoMaterialEServicoWebForLinkAppService
    {
        private readonly IConfiguracaoWebForLinkService _configuracao;
        private readonly ISolicitacaoServicoMaterialWebForLinkService _unspscSolicitacao;

        public SolicitacaoMaterialEServicoWebForLinkAppService(ISolicitacaoServicoMaterialWebForLinkService solUnspsc,
            IConfiguracaoWebForLinkService configuracao)
        {
            try
            {
                _configuracao = configuracao;
                _unspscSolicitacao = solUnspsc;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO_UNSPSC BuscarPorId(int id)
        {
            try
            {
                return _unspscSolicitacao.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(
                    "Erro ao buscar uma solicitacao de cadastro de Materias de serviço por ID", ex);
            }
        }

        public void ManterUnspscSolicitacao(List<SOLICITACAO_UNSPSC> unscpsc, int idSolicitacao)
        {
            try
            {
                BeginTransaction();
                foreach (var item in _unspscSolicitacao.BuscarPorSolicitacaoId(idSolicitacao))
                    _unspscSolicitacao.Delete(item);
                foreach (var item in unscpsc)
                    _unspscSolicitacao.Add(item);
                Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar atualizar o UNSPSC.", ex);
            }
        }

        public void InserirSolicitacoes(List<SOLICITACAO_UNSPSC> unscpsc)
        {
            try
            {
                BeginTransaction();
                foreach (var item in unscpsc)
                    _unspscSolicitacao.Add(item);
                Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar inserir a solicitacao de modificação de Unspsc.",
                    ex);
            }
        }

        public SOLICITACAO_UNSPSC Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_UNSPSC Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_UNSPSC GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_UNSPSC> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_UNSPSC> Find(Expression<Func<SOLICITACAO_UNSPSC, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO_UNSPSC entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SOLICITACAO_UNSPSC entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO_UNSPSC entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_UNSPSC Get(int id)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_UNSPSC Get(Expression<Func<SOLICITACAO_UNSPSC, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_UNSPSC> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_UNSPSC> Find(Expression<Func<SOLICITACAO_UNSPSC, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}