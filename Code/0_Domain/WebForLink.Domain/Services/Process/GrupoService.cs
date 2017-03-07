using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IGrupoWebForLinkService : IService<GRUPO>
    {
        GRUPO BuscarPorID(int id);
        int QuantidadeEmpresa(int contratanteId);
    }

    public class GrupoWebForLinkService : Service<GRUPO>, IGrupoWebForLinkService
    {
        private readonly IGrupoWebForLinkRepository _grupoRepository;

        public GrupoWebForLinkService(IGrupoWebForLinkRepository grupo) : base(grupo)
        {
            try
            {
                _grupoRepository = grupo;
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
                return _grupoRepository.Get(id);
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
                return _grupoRepository.QuantidadeEmpresa(contratanteId);
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