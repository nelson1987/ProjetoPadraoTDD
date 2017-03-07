using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class OrganizacaoComprasWebForLinkAppService : AppService<WebForLinkContexto>,
        IOrganizacaoComprasWebForLinkAppService
    {
        private readonly IContratanteOrganizacaoCompraService _contratanteOrganizacaoCompra;

        public OrganizacaoComprasWebForLinkAppService(IContratanteOrganizacaoCompraService contratanteOrganizacaoCompra)
        {
            try
            {
                _contratanteOrganizacaoCompra = contratanteOrganizacaoCompra;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorId(int id)
        {
            try
            {
                return _contratanteOrganizacaoCompra.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Organização de Compras", ex);
            }
        }

        public List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante)
        {
            try
            {
                return _contratanteOrganizacaoCompra.Find(x => x.ID == idContratante).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Organização de Compras", ex);
            }
        }

        public CONTRATANTE_ORGANIZACAO_COMPRAS Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_ORGANIZACAO_COMPRAS Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_ORGANIZACAO_COMPRAS GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_ORGANIZACAO_COMPRAS> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_ORGANIZACAO_COMPRAS> Find(
            Expression<Func<CONTRATANTE_ORGANIZACAO_COMPRAS, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(CONTRATANTE_ORGANIZACAO_COMPRAS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(CONTRATANTE_ORGANIZACAO_COMPRAS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(CONTRATANTE_ORGANIZACAO_COMPRAS entity)
        {
            throw new NotImplementedException();
        }
    }
}