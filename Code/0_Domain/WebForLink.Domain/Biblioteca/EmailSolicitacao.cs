using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebForDocs.Business.Infrastructure.Enum;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Dominio.Models;
using WebForDocs.Interfaces;

namespace WebForDocs.Biblioteca
{
    /// <summary>
    /// Classe de envio de email após aprovar ou reprovar uma solicitação.
    /// </summary>
    public class EmailSolicitacao
    {
        WFLModel db = new WFLModel();


        #region Chamadas Para BP
        public IGeral _metodosGerais;
        #endregion

        public EmailSolicitacao()
        {
            _metodosGerais = new Geral();
        }
        public void EnviarEmailSolicitacao(int ContratanteId, int SolicitacaoId, int FluxoId, List<WFL_FLUXO_SEQUENCIA> proxPapeis)
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
                    var objSolicitacao = db.WFD_SOLICITACAO.Include("WFD_USUARIO").Include("WFL_FLUXO").SingleOrDefault(x => x.ID == SolicitacaoId);

                    var emailCfg = db.WFD_CONTRATANTE_CONFIG_EMAIL.SingleOrDefault(x => x.CONTRATANTE_ID == ContratanteId && x.EMAIL_TP_ID == 2);
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

        private string LayoutMensagemEmailAprovacao(string mensagem, WFD_SOLICITACAO Solicitacao)
        {
            return mensagem.Replace("^NomeFLuxo^", Solicitacao.WFL_FLUXO.FLUXO_NM).Replace("^SolicitacaoId^", Solicitacao.ID.ToString()).Replace("^DataSolicitacao^", Solicitacao.SOLICITACAO_DT_CRIA.ToString("dd/MM/yyyy"));
        }

        public void EnviarEmailReprovacao(int SolicitacaoId)
        {
            EnviaEmailSolicitante(SolicitacaoId);
        }

        private void EnviaEmailSolicitante(int solicitacaoId)
        {
            WFD_SOLICITACAO objSolicitacao =
                    db.WFD_SOLICITACAO
                    .Include("WFD_USUARIO")
                    .Include("WFL_FLUXO")
                    .SingleOrDefault(x => x.ID == solicitacaoId);

            if (objSolicitacao != null)
            {
                if (objSolicitacao.WFD_USUARIO != null)
                {
                    string assunto = string.Empty;
                    if (objSolicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Reprovado)
                        assunto = "Reprovação da solicitação";
                    if (objSolicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Concluido)
                        assunto = "Finalização da solicitação";

                    bool envioEmail = _metodosGerais.EnviarEmail(objSolicitacao.WFD_USUARIO.EMAIL, assunto, LayoutMensagemEmailSolicitante(objSolicitacao));
                    if (!envioEmail)
                        throw new Exception();

                }
            }
        }

        private string LayoutMensagemEmailSolicitante(WFD_SOLICITACAO Solicitacao)
        {
            string mensagem = "";

            // Finalizado
            if (Solicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Concluido)
            {
                mensagem += "<p style='text-align: center;'><span style='font-size: 130%;'><b>" + ConfigurationManager.AppSettings["NomeSistema"] + "</b></span></p>";
                mensagem += "Sua solicitação de " + Solicitacao.WFL_FLUXO.FLUXO_NM + " de número " + Solicitacao.ID + " foi finalizada.<br>";
            }

            // Reprovado
            if (Solicitacao.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Reprovado)
            {
                mensagem += "<p style='text-align: center;'><span style='font-size: 130%;'><b>" + ConfigurationManager.AppSettings["NomeSistema"] + "</b></span></p>";
                mensagem += "A solicitação de " + Solicitacao.WFL_FLUXO.FLUXO_NM + " de número " + Solicitacao.ID + ", criado por " + Solicitacao.WFD_USUARIO.NOME + " em " + Solicitacao.SOLICITACAO_DT_CRIA + " foi reprovada.<br>";
            }

            return mensagem;
        }
    }
}

