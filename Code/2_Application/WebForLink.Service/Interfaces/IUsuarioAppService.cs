using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;

namespace WebForLink.Application.Interfaces
{
    public interface IUsuarioAppService : IAppService<Usuario>
    {
        Usuario ProcessoLoginConvencional(string usuario, string senha);
    }
}