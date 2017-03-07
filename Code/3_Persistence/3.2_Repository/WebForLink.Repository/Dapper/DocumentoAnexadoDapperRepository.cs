using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class DocumentoAnexadoDapperRepository : Common.Repository, IDocumentoAnexadoReadOnlyRepository
    {
        public IEnumerable<DocumentoAnexado> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentoAnexado> Find(Expression<Func<DocumentoAnexado, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DocumentoAnexado Get(int id)
        {
            throw new NotImplementedException();
        }

        public DocumentoAnexado GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}