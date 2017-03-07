using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Application.Services.Process
{
    public interface ICategoriaIrfCodWebForLinkAppService
    {
    }

    public class CategoriaIrfCodWebForLinkAppService : AppService<WebForLinkContexto>,
        ICategoriaIrfCodWebForLinkAppService
    {
        public CategoriaIrfCodWebForLinkAppService()
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