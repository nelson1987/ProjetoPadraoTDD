using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class ListaDocumentoDapperRepository : Common.Repository, IListaDocumentoReadOnlyRepository
    {
        public IEnumerable<ListaDocumento> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaDocumento> Find(Expression<Func<ListaDocumento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ListaDocumento Get(int id)
        {
            throw new NotImplementedException();
        }

        public ListaDocumento GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}