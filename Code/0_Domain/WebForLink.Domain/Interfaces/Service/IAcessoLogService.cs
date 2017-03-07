namespace WebForLink.Domain.Interfaces.Service
{
    public interface IAcessoLogWebForLinkService
    {
        void GravarLogAcesso(int usuarioId, string ip, string navegador);
    }
}