using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class ListasSolicitanteDapperRepository : Common.Repository, IListasSolicitanteReadOnlyRepository
    {
        public IEnumerable<ListasSolicitante> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListasSolicitante> Find(Expression<Func<ListasSolicitante, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ListasSolicitante Get(int id)
        {
            throw new NotImplementedException();
        }

        public ListasSolicitante GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}