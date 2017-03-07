using System;
using System.Collections.Generic;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public interface IDescricaoWebForLinkAppService
    {
        TIPO_DESCRICAO BuscarPorID(int id);
        List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId);
    }

    public class DescricaoWebForLinkAppService : AppService<WebForLinkContexto>, IDescricaoWebForLinkAppService
    {
        private readonly ITipoDescricaoWebForLinkService _descricao;

        public DescricaoWebForLinkAppService(ITipoDescricaoWebForLinkService descricao)
        {
            try
            {
                _descricao = descricao;
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
        public TIPO_DESCRICAO BuscarPorID(int id)
        {
            try
            {
                return _descricao.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma descrição por Id", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns></returns>
        public List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId)
        {
            try
            {
                return _descricao.ListarPorGrupoId(grupoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma descrição por grupoId", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}