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
    public class ArquivoAppService : AppService<ChMasterDataContext>, IArquivoAppService
    {
        private readonly IArquivoService _Arquivoservice;

        public ArquivoAppService(IArquivoService service)
        {
            _Arquivoservice = service;
        }

        public IEnumerable<Arquivo> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Arquivo entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Arquivo> Find(Expression<Func<Arquivo, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Arquivo Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Arquivo Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Arquivo GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Arquivo entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Arquivo entity)
        {
            throw new NotImplementedException();
        }
    }
}