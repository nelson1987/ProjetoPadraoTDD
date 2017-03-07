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
    public class ListasSolicitanteAppService : AppService<ChMasterDataContext>, IListasSolicitanteAppService
    {
        public ListasSolicitanteAppService(IListasSolicitanteService service)
        {
            ListasSolicitanteservice = service;
        }

        public IListasSolicitanteService ListasSolicitanteservice { get; private set; }

        public IEnumerable<ListasSolicitante> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(ListasSolicitante entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListasSolicitante> Find(Expression<Func<ListasSolicitante, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ListasSolicitante Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ListasSolicitante Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ListasSolicitante GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(ListasSolicitante entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(ListasSolicitante entity)
        {
            throw new NotImplementedException();
        }
    }
}