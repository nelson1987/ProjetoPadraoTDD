using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Application.Services.Process
{
    public interface IContrantanteFornecedorBancoWebForLinkAppService
    {
    }

    public class ContrantanteFornecedorBancoWebForLinkAppService : AppService<WebForLinkContexto>,
        IContrantanteFornecedorBancoWebForLinkAppService
    {
        public ContrantanteFornecedorBancoWebForLinkAppService()
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