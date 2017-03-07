using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IDescricaoWebForLinkService : IService<TIPO_DESCRICAO>
    {
        TIPO_DESCRICAO BuscarPorID(int id);
        List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId);
    }

    public class DescricaoWebForLinkService : Service<TIPO_DESCRICAO>, IDescricaoWebForLinkService
    {
        private readonly ITipoDescricaoWebForLinkRepository _descricao;

        public DescricaoWebForLinkService(ITipoDescricaoWebForLinkRepository descricao) : base(descricao)
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