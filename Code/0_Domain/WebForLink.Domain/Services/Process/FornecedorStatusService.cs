using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class FornecedorStatusWebForLinkService : Service<FORNECEDOR_STATUS>
    {
        private readonly IFornecedorStatusWebForLinkRepository _statusFornecedorRepository;

        public FornecedorStatusWebForLinkService(IFornecedorStatusWebForLinkRepository statusFornecedorRepository)
            : base(statusFornecedorRepository)
        {
            try
            {
                _statusFornecedorRepository = statusFornecedorRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FORNECEDOR_STATUS BuscarPorID(int id)
        {
            try
            {
                return _statusFornecedorRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um status de fornecedor por ID", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}