
using System;
using System.Web.Mvc;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using WebForLink.Application.Interfaces;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Controllers
{
    /// <summary>
    /// Notificação Enviada pela pagseguro
    /// </summary>
    public class NotificacaoController : ControllerPadrao
    {
        private IAdesaoWebForLinkAppService _adesaoService { get; set; }
        public NotificacaoController(IAdesaoWebForLinkAppService adesao)
        {
            _adesaoService = adesao;
        }
        // GET: Notificacao
        [AllowAnonymous]
        [Route("NotificacaoPagamento")]
        [HttpPost]
        public JsonResult Notificar(string notificationCode, string notificationType)
        {
            try
            {
                if ("transaction".Equals(notificationType))
                {
                    Transaction transacao = _adesaoService.BuscarTransacaoPagSeguro(notificationCode);
                    if (transacao.TransactionStatus == TransactionStatus.Paid)
                    {
                        EmailWebForLink servicoEmail = new EmailWebForLinkBuilder()
                            .De("webforlink@chconsultoria.com.br")
                            .Para("nelson.ash@outlook.com")
                            .Assunto("Novo Fornecedor Individual")
                            .Mensagem(string.Format("Um novo fornecedor individual foi incluído no sistema, consultar no link NotificationCode: {0}/notificationType: {1}", notificationCode, notificationType))
                            .Constroi();
                        _metodosGerais.EnviarEmail(servicoEmail);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Mensagem = ex.Message });
            }

            return Json(new { Mensagem = "OK" });
        }
    }
}