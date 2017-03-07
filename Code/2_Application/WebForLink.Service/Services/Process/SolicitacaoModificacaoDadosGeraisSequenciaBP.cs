using System;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoModificacaoDadosGeraisSequenciaWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoModificacaoDadosGeraisSequenciaWebForLinkAppService
    {
        public SolicitacaoModificacaoDadosGeraisSequenciaWebForLinkAppService()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }
}