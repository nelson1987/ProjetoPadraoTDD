using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class SolicitanteDapperRepository : Common.Repository, ISolicitanteReadOnlyRepository
    {
        public IEnumerable<Solicitante> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitante> Find(Expression<Func<Solicitante, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Solicitante Get(int id)
        {
            throw new NotImplementedException();
        }

        public Solicitante GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}