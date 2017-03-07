using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class CategoriaFornecedorChWebForLinkService : Service<FORNECEDOR_CATEGORIA_CH>,
        ICategoriaFornecedorChWebForLinkService
    {
        public CategoriaFornecedorChWebForLinkService(ICategoriaFornecedorCHWebForLinkRepository processo)
            : base(processo)
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

        public ICategoriaFornecedorCHWebForLinkRepository Processo { get; private set; }

        public void Dispose()
        {
        }
    }
}