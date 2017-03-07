using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ITipoGrupoWebForLinkService
    {
        List<TIPO_GRUPO> ListarGruposPorVisao(int visaoId);
    }

    public class TipoGrupoWebForLinkService : Service<TIPO_GRUPO>, ITipoGrupoWebForLinkService
    {
        private readonly ITipoGrupoWebForLinkRepository _tipoGrupoRepository;

        public TipoGrupoWebForLinkService(ITipoGrupoWebForLinkRepository tipoGrupo) : base(tipoGrupo)
        {
            try
            {
                _tipoGrupoRepository = tipoGrupo;
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
                return _tipoGrupoRepository.ListarGruposPorVisao(visaoId);
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