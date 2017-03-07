using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Infrastructure
{
    public class ConsultaCNPJSimplesNacional
    {
        private string viewState, eventValidation, hiddenField1, servidorCaptcha, stringCookies, token = string.Empty, img = string.Empty;

        private const String urlBaseReceitaFederalCNPJ = "http://www8.receita.fazenda.gov.br/SimplesNacional/Aplicacoes/ATBHE/ConsultaOptantes.app/ConsultarOpcao.aspx";
        private const String paginaCaptchaCNPJ = "http://www8.receita.fazenda.gov.br/SimplesNacional/Aplicacoes/ATBHE/ConsultaOptantes.app/Captcha/Inicializa.ashx";

        /// <summary>
        /// Captura Captcha e também armazena valores de alguns parametros a serem enviados na Consulta
        /// </summary>
        /// <returns>Retorna BITMAP contendo imagem Captcha</returns>
        public Bitmap GetCaptchaCNPJ()
        {
            String htmlResult = string.Empty;
            Bitmap retorno = null;

            try
            {
                WebClient wc = new WebClient();

                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                //requisita valores das variaveis para armazenamento
                htmlResult = wc.DownloadString(urlBaseReceitaFederalCNPJ);

                if (htmlResult.Length > 0)
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlResult);


                    servidorCaptcha = doc.DocumentNode.Descendants("input")
                                    .First(i => i.Attributes["id"] != null &&
                                                i.Attributes["id"].Value == "hddServidorCaptcha")
                                    .Attributes["value"].Value;

                    viewState = doc.DocumentNode.Descendants("input")
                                    .First(i => i.Attributes["id"] != null &&
                                                i.Attributes["id"].Value == "__VIEWSTATE")
                                    .Attributes["value"].Value;

                    eventValidation = doc.DocumentNode.Descendants("input")
                                    .First(i => i.Attributes["id"] != null &&
                                                i.Attributes["id"].Value == "__EVENTVALIDATION")
                                    .Attributes["value"].Value;

                    hiddenField1 = doc.DocumentNode.Descendants("input")
                                    .First(i => i.Attributes["id"] != null &&
                                                i.Attributes["id"].Value == "ctl00_ContentPlaceHolderConteudo_HiddenField1")
                                    .Attributes["value"].Value;

                    //requisita Captcha armazenando TOKEN a ser utilizado na Consulta
                    htmlResult = wc.DownloadString(paginaCaptchaCNPJ);

                    if (htmlResult.Length > 0)
                    {
                        stringCookies = htmlResult;

                        string[] x = Regex.Split(htmlResult, "\",\"");

                        foreach (string s in x)
                        {
                            if (s.IndexOf("Token") >= 0)
                                token = new Regex(@"[^\d]").Replace(s, string.Empty);
                            else if (s.IndexOf("Dados") >= 0)
                                img = s.Substring(8);
                        }

                        if (img.Length > 0)
                        {
                            byte[] imageBytes = Convert.FromBase64String(img);
                            MemoryStream ms = new MemoryStream(imageBytes);

                            Image image = Image.FromStream(ms, true, true);

                            retorno = (Bitmap)image;
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return retorno;

        }

        /// <summary>
        /// Faz Consulta do CNPJ informado pelo Usuário, verificando se o mesmo é Optante pelo Simples Nacional
        /// </summary>
        /// <param name="aCNPJ">CNPJ sem traços</param>
        /// <param name="aCaptcha">Conteúdo Captcha informado</param>
        /// <returns>Retorna Dados Cadastrais CNPJ e Regime Tributário Optado</returns>
        public List<KeyValuePair<string, string>> ConsultaCNPJ(string aCNPJ, string aCaptcha)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers[HttpRequestHeader.Cookie] = "captcha_token=" + token;
                    wc.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    wc.Headers[HttpRequestHeader.AcceptLanguage] = "pt-BR,pt;q=0.8,en-US;q=0.6,en;q=0.4";
                    wc.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
                    wc.Headers[HttpRequestHeader.Host] = "www8.receita.fazenda.gov.br";
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.152 Safari/537.36";


                    byte[] response =
                    wc.UploadValues(urlBaseReceitaFederalCNPJ, new NameValueCollection()
                    {
                    { "__EVENTTARGET", "null" }, //conteudo estático, sempre nulo
                    { "__EVENTARGUMENT", "null" }, //conteudo estático, sempre nulo
                    { "__VIEWSTATE", viewState }, //conteudo armazenado durante consulta Captcha
                    { "__EVENTVALIDATION", eventValidation },//conteudo armazenado durante consulta Captcha
                    { string.Format("ctl00$ContentPlaceHolderConteudo${0}", hiddenField1), new Regex(@"[^\d]").Replace(aCNPJ, string.Empty) }, //identifica parametro CNPJ adicionando à propriedade NAME o conteudo dinâmico da varíavel armazenada durante captura Captcha
                    { "ctl00$ContentPlaceHolderConteudo$HiddenField1", hiddenField1 }, //conteudo armazenado durante consulta Captcha
                    { "ctl00$ContentPlaceHolderConteudo$hddServidorCaptcha", servidorCaptcha }, //conteudo armazenado durante consulta Captcha
                    { "ctl00$ContentPlaceHolderConteudo$txtTexto_captcha_serpro_gov_br", aCaptcha }, //Captcha digitado
                    { "ctl00$ContentPlaceHolderConteudo$btnConfirmar", "Consultar" } //conteúdo estático de consulta
                    });

                    string result = System.Text.Encoding.UTF8.GetString(response);

                    //trata e retorna valores encontrados
                    return TratarRetorno(result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Captura os dados de Retorno do Site da Receita Federal
        /// </summary>
        /// <param name="resultado">STRING contendo Retorno da Consulta Realizada na Receita Federal</param>
        /// <returns>Retorna lista contendo Dados Cadastrais e Situação do CNPJ na Receita Federal</returns>
        private static List<KeyValuePair<string, string>> TratarRetorno(string resultado)
        {
            List<KeyValuePair<string, string>> retorno = new List<KeyValuePair<string, string>>();

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(resultado);

                #region ... Validação CAPTCHA ERROR...
                var itemError = (HtmlAgilityPack.HtmlNodeCollection)doc.DocumentNode.SelectNodes("//div[@id='ctl00_ContentPlaceHolderConteudo_pnlConsulta']");

                if (itemError != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode node in itemError)
                    {
                        if (node.SelectNodes("//span[@id='ctl00_ContentPlaceHolderConteudo_lblErroCaptcha']") != null)
                            throw new Exception("Caracteres Captcha Inválidos.");
                    }
                }
                #endregion

                #region ...Dados Empresa...
                var dadosEmpresa = (HtmlAgilityPack.HtmlNodeCollection)doc.DocumentNode.SelectNodes("//div[@id='ctl00_ContentPlaceHolderConteudo_Panel1']");

                if (dadosEmpresa != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode node in dadosEmpresa)
                    {
                        foreach (HtmlAgilityPack.HtmlNode node2 in node.SelectNodes("//span[@id]"))
                        {
                            string attributeValue = node2.GetAttributeValue("id", "");
                            if (attributeValue == "ctl00_ContentPlaceHolderConteudo_lblCNPJ")
                            {
                                retorno.Add(new KeyValuePair<string, string>("CNPJ", node2.InnerText));
                            }
                            else if (attributeValue == "ctl00_ContentPlaceHolderConteudo_lblNomeEmpresa")
                            {
                                retorno.Add(new KeyValuePair<string, string>("EMPRESA", node2.InnerText));
                            }
                        }
                    }

                }
                #endregion

                #region ...Situação do CNPJ...
                var dadosRetorno = (HtmlAgilityPack.HtmlNodeCollection)doc.DocumentNode.SelectNodes("//div[@id='ctl00_ContentPlaceHolderConteudo_Panel2']");

                if (dadosRetorno != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode node in dadosRetorno)
                    {
                        foreach (HtmlAgilityPack.HtmlNode node2 in node.SelectNodes("//span[@id]"))
                        {
                            string attributeValue = node2.GetAttributeValue("id", "");
                            if (attributeValue == "ctl00_ContentPlaceHolderConteudo_lblSituacaoSimples")
                            {
                                retorno.Add(new KeyValuePair<string, string>("SITUAÇÃO", node2.InnerText));
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }
    }
    public class Geral : IGeral
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const string Key = "r10X310y";

        public string ValorKey()
        {
            return Key;
        }


        public void CriarAuthTicket(string dados, string roles)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                dados,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),  // validade 30 min tá bom demais
                false,   // Se você deixar true, o cookie ficará no PC do usuário
                roles);


            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public static object PegaAuthTicket(string propriedade)
        {
            try
            {
                EncryptDecryptQueryString cripto = new EncryptDecryptQueryString();
                string dados = cripto.Descriptografar(HttpContext.Current.User.Identity.Name, Key);
                byte[] byteArray = Encoding.UTF8.GetBytes(dados);
                MemoryStream st = new MemoryStream(byteArray);

                st.Position = 0;

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Autenticado));
                Autenticado aut = (Autenticado)ser.ReadObject(st);

                return aut.GetType().GetProperty(propriedade).GetValue(aut);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public void AuthenticateRequest()
        {
            if (HttpContext.Current.User == null) return;

            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            if (HttpContext.Current.User.Identity is FormsIdentity)
            {
                var id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                string userData = ticket.UserData;
                string[] roles = userData.Split(',');
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
            }
        }

        public string LogQueries(string message)
        {
            return message;
        }

        public string CriarAssinaturaEmail(AssinaturaEmailVM assinatura)
        {
            try
            {
                assinatura.CriarMensagem();
                return assinatura.Mensagem;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                Log.Error(ex.Message);
                return assinatura.Mensagem;
            }
        }
        public bool EnviarEmail(string email, string assunto, string mensagem)
        {
            try
            {
                // ENVIA O EMAIL
                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
                objEmail.To.Add(email);
                objEmail.Priority = MailPriority.Normal;
                objEmail.IsBodyHtml = true;
                objEmail.Subject = assunto;
                objEmail.Body = mensagem;
                //objEmail.Body += "Link: " + url;
                SmtpClient objSmtp = new SmtpClient();
                objSmtp.Host = ConfigurationManager.AppSettings["HostEmail"];
                objSmtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortEmail"]);
                objSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["CredentialEmail"]);
                objSmtp.Send(objEmail);

                return true;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                Log.Error(ex.Message);
                return false;
            }
        }

        public bool EnviarEmail(string email, string assunto, string mensagem, Stream arquivo, string nomeArquivo)
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
                    From = new MailAddress(ConfigurationManager.AppSettings["Email"]),
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
                    Host = ConfigurationManager.AppSettings["HostEmail"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortEmail"]),
                    Credentials =
                        new NetworkCredential(ConfigurationManager.AppSettings["Email"],
                            ConfigurationManager.AppSettings["CredentialEmail"])
                };
                objSmtp.Send(objEmail);

                return true;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                Log.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método estático para remover tag de um ponto inicial até seu ponto final
        /// EX: string x = "<script>javascript code</script>";
        /// .RemoveTag("x","<script", "</script>");
        /// Todo o conteúdo será removido.
        /// </summary>
        /// <param name="html">A string que contém caracter para ser removido </param>
        /// <param name="startTag">Início da remoção</param>
        /// <param name="endTag">Fim da remoção</param>
        /// <returns></returns>
        public string RemoveTag(String html, String startTag, String endTag)
        {
            Boolean bAgain;
            do
            {
                bAgain = false;
                Int32 startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
                if (startTagPos < 0)
                    continue;
                Int32 endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
                if (endTagPos <= startTagPos)
                    continue;
                html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
                bAgain = true;
            } while (bAgain);
            return html;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EncodeCodigoHtml(string param)
        {
            Encoding.ASCII.GetString(Encoding.Convert(
                Encoding.UTF8,
                Encoding.GetEncoding(
                    Encoding.ASCII.EncodingName,
                    new EncoderReplacementFallback(string.Empty),
                    new DecoderExceptionFallback()
                ),
                Encoding.UTF8.GetBytes(param)
            ));

            return param;
        }

        public string FormatarCnpjCpf(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return string.Empty;
            return documento.Length == 14
                    ? Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00")
                    : Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00");
        }

        public bool EnviarEmail(EmailWebForLink email)
        {
            MailMessage objEmail = new MailMessage();
            objEmail.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
            objEmail.To.Add(email.Para.FirstOrDefault());
            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = email.Assunto;
            objEmail.Body = email.Mensagem;

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = ConfigurationManager.AppSettings["HostEmail"];
            objSmtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PortEmail"]);
            objSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["CredentialEmail"]);
            objSmtp.Send(objEmail);
            return true;
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(
            this IEnumerable<T> source, params T[] tail)
        {
            return source.Concat(tail);
        }
    }

}