using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class SolicitadoDapperRepository : Common.Repository, ISolicitadoReadOnlyRepository
    {
        public IEnumerable<Solicitado> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitado> Find(Expression<Func<Solicitado, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Solicitado Get(int id)
        {
            throw new NotImplementedException();
        }

        public Solicitado GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}