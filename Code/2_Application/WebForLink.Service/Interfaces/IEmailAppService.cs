namespace WebForLink.Application.Interfaces
{
    public interface IEmailAppService
    {
        void IncluirMensagemEmail(string mensagem);
        void IncluirAssuntoEmail(string assunto);
        void EnviarEmail(string emailDestino);
    }
}