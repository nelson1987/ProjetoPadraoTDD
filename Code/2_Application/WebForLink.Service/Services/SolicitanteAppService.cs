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
    public class SolicitanteAppService : AppService<ChMasterDataContext>, ISolicitanteAppService
    {
        private readonly ISolicitanteService _Solicitanteservice;

        public SolicitanteAppService(ISolicitanteService service)
        {
            _Solicitanteservice = service;
        }

        public IEnumerable<ListaDocumento> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(ListaDocumento entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult CriarSolicitante(Solicitante solicitante)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
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

        public ValidationResult Remove(ListaDocumento entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(ListaDocumento entity)
        {
            throw new NotImplementedException();
        }
    }
}