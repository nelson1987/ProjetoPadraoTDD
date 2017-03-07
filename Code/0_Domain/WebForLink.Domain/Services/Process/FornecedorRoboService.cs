using System;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorRoboWebForLinkService : IService<ROBO>
    {
        ROBO BuscarPorId(int id);
        ROBO BuscarPorIdSolicitacao(int id);
        ROBO Buscar(Expression<Func<ROBO, bool>> filtro);
    }

    public class FornecedorRoboWebForLinkService : Service<ROBO>, IFornecedorRoboWebForLinkService
    {
        private readonly IRoboWebForLinkRepository _roboFornecedorRepository;

        public FornecedorRoboWebForLinkService(IRoboWebForLinkRepository roboFornecedorRepository)
            : base(roboFornecedorRepository)
        {
            try
            {
                _roboFornecedorRepository = roboFornecedorRepository;
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
                return _roboFornecedorRepository.Get(filtro);
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
                return _roboFornecedorRepository.Get(id);
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
                return _roboFornecedorRepository.Find(x => x.SOLICITACAO_ID == id).FirstOrDefault();
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