using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IContrantanteFornecedorBancoService
    {
    }

    public class ContrantanteFornecedorBancoService : WebForLink.Domain.Services.Common.Service<FORN>, IContrantanteFornecedorBancoService
    {
        private readonly IUnitOfWork _processo;

        public ContrantanteFornecedorBancoService(IUnitOfWork processo)
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

