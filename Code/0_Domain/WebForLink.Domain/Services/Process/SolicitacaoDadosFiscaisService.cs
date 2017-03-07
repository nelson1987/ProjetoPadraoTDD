using System;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class SolicitacaoDadosFiscaisService : WebForLink.Domain.Services.Common.Service<IUnitOfWork>
    {
        private readonly ISolicitacaoDadosFiscaisWebForLinkRepository Processo;

        public SolicitacaoDadosFiscaisService(ISolicitacaoDadosFiscaisWebForLinkRepository processo) : base(processo)
        {
            try
            {
                Processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
            Processo.Finalizar();
        }
    }

}
