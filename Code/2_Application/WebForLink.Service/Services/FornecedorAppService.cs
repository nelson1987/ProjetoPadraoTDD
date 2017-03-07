using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class FornecedorAppService : AppService<MusicStoreContext>, IFornecedorAppService
    {
        private readonly IFornecedorService _service;

        public FornecedorAppService(IFornecedorService albumService)
        {
            _service = albumService;
        }

        public IEnumerable<Fornecedor> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fornecedor> BuscarFornecedor(int id)
        {
            return _service
                .Find(x => x.Id == id, true)
                .AsEnumerable();
        }
        public Fornecedor Criar(Fornecedor fornecedor)
        {
            BeginTransaction();
            ValidationResult.Add(_service.Add(fornecedor));
            if (ValidationResult.EstaValidado)
                Commit();
            return fornecedor;
        }
        public ValidationResult Create(Fornecedor fornecedor)
        {

            BeginTransaction();
            ValidationResult.Add(_service.Add(fornecedor));
            if (ValidationResult.EstaValidado) Commit();

            return ValidationResult;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Fornecedor> Find(Expression<Func<Fornecedor, bool>> predicate, bool @readonly = false)
        {
            return _service.Find(predicate, @readonly);
        }

        public Fornecedor Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Fornecedor orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Fornecedor orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}