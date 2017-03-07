using System;
using System.Linq.Expressions;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorRoboWebForLinkAppService
    {
        ROBO BuscarPorId(int id);
        ROBO BuscarPorIdSolicitacao(int id);
        ROBO Buscar(Expression<Func<ROBO, bool>> filtro);
    }

    public class FornecedorRoboWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorRoboWebForLinkAppService
    {
        private readonly IRoboWebForLinkService _roboFornecedorService;

        public FornecedorRoboWebForLinkAppService(IRoboWebForLinkService roboFornecedorService)
        {
            try
            {
                _roboFornecedorService = roboFornecedorService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public ROBO Buscar(Expression<Func<ROBO, bool>> filtro)
        {
            try
            {
                return _roboFornecedorService.Get(filtro);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ROBO BuscarPorId(int id)
        {
            try
            {
                return _roboFornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ROBO BuscarPorIdSolicitacao(int id)
        {
            try
            {
                return _roboFornecedorService.Get(x => x.SOLICITACAO_ID == id);
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