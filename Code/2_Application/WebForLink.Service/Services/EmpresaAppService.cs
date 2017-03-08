using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class EmpresaAppService : AppService<WebForLinkContexto>, IEmpresaAppService
    {
        private readonly IEmpresaService _Solicitanteservice;

        public EmpresaAppService(IEmpresaService service)
        {
            _Solicitanteservice = service;
        }
        public IEnumerable<Empresa> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Empresa entity)
        {
            BeginTransaction();
            var resultado = _Solicitanteservice.Add(entity);
            Commit();
            return resultado;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empresa> Find(Expression<Func<Empresa, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Empresa Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Empresa Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Empresa GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Empresa ProcessoLoginConvencional(string usuario, string senha)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Empresa entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Empresa entity)
        {
            throw new NotImplementedException();
        }
    }
}
