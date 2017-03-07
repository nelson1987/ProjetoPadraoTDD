using System;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Domain.Services.Process
{
    public interface ICategoriaIrfCodService : I
    {
    }
    public class CategoriaIrfCodBP : WebForLink.Domain.Services.Common.Service<CATEGORIA_IRF>, ICategoriaIrfCodService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaIrfCodBP(IUnitOfWork processo)
        {
            try
            {
                _unitOfWork = processo;
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
