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
    public class SolicitadoAppService : AppService<ChMasterDataContext>, ISolicitadoAppService
    {
        private readonly ISolicitadoService _Solicitadoservice;

        public SolicitadoAppService(ISolicitadoService service)
        {
            _Solicitadoservice = service;
        }

        public IEnumerable<Solicitado> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Solicitado entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult CriarSolicitado(Solicitado solicitado, List<Responsavel> responsaveis)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitado> Find(Expression<Func<Solicitado, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Solicitado Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Solicitado Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Solicitado GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Solicitado entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Solicitado entity)
        {
            throw new NotImplementedException();
        }
    }
}