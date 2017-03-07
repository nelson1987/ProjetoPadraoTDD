using WebForLink.Domain.Entities;
namespace WebForLink.Domain.Interfaces
{
    public interface IApiPagamento
    {
        string GerarLinkPagSeguro(Adesao adesao);
    }
}
