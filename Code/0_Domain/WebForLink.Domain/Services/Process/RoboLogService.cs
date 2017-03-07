using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class RoboLogService : Service<ROBO_LOG>
    {
        private readonly ILogRoboWebForLinkRepository _logRoboRepository;

        public RoboLogService(ILogRoboWebForLinkRepository logRoboRepository) : base(logRoboRepository)
        {
            _logRoboRepository = logRoboRepository;
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Inserir(ROBO_LOG entidade)
        {
            try
            {
                _logRoboRepository.Add(entidade);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um log do robô de fornecedor por ID", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}