using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class FornecedorBaseContatoService : Service<FORNECEDORBASE_CONTATOS>
    {
        private readonly IFornecedorBaseContatosWebForLinkRepository _fornecedorBaseRepositoryContatos;

        public FornecedorBaseContatoService(IFornecedorBaseContatosWebForLinkRepository fornecedorBaseRepositoryContatos)
            : base(fornecedorBaseRepositoryContatos)
        {
            try
            {
                _fornecedorBaseRepositoryContatos = fornecedorBaseRepositoryContatos;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public FORNECEDORBASE_CONTATOS BuscarPorID(int id)
        {
            try
            {
                return _fornecedorBaseRepositoryContatos.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar os Contatos dos Fornecedores Base por id", ex);
            }
        }
    }
}