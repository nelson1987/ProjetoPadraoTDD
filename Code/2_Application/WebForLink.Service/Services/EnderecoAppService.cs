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
    public class EnderecoAppService : AppService<ChMasterDataContext>, IEnderecoAppService
    {
        private readonly IEnderecoService _fichaCadastralService;

        public EnderecoAppService(IEnderecoService fichaCadastral):base()
        {
            _fichaCadastralService = fichaCadastral;
        }

        public Endereco Get(Expression<Func<Endereco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Endereco> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Endereco> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Endereco orderDetail)
        {
            BeginTransaction();
            _fichaCadastralService.Add(orderDetail);
            Commit();
            return ValidationResult;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Endereco> Find(Expression<Func<Endereco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Endereco> Find(Expression<Func<Endereco, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Endereco Get(int id)
        {
            throw new NotImplementedException();
        }

        public Endereco Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Endereco Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Endereco GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Endereco orderDetail)
        {
            BeginTransaction();

                    _fichaCadastralService.Delete(orderDetail);
            Commit();
            return ValidationResult;
        }

        public ValidationResult Update(Endereco orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult UpdateOrCreate(List<Endereco> entity)
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

        public Endereco Insert(Endereco predicate)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(ICollection<Endereco> endereco)
        {
            foreach (var item in endereco)
            {
               ValidationResult.Add(Remove(item));
            }
            return ValidationResult;
        }
    }
}