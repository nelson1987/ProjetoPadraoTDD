using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class DescricaoDocumentosChWebForLinkAppService : AppService<WebForLinkContexto>,
        IDescricaoDocumentosChWebForLinkAppService
    {
        private readonly IDescricaoDocumentosChWebForLinkService _descricaoDocumentosCH;

        public DescricaoDocumentosChWebForLinkAppService(IDescricaoDocumentosChWebForLinkService descricaoDocumentosCH)
        {
            try
            {
                _descricaoDocumentosCH = descricaoDocumentosCH;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public WFD_DESCRICAO_DOCUMENTOS_CH Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public WFD_DESCRICAO_DOCUMENTOS_CH Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public WFD_DESCRICAO_DOCUMENTOS_CH GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WFD_DESCRICAO_DOCUMENTOS_CH> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WFD_DESCRICAO_DOCUMENTOS_CH> Find(
            Expression<Func<WFD_DESCRICAO_DOCUMENTOS_CH, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(WFD_DESCRICAO_DOCUMENTOS_CH entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(WFD_DESCRICAO_DOCUMENTOS_CH entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(WFD_DESCRICAO_DOCUMENTOS_CH entity)
        {
            throw new NotImplementedException();
        }

        public WFD_DESCRICAO_DOCUMENTOS_CH Get(int id)
        {
            throw new NotImplementedException();
        }

        public WFD_DESCRICAO_DOCUMENTOS_CH Get(Expression<Func<WFD_DESCRICAO_DOCUMENTOS_CH, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WFD_DESCRICAO_DOCUMENTOS_CH> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WFD_DESCRICAO_DOCUMENTOS_CH> Find(
            Expression<Func<WFD_DESCRICAO_DOCUMENTOS_CH, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WFD_DESCRICAO_DOCUMENTOS_CH BuscarPorID(int id)
        {
            try
            {
                return _descricaoDocumentosCH.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma descrição de documentos CH por Id", ex);
            }
        }
    }
}