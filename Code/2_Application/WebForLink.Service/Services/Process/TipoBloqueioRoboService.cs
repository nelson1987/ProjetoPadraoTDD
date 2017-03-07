using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public class TipoBloqueioRoboWebForLinkAppService : AppService<WebForLinkContexto>,
        ITipoBloqueioRoboWebForLinkAppService
    {
        private readonly ITipoFuncaoBloqueioWebForLinkService _tipoBloqueioService;

        public TipoBloqueioRoboWebForLinkAppService(ITipoFuncaoBloqueioWebForLinkService tipoBloqueio)
        {
            try
            {
                _tipoBloqueioService = tipoBloqueio;
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
                return _tipoBloqueioService.Get(id);
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
                return _tipoBloqueioService.All().OrderBy(a => a.FUNCAO_BLOQ_COD).ToList();
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