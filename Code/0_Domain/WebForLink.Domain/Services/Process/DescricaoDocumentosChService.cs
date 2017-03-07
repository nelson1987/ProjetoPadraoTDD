using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class DescricaoDocumentosChService : Service<WFD_DESCRICAO_DOCUMENTOS_CH>,
        IDescricaoDocumentosChWebForLinkService
    {
        private readonly IDescricaoDocumentosChWebForLinkRepository _descricaoDocumentosCH;

        public DescricaoDocumentosChService(IDescricaoDocumentosChWebForLinkRepository descricaoDocumentosCH)
            : base(descricaoDocumentosCH)
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