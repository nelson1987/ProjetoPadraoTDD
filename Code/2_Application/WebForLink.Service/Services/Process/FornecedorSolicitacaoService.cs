using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class FornecedorSolicitacaWebForLinkAppService : AppService<WebForLinkContexto>,
        IAppService<FORNECEDOR_SOLICITACAO>
    {
        private readonly IFornecedorSolicitacaoWebForLinkService _solicitacaoOrigemFornecedorService;

        public FornecedorSolicitacaWebForLinkAppService(
            IFornecedorSolicitacaoWebForLinkService solicitacaoOrigemFornecedorService)
        {
            _solicitacaoOrigemFornecedorService = solicitacaoOrigemFornecedorService;
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public ValidationResult Create(FORNECEDOR_SOLICITACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDOR_SOLICITACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(FORNECEDOR_SOLICITACAO entity)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_SOLICITACAO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_SOLICITACAO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_SOLICITACAO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_SOLICITACAO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_SOLICITACAO> Find(Expression<Func<FORNECEDOR_SOLICITACAO, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FORNECEDOR_SOLICITACAO BuscarPorID(int id)
        {
            try
            {
                return _solicitacaoOrigemFornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma solicitação de cadastro de fornecedor por ID",
                    ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FORNECEDOR_SOLICITACAO BuscarPorIdDocumentoSolicitados(int id)
        {
            try
            {
                return _solicitacaoOrigemFornecedorService.Get(c => c.ID == id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(
                    "Erro ao buscar uma solicitacao de cadastro de Fornecedor por Id de Documentos Solicitados", ex);
            }
        }
    }
}