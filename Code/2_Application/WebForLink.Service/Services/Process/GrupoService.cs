using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public interface IGrupoWebForLinkAppService
    {
        GRUPO BuscarPorID(int id);
        int QuantidadeEmpresa(int contratanteId);
    }

    public class GrupoWebForLinkAppService : AppService<WebForLinkContexto>, IGrupoWebForLinkAppService
    {
        private readonly IGrupoWebForLinkService _grupoService;

        public GrupoWebForLinkAppService(IGrupoWebForLinkService grupo)
        {
            try
            {
                _grupoService = grupo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public GRUPO BuscarPorID(int id)
        {
            try
            {
                return _grupoService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public int QuantidadeEmpresa(int contratanteId)
        {
            try
            {
                return _grupoService.QuantidadeEmpresa(contratanteId);
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