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
    public class ResponsavelAppService : AppService<ChMasterDataContext>, IResponsavelAppService
    {
        private readonly IResponsavelService _Responsavelservice;

        public ResponsavelAppService(IResponsavelService service)
        {
            _Responsavelservice = service;
        }

        public IEnumerable<Responsavel> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Responsavel entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Responsavel> Find(Expression<Func<Responsavel, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Responsavel Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Responsavel Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Responsavel GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Responsavel entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Responsavel entity)
        {
            throw new NotImplementedException();
        }
    }
}