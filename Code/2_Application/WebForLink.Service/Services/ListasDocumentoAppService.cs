using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class ListasDocumentoAppService : AppService<ChMasterDataContext>, IListasDocumentoAppService
    {
        private readonly IListasDocumentoService _ListasDocumentosservice;

        public ListasDocumentoAppService(IListasDocumentoService service)
        {
            _ListasDocumentosservice = service;
        }

        public IEnumerable<ListaDocumento> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Contato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(ListaDocumento entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> Find(Expression<Func<Contato, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaDocumento> Find(Expression<Func<ListaDocumento, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ListaDocumento Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ListaDocumento Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ListaDocumento GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Contato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(ListaDocumento entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Contato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(ListaDocumento entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Contato> IAppService<Contato>.All(bool @readonly)
        {
            throw new NotImplementedException();
        }

        Contato IAppService<Contato>.Get(string id, bool @readonly)
        {
            throw new NotImplementedException();
        }

        Contato IAppService<Contato>.Get(int id, bool @readonly)
        {
            throw new NotImplementedException();
        }

        Contato IAppService<Contato>.GetAllReferences(int id, bool @readonly)
        {
            throw new NotImplementedException();
        }
    }
}