using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class TipoDocumentosWebForLinkService : Service<TipoDeDocumento>, ITipoDocumentosWebForLinkService
    {
        private readonly ITipoDocumentoWebForLinkRepository _tipoDocumento;

        public TipoDocumentosWebForLinkService(ITipoDocumentoWebForLinkRepository tipoDocumento)
            : base(tipoDocumento)
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

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public List<TipoDeDocumento> ListarPorIdContratante(int contratanteId)
        {
            try
            {
                //return _tipoDocumento.Find(x => x.ATIVO && x.CONTRATANTE_ID == contratanteId, e => e.DESCRICAO).ToList();
                return _tipoDocumento.Find(x => x.ATIVO && x.CONTRATANTE_ID == contratanteId).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Tipo De Documento", ex);
            }
        }
    }
}