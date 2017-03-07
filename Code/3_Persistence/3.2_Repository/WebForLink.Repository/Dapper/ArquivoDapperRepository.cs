using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class ArquivoDapperRepository : Common.Repository, IArquivoReadOnlyRepository
    {
        public IEnumerable<Arquivo> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Arquivo> Find(Expression<Func<Arquivo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Arquivo Get(int id)
        {
            throw new NotImplementedException();
        }

        public Arquivo GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}