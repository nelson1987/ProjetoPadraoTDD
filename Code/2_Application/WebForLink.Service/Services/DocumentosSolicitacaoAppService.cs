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
    public class DocumentosSolicitacaoAppService : AppService<ChMasterDataContext>, IDocumentosSolicitacaoAppService
    {
        private readonly IDocumentosSolicitacaoService _DocumentosSolicitacaoservice;

        public DocumentosSolicitacaoAppService(IDocumentosSolicitacaoService service)
        {
            _DocumentosSolicitacaoservice = service;
        }

        public IEnumerable<DocumentoSolicitacao> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(DocumentoSolicitacao entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentoSolicitacao> Find(Expression<Func<DocumentoSolicitacao, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentoSolicitacao Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentoSolicitacao Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentoSolicitacao GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(DocumentoSolicitacao entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(DocumentoSolicitacao entity)
        {
            throw new NotImplementedException();
        }
    }
}