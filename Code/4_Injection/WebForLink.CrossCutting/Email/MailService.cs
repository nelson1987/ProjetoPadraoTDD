using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace WebForLink.CrossCutting.InversionControl.Email
{
    public interface IEMailService
    {
        void EnviarEmail(string email, string assunto, string mensagem);
        void EnviarEmail(string email, string assunto, string mensagem, Stream arquivo, string nomeArquivo);
    }
    public interface IServidorEMailService
    {
        string Email { get; }
        string HostEmail { get; }
        int PortEmail { get; }
        string CredentialEmail { get; }
    }
    public class MailServiceException : Exception
    {
        public MailServiceException(string mensagem) : base(mensagem) { }
        public MailServiceException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
    }
    public class MailService : IEMailService
    {
        public MailService(string emailSistema, string hostSistema, int portaSistema, string senhaSistema)
        {
            Email = emailSistema;
            HostEmail = hostSistema;
            PortEmail = portaSistema;
            CredentialEmail = senhaSistema;
            Validar();
        }
        public MailService(IServidorEMailService servidor)
        {
            Email = servidor.Email;
            HostEmail = servidor.HostEmail;
            PortEmail = servidor.PortEmail;
            CredentialEmail = servidor.CredentialEmail;
            Validar();
        }
        private string Email { get; }
        private string HostEmail { get; }
        private int PortEmail { get; }
        private string CredentialEmail { get; }
        private void Validar()
        {
            if (string.IsNullOrEmpty(Email))
                throw new MailServiceException("Valor do e-mail do sistema deve estar configurado.");
            if (string.IsNullOrEmpty(HostEmail))
                throw new MailServiceException("Valor do servidor de e-mail do sistema deve estar configurado.");
            if (PortEmail != 0)
                throw new MailServiceException("Valor do porta do servidor de e-mail do sistema deve estar configurado.");
            if (string.IsNullOrEmpty(CredentialEmail))
                throw new MailServiceException("Valor da senha do e-mail do sistema deve estar configurado.");
        }
        public void EnviarEmail(string email, string assunto, string mensagem)
        {
            try
            {
                // ENVIA O EMAIL
                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress(Email);
                objEmail.To.Add(email);
                objEmail.Priority = MailPriority.Normal;
                objEmail.IsBodyHtml = true;
                objEmail.Subject = assunto;
                objEmail.Body = mensagem;
                SmtpClient objSmtp = new SmtpClient();
                objSmtp.Host = HostEmail;
                objSmtp.Port = PortEmail;
                objSmtp.Credentials = new NetworkCredential(Email, CredentialEmail);
                objSmtp.Send(objEmail);

                //return true;
            }
            catch (Exception ex)
            {
                //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                //Log.Error(ex.Message);
                //return false;
                throw new MailServiceException("Erro ao tentar enviar um e-mail.", ex);
            }
        }

        public void EnviarEmail(string email, string assunto, string mensagem, Stream arquivo, string nomeArquivo)
        {
            try
            {
                /*
                 * 1 - Buscar o arquivo anexo
                 * Pegar o arquivo físico e anexar no email
                 */

                Attachment attachment;
                attachment = new Attachment(arquivo, nomeArquivo);

                // ENVIA O EMAIL
                MailMessage objEmail = new MailMessage
                {
                    From = new MailAddress(Email),
                    Priority = MailPriority.Normal,
                    IsBodyHtml = true,
                    Subject = assunto,
                    Body = mensagem
                };
                objEmail.To.Add(email);
                objEmail.Attachments.Add(attachment);

                //objEmail.Body += "Link: " + url;
                SmtpClient objSmtp = new SmtpClient
                {
                    Host = HostEmail,
                    Port = PortEmail,
                    Credentials = new NetworkCredential(Email, CredentialEmail)
                };
                objSmtp.Send(objEmail);

                //return true;
            }
            catch (Exception ex)
            {
                throw new MailServiceException("Erro ao tentar enviar um e-mail.", ex);
                //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                //Log.Error(ex.Message);
                //return false;
            }
        }

    }
}
