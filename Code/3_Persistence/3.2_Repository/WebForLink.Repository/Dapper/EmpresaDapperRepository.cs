using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class EmpresaDapperRepository : Common.Repository, IEmpresaReadOnlyRepository
    {
        public IEnumerable<Empresa> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empresa> Find(Expression<Func<Empresa, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Empresa Get(int id)
        {
            throw new NotImplementedException();
        }

        public Empresa GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }

        Empresa IReadOnlyRepository<Empresa>.GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }

    }
}
