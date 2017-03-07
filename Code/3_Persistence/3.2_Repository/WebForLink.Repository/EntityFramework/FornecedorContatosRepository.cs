using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IFornecedorContatoRepository : IRepository<FORNECEDOR_CONTATOS>
    {
    }

    public class FornecedorContatosRepository : EntityFrameworkRepository<FORNECEDOR_CONTATOS, WebForLinkContexto>,
        IFornecedorContatoRepository
    {
    }
}