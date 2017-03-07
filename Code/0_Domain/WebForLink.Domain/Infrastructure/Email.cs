using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace WebForLink.Domain.Infrastructure
{
    public class EmailWebForLink
    {
        public EmailWebForLink(string para)
        {
            Para = new List<MailAddress> { new MailAddress(para) };
        }
        public EmailWebForLink()
        {
        }

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
            Para.ForEach(x => { objEmail.To.Add(x); });
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
                var objEmail = new MailMessage();
                MontarEmail(objEmail);
                if (Arquivo != null)
                    if (Arquivo.Length > 0)
                    {
                        var attachment = new Attachment(Arquivo, NomeArquivo);
                        objEmail.Attachments.Add(attachment);
                    }
                var objSmtp = new SmtpClient();
                MontarSmtp(objSmtp);
                objSmtp.Send(objEmail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void EsquecerSenha(string url)
        {
            Assunto = "WebForLink - Esqueci Minha Senha";
            var mensagem = "";
            mensagem += "<p style='text-align: center;'><h3><b>WebForLink</b></h3><br />";
            mensagem += "<b>Esqueci Minha Senha</b></p>";
            mensagem += "<p style='text-align: left'>";
            mensagem += "Você está recebendo este e-mail porque solicitou nova senha no Esqueci Minha Senha.<br />";
            mensagem +=
                "Para proceder com a troca da senha clique no link abaixo ou copie e cole em seu navegador<br /><br />";
            mensagem += "<a href='" + url + "'>Link</a> - " + url;
            mensagem += "</p><br /><br />";
            mensagem += "<p style='font-size: 10px;'>Este é um e-mail automático, favor não responder!</p>";
            Mensagem = mensagem;
        }
    }

    public class EmailWebForLinkBuilder
    {
        private EmailWebForLink _servicoEmail { get; set; }
        public EmailWebForLinkBuilder()
        {
            _servicoEmail = new EmailWebForLink();
        }
        public EmailWebForLinkBuilder De(string de)
        {
            _servicoEmail.De = de;
            return this;
        }
        public EmailWebForLinkBuilder Para(string de)
        {
            _servicoEmail.Para = new List<MailAddress> { new MailAddress(de) };
            return this;
        }
        public EmailWebForLinkBuilder Assunto(string de)
        {
            _servicoEmail.Assunto = de;
            return this;
        }
        public EmailWebForLinkBuilder Mensagem(string de)
        {
            _servicoEmail.Mensagem = de;
            return this;
        }
        public EmailWebForLink Constroi()
        {
            return _servicoEmail;
        }
    }
}