using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class SolicitacaoStatusService : Service<SOLICITACAO_STATUS>
    {
        private readonly ISolicitacaoStatusWebForLinkRepository _statusSolicitacaoRepository;

        public SolicitacaoStatusService(ISolicitacaoStatusWebForLinkRepository statusSolicitacaoRepository)
            : base(statusSolicitacaoRepository)
        {
            try
            {
                _statusSolicitacaoRepository = statusSolicitacaoRepository;
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
        public SOLICITACAO_STATUS BuscarPorID(int id)
        {
            try
            {
                return _statusSolicitacaoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}