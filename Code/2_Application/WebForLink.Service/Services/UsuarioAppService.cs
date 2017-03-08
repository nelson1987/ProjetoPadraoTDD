using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class UsuarioAppService : AppService<WebForLinkContexto>, IUsuarioAppService
    {
        private readonly IUsuarioService _Solicitanteservice;

        public UsuarioAppService(IUsuarioService service)
        {
            _Solicitanteservice = service;
        }

        public ValidationResult Create(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Usuario Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Usuario Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Usuario GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> Find(Expression<Func<Usuario, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Usuario ProcessoLoginConvencional(string usuario, string senha)
        {
           return _Solicitanteservice.Get(x => x.Login == usuario);
        }
    }
}