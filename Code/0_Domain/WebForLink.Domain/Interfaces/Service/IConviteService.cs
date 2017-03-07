namespace WebForLink.Domain.Interfaces.Service
{
    public interface IConviteService
    {
        void CriarConvite();
        void AtualizarConviteAposCliqueDoClienteWebFormat(string arquivo, string responsavel);
    }
}
