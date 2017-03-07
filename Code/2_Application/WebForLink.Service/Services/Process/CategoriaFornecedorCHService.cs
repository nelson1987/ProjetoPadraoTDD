using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class CategoriaFornecedorChWebForLinkAppService : AppService<WebForLinkContexto>,
        ICategoriaFornecedorChWebForLinkAppService
    {
        public CategoriaFornecedorChWebForLinkAppService()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDOR_CATEGORIA_CH Get(Expression<Func<FORNECEDOR_CATEGORIA_CH, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_CATEGORIA_CH> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_CATEGORIA_CH> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(FORNECEDOR_CATEGORIA_CH entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IEnumerable<FORNECEDOR_CATEGORIA_CH> Find(Expression<Func<FORNECEDOR_CATEGORIA_CH, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_CATEGORIA_CH> Find(Expression<Func<FORNECEDOR_CATEGORIA_CH, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA_CH Get(int id)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA_CH Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA_CH Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA_CH GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(FORNECEDOR_CATEGORIA_CH entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDOR_CATEGORIA_CH entity)
        {
            throw new NotImplementedException();
        }
    }
}