using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using WebForLink.CrossCutting.InversionControl.Exceptions;

namespace WebForLink.CrossCutting.InversionControl.Email
{
    public class EmailsService : IEmail
    {
        public string De { get; set; }
        public List<MailAddress> Para { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Servidor { get; set; }
        public string Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public Stream Arquivo { get; set; }
        public string NomeArquivo { get; set; }
        private void MontarEmail(MailMessage objEmail)
        {
            objEmail.From = new MailAddress(De);
            Para.ForEach(x =>
            {
                objEmail.To.Add(x);
            });
            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = Assunto;
            objEmail.Body = Mensagem;
        }
        private void MontarSmtp(SmtpClient objSmtp)
        {
            objSmtp.Host = Servidor;
            objSmtp.Port = Convert.ToInt32(Porta);
            objSmtp.Credentials = new NetworkCredential(Usuario, Senha);
        }
        public bool EnviarEmail()
        {
            try
            {
                /*
                 * 1 - Buscar o arquivo anexo
                 * Pegar o arquivo físico e anexar no email
                 */
                MailMessage objEmail = new MailMessage();
                MontarEmail(objEmail);
                if (Arquivo.Length > 0)
                {
                    var attachment = new Attachment(Arquivo, NomeArquivo);
                    objEmail.Attachments.Add(attachment);
                }
                SmtpClient objSmtp = new SmtpClient();
                MontarSmtp(objSmtp);
                objSmtp.Send(objEmail);
                return true;
            }
            catch (Exception ex)
            {
                new WebForLinkException("Erro ao tentar enviar email.", ex);
                return false;
            }
        }

    }

}
