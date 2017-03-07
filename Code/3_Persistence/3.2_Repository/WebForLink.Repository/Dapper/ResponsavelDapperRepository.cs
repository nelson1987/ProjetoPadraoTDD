using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class ResponsavelDapperRepository : Common.Repository, IResponsavelReadOnlyRepository
    {
        public IEnumerable<Responsavel> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Responsavel> Find(Expression<Func<Responsavel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Responsavel Get(int id)
        {
            throw new NotImplementedException();
        }

        public Responsavel GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}