using System;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoModificacaoDadosGeraisSequenciaService
    {
    }
    public class SolicitacaoDadosGeraisSequenciaService : WebForLink.Domain.Services.Common.Service<>, ISolicitacaoModificacaoDadosGeraisSequenciaService
    {
        private readonly ISolicitacaoModificacaoDadosGeraisSequenciaRepository _processo;

        public SolicitacaoDadosGeraisSequenciaService(IUnitOfWork processo):base()

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

