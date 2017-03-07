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
    public class BancoAppService : AppService<ChMasterDataContext>, IBancoAppService
    {
        private readonly IBancoService _fichaCadastralService;

        public BancoAppService(IBancoService fichaCadastral)
        {
            _fichaCadastralService = fichaCadastral;
        }

        public Banco Get(Expression<Func<Banco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Banco> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Banco> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Banco orderDetail)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Banco> Find(Expression<Func<Banco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Banco> Find(Expression<Func<Banco, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Banco Get(int id)
        {
            throw new NotImplementedException();
        }

        public Banco Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Banco Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Banco GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Banco orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Banco orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult UpdateOrCreate(List<Banco> entity)
        {
            try
            {
                BeginTransaction();
                entity.ForEach(x =>
                {
                    if (x.Id != 0)
                        _fichaCadastralService.UpdateReadOnly(x);
                    else
                        _fichaCadastralService.InsertReadOnly(x);
                });
                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Banco Insert(Banco predicate)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(List<Banco> endereco)
        {
            foreach (var item in endereco)
            {
                ValidationResult.Add(Remove(item));
            }
            return ValidationResult;
        }
    }
}