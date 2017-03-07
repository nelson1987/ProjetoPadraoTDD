using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IFornecedorEnderecoRepository : IRepository<FORNECEDOR_ENDERECO>
    {
    }

    public class FornecedorEnderecoRepository : EntityFrameworkRepository<FORNECEDOR_ENDERECO, WebForLinkContexto>,
        IFornecedorEnderecoRepository
    {
    }
}