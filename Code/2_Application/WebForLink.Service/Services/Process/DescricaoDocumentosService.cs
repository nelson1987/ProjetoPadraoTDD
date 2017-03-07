using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class DescricaoDocumentosWebForLinkAppService : AppService<WebForLinkContexto>,
        IDescricaoDocumentosWebForLinkAppService
    {
        public DescricaoDocumentosWebForLinkAppService()
        {
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

        public DescricaoDeDocumentos Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DescricaoDeDocumentos Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DescricaoDeDocumentos GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DescricaoDeDocumentos> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DescricaoDeDocumentos> Find(Expression<Func<DescricaoDeDocumentos, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(DescricaoDeDocumentos entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(DescricaoDeDocumentos entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(DescricaoDeDocumentos entity)
        {
            throw new NotImplementedException();
        }

        public DescricaoDeDocumentos Get(int id)
        {
            throw new NotImplementedException();
        }

        public DescricaoDeDocumentos Get(Expression<Func<DescricaoDeDocumentos, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DescricaoDeDocumentos> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DescricaoDeDocumentos> Find(Expression<Func<DescricaoDeDocumentos, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}