using WebForLink.Domain.Infrastructure.FiltrosDTO;

namespace WebForLink.Application.Interfaces
{
    public interface IProcessoLoginWebForLinkAppService
    {
        ProcessoLoginDTO ExecutarLogin(string login, string senha);
    }
}
