using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public interface IDocumentosCompartilhadosWebForLinkAppService
    {
        Compartilhamentos BuscarPorID(int id);
    }

    public class DocumentosCompartilhadosWebForLinkAppService : AppService<WebForLinkContexto>,
        IDocumentosCompartilhadosWebForLinkAppService
    {
        private readonly ICompartilhamentoWebForLinkService _compartilhamentoService;

        public DocumentosCompartilhadosWebForLinkAppService(ICompartilhamentoWebForLinkService compartilhamentos)
        {
            try
            {
                _compartilhamentoService = compartilhamentos;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Compartilhamentos BuscarPorID(int id)
        {
            try
            {
                return _compartilhamentoService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}