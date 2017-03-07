using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Exceptions;
using WebForLink.Web.ViewModels.Carga;

namespace WebForLink.Web.Infrastructure
{
    public class ThreadRoboRetornoCarga : ControllerPadrao
    {
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoBP;
        private readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfig;
        private readonly IAprovacaoWebForLinkAppService _aprovacaoBp;
        private readonly ITramiteWebForLinkAppService _tramite;

        public ThreadRoboRetornoCarga(ISolicitacaoWebForLinkAppService solicitacaoBP,
        IContratanteConfiguracaoWebForLinkAppService contratanteConfig,
        IAprovacaoWebForLinkAppService aprovacaoBp,
        ITramiteWebForLinkAppService tramite)
        {
            _solicitacaoBP = solicitacaoBP;
            _contratanteConfig = contratanteConfig;
            _aprovacaoBp = aprovacaoBp;
            _tramite = tramite;
        }

        public void InicializarRobo()
        {
            WebForLinkContexto db = new WebForLinkContexto();

            _solicitacaoBP.BuscarSolicitacaoAguardandoRetornoCarga()
                .ToList()
                .ForEach(x =>
                {
                    LerArquivos(db, x);
                });
        }

        private void LerArquivos(WebForLinkContexto db, int ContratanteId)
        {
            try
            {
                var diretorioCarga = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioRetornoSap");
                if (!Directory.Exists(diretorioCarga))
                {
                    Directory.CreateDirectory(diretorioCarga);
                }
                var arquivoRetorno = _contratanteConfig.BuscarPorID(ContratanteId).FORNECEDOR_RETORNO;
                var nome = string.Format("{0}\\{1}", diretorioCarga, arquivoRetorno);
                //List<string> solicitacoes = new List<string>();
                foreach (var arquivoCriacaoFornecedores in Directory.EnumerateFiles(diretorioCarga, arquivoRetorno))
                {
                    AtualizarCodigoERP(db, deserializarArquivo(arquivoCriacaoFornecedores).Mensagens);
                    //solicitacoes.Add(arquivoCriacaoFornecedores);
                }
            }
            catch (Exception ex)
            {
                throw new WebForLinkException("Erro ao tentar nomear o arquivo", ex);
            }
        }

        private SolicitacaoCarga deserializarArquivo(string localArquivo)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SolicitacaoCarga));
                FileStream fs = new FileStream(localArquivo, FileMode.Open);
                SolicitacaoCarga ret = (SolicitacaoCarga)serializer.Deserialize(fs);
                return ret;
            }
            catch (Exception ex)
            {
                throw new WebForLinkException("Erro ao tentar criar o arquivo", ex);
            }
        }

        /// <summary>
        /// Será atualizado o código de ERP se o mesmo não vier nulo apenas nos retorno de Cad.Fornecedor Nacional e Nacional Direto
        /// </summary>
        /// <param name="db">contexto do Banco de Dados</param>
        /// <param name="lstRetorno">Retorno do arquivo</param>
        private void AtualizarCodigoERP(WebForLinkContexto db, List<MensagensCarga> lstRetorno)
        {
            try
            {
                lstRetorno.ForEach(
                    x =>
                    {
                        var solicitacao = db.WFD_SOLICITACAO
                            .Include(y=>y.Usuario)
                            .Include(y => y.Fluxo)
                            .FirstOrDefault(ws => ws.ID == x.SolicitacaoId);
                        bool mandaEmail = false;
                        switch (solicitacao.Fluxo.FLUXO_TP_ID)
                        {
                            case (int)EnumTiposFluxo.CadastroFornecedorNacional:
                            case (int)EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                                if (x.CodigoERP > 0)
                                {
                                    SolicitacaoCadastroFornecedor solCadastro = db.WFD_SOL_CAD_PJPF
                                        .FirstOrDefault(y => y.SOLICITACAO_ID == x.SolicitacaoId);

                                    solCadastro.COD_PJPF_ERP = x.CodigoERP.ToString();
                                    db.Entry(solCadastro).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                mandaEmail = true;
                                break;
                        }
                        AtualizarSolicitacao(db, solicitacao.ID, solicitacao.Fluxo.FLUXO_TP_ID, solicitacao.CONTRATANTE_ID, solicitacao.Usuario.ID, solicitacao.Usuario.EMAIL, mandaEmail);
                    }
                    );
            }
            catch (Exception)
            {
            }
        }

        private void AtualizarSolicitacao(WebForLinkContexto Db, int Id, int Fluxo, int ContratanteId, int usuarioId, string emailUsuario, bool mandaEmail)
        {
            int? grupoId = (int?)Geral.PegaAuthTicket("Grupo");
            _aprovacaoBp.FinalizarSolicitacao(grupoId, Fluxo, Id);

            //Aprovacao.FinalizaSolicitacao(Fluxo, Id);
            _tramite.AtualizarTramite(ContratanteId, Id, Fluxo, (int)EnumPapeisWorkflow.RetornoCarga, 2, null);

            if (mandaEmail)
                EnviarEmailFornecedorUsuario(emailUsuario, usuarioId);
        }

        private void EnviarEmailFornecedorUsuario(string EmailSolicitante, int UsuarioId)
        {
            // CRIPTOGRAFA A URL QUE SERA ENVIADA AO USUÁRIO
            //ServicosController serv = new ServicosController();
            //string url = serv.UrlAction(UsuarioId);
            string assunto = "WebForLink - Cadastro de Usuário concluído com sucesso";
            string mensagem = "";
            mensagem += "<p style='text-align: center;'><h3><b>WebForLink</b></h3><br />";
            mensagem += "<b>Incluir novo usuário</b></p>";
            mensagem += "<p style='text-align: left'>";
            mensagem += "Você está recebendo este e-mail porque sua solicitação de cadastro foi concluída.<br />";
            mensagem += "Para proceder com a inclusão da senha clique no link abaixo ou copie e cole em seu navegador<br /><br />";
            //mensagem += "<a href='" + url + "'>Link</a> - " + url;
            mensagem += "</p><br /><br />";
            mensagem += "<p style='font-size: 10px;'>Este é um e-mail automático, favor não responder!</p>";

            _metodosGerais.EnviarEmail(EmailSolicitante, assunto, mensagem);
        }
    }
}