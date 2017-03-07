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
    public class CarrinhoAppService : AppService<ChMasterDataContext>, ICarrinhoAppService
    {
        private readonly ICarrinhoService _Carrinhoservice;

        public CarrinhoAppService(ICarrinhoService service)
        {
            _Carrinhoservice = service;
        }

        public IEnumerable<Carrinho> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Carrinho entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Carrinho> Find(Expression<Func<Carrinho, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Carrinho Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Carrinho Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Carrinho GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Carrinho entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Carrinho entity)
        {
            throw new NotImplementedException();
        }
    }
}