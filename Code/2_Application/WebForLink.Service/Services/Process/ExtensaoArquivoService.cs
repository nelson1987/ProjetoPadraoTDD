using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Infrastructure.Exceptions;

namespace WebForLink.Application.Services.Process
{
    public interface IExtensaoArquivoWebForLinkAppService
    {
    }

    public class ExtensaoArquivoWebForLinkAppService : AppService<WebForLinkContexto>,
        IExtensaoArquivoWebForLinkAppService
    {
        public ExtensaoArquivoWebForLinkAppService()
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