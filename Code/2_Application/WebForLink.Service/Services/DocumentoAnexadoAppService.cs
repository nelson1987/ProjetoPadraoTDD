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
    public class DocumentoAnexadoAppService : AppService<ChMasterDataContext>, IDocumentoAnexadoAppService
    {
        private readonly IDocumentoAnexadoService _DocumentoAnexadoservice;

        public DocumentoAnexadoAppService(IDocumentoAnexadoService service)
        {
            _DocumentoAnexadoservice = service;
        }

        public IEnumerable<DocumentoAnexado> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(DocumentoAnexado entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentoAnexado> Find(Expression<Func<DocumentoAnexado, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentoAnexado Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentoAnexado Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentoAnexado GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(DocumentoAnexado entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(DocumentoAnexado entity)
        {
            throw new NotImplementedException();
        }
    }
}