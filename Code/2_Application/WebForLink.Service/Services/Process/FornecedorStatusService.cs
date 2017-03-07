using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class FornecedorStatusWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorStatusWebForLinkAppService
    {
        private readonly IFornecedorStatusWebForLinkService _statusFornecedorService;

        public FornecedorStatusWebForLinkAppService(IFornecedorStatusWebForLinkService statusFornecedorService)
        {
            try
            {
                _statusFornecedorService = statusFornecedorService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDOR_STATUS Get(Expression<Func<FORNECEDOR_STATUS, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_STATUS> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_STATUS> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(FORNECEDOR_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IEnumerable<FORNECEDOR_STATUS> Find(Expression<Func<FORNECEDOR_STATUS, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_STATUS> Find(Expression<Func<FORNECEDOR_STATUS, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_STATUS Get(int id)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_STATUS Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_STATUS Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_STATUS GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(FORNECEDOR_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDOR_STATUS entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FORNECEDOR_STATUS BuscarPorID(int id)
        {
            try
            {
                return _statusFornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um status de fornecedor por ID", ex);
            }
        }
    }
}