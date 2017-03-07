using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class CarrinhoDapperRepository : Common.Repository, ICarrinhoReadOnlyRepository
    {
        public IEnumerable<Carrinho> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Carrinho> Find(Expression<Func<Carrinho, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Carrinho Get(int id)
        {
            throw new NotImplementedException();
        }

        public Carrinho GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}