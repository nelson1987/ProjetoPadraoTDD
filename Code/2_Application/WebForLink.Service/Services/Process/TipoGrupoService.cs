using System;
using System.Collections.Generic;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class TipoGrupoWebForLinkAppService : AppService<WebForLinkContexto>, ITipoGrupoWebForLinkAppService
    {
        private readonly ITipoGrupoWebForLinkService _tipoGrupoService;
        public TipoGrupoWebForLinkAppService(ITipoGrupoWebForLinkService tipoGrupo)
        {
            try
            {
                _tipoGrupoService = tipoGrupo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="visaoId"></param>
        /// <returns></returns>
        public List<TIPO_GRUPO> ListarGruposPorVisao(int visaoId)
        {
            try
            {
                return _tipoGrupoService.ListarGruposPorVisao(visaoId);
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