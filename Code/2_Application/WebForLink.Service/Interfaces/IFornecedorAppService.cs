using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;

namespace WebForLink.Application.Interfaces
{
    public interface IFornecedorAppService : IAppService<Fornecedor>
    {
        IEnumerable<Fornecedor> BuscarFornecedor(int id);
    }
}