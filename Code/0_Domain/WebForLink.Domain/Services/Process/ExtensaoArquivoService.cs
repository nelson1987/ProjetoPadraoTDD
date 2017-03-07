using System;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IExtensaoArquivoService : IService<>
    {
    }
    public class ExtensaoArquivoService : WebForLink.Domain.Services.Common.Service<IUnitOfWork>, IExtensaoArquivoService
    {
        private readonly IUnitOfWork _processo;

        public ExtensaoArquivoService(IUnitOfWork processo)
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

