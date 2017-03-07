using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using WebForLink.Application.Interfaces;

namespace WebForLink.Application.Services
{
    public class EmailAppService : IEmailAppService
    {
        public string MensagemEmail { get; private set; }
        public string AssuntoEmail { get; private set; }

        private int PortaServidor
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["PortEmail"]) ? 0 : int.Parse(ConfigurationManager.AppSettings["PortEmail"]);
            }
        }

        public void IncluirMensagemEmail(string mensagem)
        {
            MensagemEmail = mensagem;
        }
        public void IncluirAssuntoEmail(string assunto)
        {
            AssuntoEmail = assunto;
        }
        public void EnviarEmail(string emailDestino)
        {
            var emailHost = ConfigurationManager.AppSettings["Email"];
            var senhaHost = ConfigurationManager.AppSettings["CredentialEmail"];
            var hostLocal = ConfigurationManager.AppSettings["HostEmail"];

            //client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            
            mail.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
            mail.To.Add(emailDestino);
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.Subject = AssuntoEmail;
            mail.Body = MensagemEmail;
            mail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            SmtpClient client = new SmtpClient();
            client.Host = hostLocal;
            if (PortaServidor != 0)
                client.Port = PortaServidor;
            client.Credentials = new System.Net.NetworkCredential(emailHost, senhaHost);

            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
                Debug.Write(erro);
            }
            finally
            {
                mail = null;
            }
            //throw new NotImplementedException();
        }
    }
}