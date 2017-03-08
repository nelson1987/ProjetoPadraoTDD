using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository.ReadOnly
{
    public interface IUsuarioReadOnlyRepository : IReadOnlyRepository<Usuario>
    {
        Usuario GetAllReferencesFichaCadastral(int id);
    }
}