using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;

namespace WebForLink.Application.Interfaces
{
    public interface IEmpresaAppService : IAppService<Empresa>
    {
        Empresa ProcessoLoginConvencional(string usuario, string senha);
    }
}
