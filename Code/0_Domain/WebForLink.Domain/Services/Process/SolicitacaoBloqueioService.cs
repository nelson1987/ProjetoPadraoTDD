using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class SolicitacaoBloqueioService : Service<SOLICITACAO_BLOQUEIO>
    {
        private readonly ISolicitacaoBloqueioWebForLinkRepository _solicitacaoBloqueioRepository;

        public SolicitacaoBloqueioService(ISolicitacaoBloqueioWebForLinkRepository solicitacaoBloqueioRepository)
            : base(solicitacaoBloqueioRepository)
        {
            try
            {
                _solicitacaoBloqueioRepository = solicitacaoBloqueioRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO_BLOQUEIO BuscarPorID(int id)
        {
            try
            {
                return _solicitacaoBloqueioRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}