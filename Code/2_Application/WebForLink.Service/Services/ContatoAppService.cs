using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class ContatoAppService : AppService<ChMasterDataContext>, IContatoAppService
    {
        private readonly IContatoService _contatoService;

        public ContatoAppService(IContatoService service)
        {
            _contatoService = service;
        }

        public ValidationResult UpdateOrCreate(List<Contato> entity)
        {
            try
            {
                BeginTransaction();
                entity.ForEach(x =>
                {
                    if (x.Id != 0)
                        _contatoService.UpdateReadOnly(x);
                    else
                        _contatoService.InsertReadOnly(x);
                });
                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ValidationResult Create(Contato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Contato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Contato entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Contato Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Contato Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Contato GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> Find(Expression<Func<Contato, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Contato Get(int id)
        {
            throw new NotImplementedException();
        }

        public Contato Get(Expression<Func<Contato, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> Find(Expression<Func<Contato, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(List<Contato> endereco)
        {
            foreach (var item in endereco)
            {
                ValidationResult.Add(Remove(item));
            }
            return ValidationResult;
        }
    }
}