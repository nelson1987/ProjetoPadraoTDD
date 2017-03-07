using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using WebForDocs.Business.Infrastructure.Enum;
using WebForDocs.Business.Process;
using WebForDocs.Controllers;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Dominio.Models;
using WebForDocs.Exceptions;
using WebForDocs.ExtensaoControllers;
using WebForDocs.ViewModels.Carga;

namespace WebForDocs.Biblioteca
{
    public class ThreadRoboRetornoCarga : BaseController
    {
        static SolicitacaoBP solicitacaoBP = new SolicitacaoBP();

        public static void InicializarRobo()
        {
            WFLModel db = new WFLModel();

            solicitacaoBP.BuscarSolicitacaoAguardandoRetornoCarga()
                .ToList()
                .ForEach(x=>
                {
                    LerArquivos(db, x);
                });
        }

        private static void LerArquivos(WFLModel db, int ContratanteId)
        {
            try
            {
                var diretorioCarga = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioRetornoSap");
                if (!Directory.Exists(diretorioCarga))
                {
                    Directory.CreateDirectory(diretorioCarga);
                }

                ContratanteConfiguracaoBP contratanteConfig = new ContratanteConfiguracaoBP();
                var arquivoRetorno = contratanteConfig.BuscarPorID(ContratanteId).FORNECEDOR_RETORNO;
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

        private static SolicitacaoCarga deserializarArquivo(string localArquivo)
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
        private static void AtualizarCodigoERP(WFLModel db, List<MensagensCarga> lstRetorno)
        {
            try
            {
                lstRetorno.ForEach(
                    x =>
                    {
                        var solicitacao = db.WFD_SOLICITACAO
                            .Include("WFD_USUARIO")
                            .Include("WFL_FLUXO")
                            .FirstOrDefault(ws => ws.ID == x.SolicitacaoId);
                        bool mandaEmail = false;
                        switch (solicitacao.WFL_FLUXO.FLUXO_TP_ID)
                        {
                            case (int)EnumTiposFluxo.CadastroFornecedorNacional:
                            case (int)EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                                if (x.CodigoERP > 0)
                                {
                                    WFD_SOL_CAD_PJPF solCadastro = db.WFD_SOL_CAD_PJPF
                                        .FirstOrDefault(y => y.SOLICITACAO_ID == x.SolicitacaoId);

                                    solCadastro.COD_PJPF_ERP = x.CodigoERP.ToString();
                                    db.Entry(solCadastro).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                mandaEmail = true;
                                break;
                        }
                        AtualizarSolicitacao(db, solicitacao.ID, solicitacao.WFL_FLUXO.FLUXO_TP_ID, solicitacao.CONTRATANTE_ID, solicitacao.WFD_USUARIO.ID, solicitacao.WFD_USUARIO.EMAIL, mandaEmail);
                    }
                    );
            }
            catch (Exception)
            {
            }
        }

        private static void AtualizarSolicitacao(WFLModel Db, int Id, int Fluxo, int ContratanteId, int usuarioId, string emailUsuario, bool mandaEmail)
        {
            Aprovacao.FinalizaSolicitacao(Db, Fluxo, Id);
            Tramite.AtualizaTramite(ContratanteId, Id, Fluxo, (int)EnumPapeisWorkflow.RetornoCarga, 2, null);

            //WFD_USUARIO usuarioSolicitante = Db.WFD_USUARIO
            //    .FirstOrDefault(x => x.ID == solicitacao.USUARIO_ID);

            //if (usuarioSolicitante == null)
            //    throw new WebForLinkException(string.Format("A solicitação Nº{0} não tem solicitante cadastrado na base.", solicitacao.ID));

            //if (usuarioSolicitante.EMAIL == null)
            //    throw new WebForLinkException("O Solicitante não tem e-mail cadastrado na base");

            //Db.SaveChanges();
            if(mandaEmail)
                EnviarEmailFornecedorUsuario(emailUsuario, usuarioId);
        }

        private static void EnviarEmailFornecedorUsuario(string EmailSolicitante, int UsuarioId)
        {
            // CRIPTOGRAFA A URL QUE SERA ENVIADA AO USUÁRIO
            ServicosController serv = new ServicosController();
            string url = serv.UrlAction(UsuarioId);
            string assunto = "WebForLink - Cadastro de Usuário concluído com sucesso";
            string mensagem = "";
            mensagem += "<p style='text-align: center;'><h3><b>WebForDocs</b></h3><br />";
            mensagem += "<b>Incluir novo usuário</b></p>";
            mensagem += "<p style='text-align: left'>";
            mensagem += "Você está recebendo este e-mail porque sua solicitação de cadastro foi concluída.<br />";
            mensagem += "Para proceder com a inclusão da senha clique no link abaixo ou copie e cole em seu navegador<br /><br />";
            mensagem += "<a href='" + url + "'>Link</a> - " + url;
            mensagem += "</p><br /><br />";
            mensagem += "<p style='font-size: 10px;'>Este é um e-mail automático, favor não responder!</p>";

            _metodosGerais.EnviarEmail(EmailSolicitante, assunto, mensagem);
        }
    }
}