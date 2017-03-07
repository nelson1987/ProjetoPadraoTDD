using System;
using System.IO;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Interfaces
{
    public interface IGeral
    {
        string ValorKey();
        void CriarAuthTicket(string dados, string roles);
        //object PegaAuthTicket(string propriedade);
        void AuthenticateRequest();
        string LogQueries(string message);
        string CriarAssinaturaEmail(AssinaturaEmailVM assinatura);
        bool EnviarEmail(EmailWebForLink email);
        bool EnviarEmail(string email, string assunto, string mensagem);
        bool EnviarEmail(string email, string assunto, string mensagem, Stream arquivo, string nomeArquivo);
        string RemoveTag(String html, String startTag, String endTag);
        string EncodeCodigoHtml(string param);
        string FormatarCnpjCpf(string documento);
    }
}