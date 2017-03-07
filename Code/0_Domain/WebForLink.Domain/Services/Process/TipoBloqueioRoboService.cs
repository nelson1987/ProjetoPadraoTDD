using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ITipoBloqueioRoboWebForLinkService
    {
        List<TIPO_FUNCAO_BLOQUEIO> ListarTodosPorCodigoFuncaoBloqueio();
        TIPO_FUNCAO_BLOQUEIO BuscarPorID(int id);
    }

    public class TipoBloqueioRoboWebForLinkService : Service<TIPO_FUNCAO_BLOQUEIO>, ITipoBloqueioRoboWebForLinkService
    {
        private readonly ITipoFuncaoBloqueioWebForLinkRepository _tipoBloqueioRepository;

        public TipoBloqueioRoboWebForLinkService(ITipoFuncaoBloqueioWebForLinkRepository tipoBloqueio)
            : base(tipoBloqueio)
        {
            try
            {
                _tipoBloqueioRepository = tipoBloqueio;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public TIPO_FUNCAO_BLOQUEIO BuscarPorID(int id)
        {
            try
            {
                return _tipoBloqueioRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Função Bloqueio", ex);
            }
        }

        public List<TIPO_FUNCAO_BLOQUEIO> ListarTodosPorCodigoFuncaoBloqueio()
        {
            try
            {
                return _tipoBloqueioRepository.All().OrderBy(a => a.FUNCAO_BLOQ_COD).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro a lista de função de bloqueio!", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}