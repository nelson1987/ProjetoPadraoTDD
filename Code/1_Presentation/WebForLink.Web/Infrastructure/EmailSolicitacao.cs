using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Interfaces;

namespace WebForLink.Web.Infrastructure
{
    /// <summary>
    /// Classe de envio de email após aprovar ou reprovar uma solicitação.
    /// </summary>
    public class EmailSolicitacao
    {
        WebForLinkContexto db = new WebForLinkContexto();


        #region Chamadas Para BP
        public IGeral _metodosGerais;
        #endregion

        public EmailSolicitacao()
        {
            _metodosGerais = new Geral();
        }
        public void EnviarEmailSolicitacao(int ContratanteId, int SolicitacaoId, int FluxoId, List<FLUXO_SEQUENCIA> proxPapeis)
        {
            if (proxPapeis != null && proxPapeis.Count > 0)
            {
                List<int?> lstPapeisUsuario = new List<int?>();

                foreach (var item in proxPapeis)
                {
                    if (item.PAPEL_ID_FIM != null)
                    {
                        lstPapeisUsuario.Add(item.PAPEL_ID_FIM);
                    }
                    else
                    {
                        EnviaEmailSolicitante(SolicitacaoId);
                    }
                }

                string[] usuariosEmail = db.WFD_USUARIO.Where(x => x.WFL_PAPEL.Any(y => lstPapeisUsuario.Contains(y.ID))).Select(z => z.EMAIL).ToArray();

                if (usuariosEmail != null && usuariosEmail.Count() > 0)
                {
                    var objSolicitacao = db.WFD_SOLICITACAO.Include("Usuario").Include("Fluxo").FirstOrDefault(x => x.ID == SolicitacaoId);

                    var emailCfg = db.WFD_CONTRATANTE_CONFIG_EMAIL.FirstOrDefault(x => x.CONTRATANTE_ID == ContratanteId && x.EMAIL_TP_ID == 2);
                    string assunto = emailCfg.ASSUNTO;
                    string mensagem = LayoutMensagemEmailAprovacao(emailCfg.CORPO, objSolicitacao);

                    foreach (var item in usuariosEmail)
                    {
                        bool envioEmail = _metodosGerais.EnviarEmail(item, assunto, mensagem);
                        if (!envioEmail)
                            throw new Exception();
                    }
                }
            }
        }

        private string LayoutMensagemEmailAprovacao(string mensagem, SOLICITACAO Solicitacao)
        {
            return mensagem.Replace("^NomeFLuxo^", Solicitacao.Fluxo.FLUXO_NM).Replace("^SolicitacaoId^", Solicitacao.ID.ToString()).Replace("^DataSolicitacao^", Solicitacao.SOLICITACAO_DT_CRIA.ToString("dd/MM/yyyy"));
        }

        public void EnviarEmailReprovacao(int SolicitacaoId)
        {
            EnviaEmailSolicitante(SolicitacaoId);
        }

        private void EnviaEmailSolicitante(int solicitacaoId)
        {
            SOLICITACAO objSolicitacao =
                    db.WFD_SOLICITACAO
                    .Include("WFD_USUARIO")
                    .Include("Fluxo")
                    .FirstOrDefault(x => x.ID == solicitacaoId);

            if (objSolicitacao != null)
            {
                if (objSolicitacao.Usuario != null)
                {
                    string assunto = string.Empty;
                    if (objSolicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Reprovado)
                        assunto = "Reprovação da solicitação";
                    if (objSolicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Concluido)
                        assunto = "Finalização da solicitação";

                    bool envioEmail = _metodosGerais.EnviarEmail(objSolicitacao.Usuario.EMAIL, assunto, LayoutMensagemEmailSolicitante(objSolicitacao));
                    if (!envioEmail)
                        throw new Exception();

                }
            }
        }

        private string LayoutMensagemEmailSolicitante(SOLICITACAO Solicitacao)
        {
            string mensagem = "";

            // Finalizado
            if (Solicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Concluido)
            {
                mensagem += "<p style='text-align: center;'><span style='font-size: 130%;'><b>" + ConfigurationManager.AppSettings["NomeSistema"] + "</b></span></p>";
                mensagem += "Sua solicitação de " + Solicitacao.Fluxo.FLUXO_NM + " de número " + Solicitacao.ID + " foi finalizada.<br>";
            }

            // Reprovado
            if (Solicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Reprovado)
            {
                mensagem += "<p style='text-align: center;'><span style='font-size: 130%;'><b>" + ConfigurationManager.AppSettings["NomeSistema"] + "</b></span></p>";
                mensagem += "A solicitação de " + Solicitacao.Fluxo.FLUXO_NM + " de número " + Solicitacao.ID + ", criado por " + Solicitacao.Usuario.NOME + " em " + Solicitacao.SOLICITACAO_DT_CRIA + " foi reprovada.<br>";
            }

            return mensagem;
        }
    }
}

