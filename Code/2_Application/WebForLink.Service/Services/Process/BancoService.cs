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
    public interface IBancoWebForLinkAppService
    {
        List<TiposDeBanco> ListarTodosPorNome();
        List<TiposDeBanco> BuscarBancosPorId(List<int> lstBancoId);
    }

    public class BancoWebForLinkAppService : AppService<WebForLinkContexto>, IBancoWebForLinkAppService
    {
        private readonly IBancoWebForLinkService _bancoService;

        public BancoWebForLinkAppService(IBancoWebForLinkService banco)
        {
            try
            {
                _bancoService = banco;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<TiposDeBanco> ListarTodosPorNome()
        {
            try
            {
                return _bancoService.All().OrderBy(a => a.BANCO_NM).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao listar os bancos", ex);
            }
        }

        public List<TiposDeBanco> BuscarBancosPorId(List<int> lstBancoId)
        {
            return _bancoService.Find(x => lstBancoId.Contains(x.ID)).ToList();
        }

        public void Dispose()
        {
        }
    }
}