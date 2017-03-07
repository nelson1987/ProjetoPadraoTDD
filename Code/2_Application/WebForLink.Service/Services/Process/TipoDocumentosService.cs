using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TipoDocumentosWebForLinkAppService : AppService<WebForLinkContexto>,
        ITipoDocumentosWebForLinkAppService
    {
        private readonly ITipoDocumentoWebForLinkService _tipoDocumento;

        public TipoDocumentosWebForLinkAppService(ITipoDocumentoWebForLinkService tipoDocumento)
        {
            try
            {
                _tipoDocumento = tipoDocumento;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public TipoDeDocumento Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public TipoDeDocumento Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public TipoDeDocumento GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoDeDocumento> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoDeDocumento> Find(Expression<Func<TipoDeDocumento, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(TipoDeDocumento entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(TipoDeDocumento entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(TipoDeDocumento entity)
        {
            throw new NotImplementedException();
        }

        public TipoDeDocumento Get(int id)
        {
            throw new NotImplementedException();
        }

        public TipoDeDocumento Get(Expression<Func<TipoDeDocumento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoDeDocumento> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoDeDocumento> Find(Expression<Func<TipoDeDocumento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public List<TipoDeDocumento> ListarPorIdContratante(int contratanteId)
        {
            try
            {
                return
                    _tipoDocumento.Find(x => x.ATIVO && x.CONTRATANTE_ID == contratanteId)
                        .OrderBy(e => e.DESCRICAO)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Tipo De Documento", ex);
            }
        }
        public List<TipoDeDocumento> ListarDocumentosPorTipo(int contratanteId)
        {
            try
            {
                return
                    _tipoDocumento.Find(x => x.ATIVO 
                    && x.CONTRATANTE_ID == contratanteId)
                        .OrderBy(e => e.DESCRICAO)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Tipo De Documento", ex);
            }
        }
    }
}