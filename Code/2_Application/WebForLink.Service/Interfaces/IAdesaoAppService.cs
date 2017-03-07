using Uol.PagSeguro.Domain;

namespace WebForLink.Application.Interfaces
{
    public interface IAdesaoWebForLinkAppService
    {
        Transaction BuscarTransacaoPagSeguro(string notificationCode);
    }
}
