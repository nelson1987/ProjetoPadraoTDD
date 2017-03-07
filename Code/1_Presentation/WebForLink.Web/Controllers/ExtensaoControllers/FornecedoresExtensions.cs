using AutoMapper;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers.Extensoes
{
    public static class FornecedoresExtensions
    {
       
        public static void FornecedorRobo(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha,
            SOLICITACAO solicitacao)
        {
        }

        public static void FornecedorRobo(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha,
            Fornecedor fornecedor)
        {
        }

        public static void FornecedorRobo(this FornecedoresController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            //ficha.FornecedorRobo = new FornecedorRoboVM();
            string htmlRfCertificadoHtml = string.Empty;
            string htmlSintCertificadohtml = string.Empty;
            //if (solicitacao.FLUXO_ID == 4)
            //{
            if (solicitacao.SolicitacaoCadastroFornecedor != null && solicitacao.SolicitacaoCadastroFornecedor.Any())
            {
                if (solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO != null)
                {


                    if (!string.IsNullOrEmpty(solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.RF_CERTIFICADO_HTML))
                    {
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.RF_CERTIFICADO_HTML, "<head>", "</head>");
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<!--<OBJECT", "</OBJECT>-->");
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<title", "</title>");
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<head", "</head>");
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<body", ">");
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<form", "</form>");
                        htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "A RFB agradece", "sua página");
                    }

                    if (!string.IsNullOrEmpty(solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.SINT_CERTIFICADO_HTML))
                    {
                        htmlSintCertificadohtml = controller._metodosGerais.RemoveTag(solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.SINT_CERTIFICADO_HTML, "<title", "</title>");
                        htmlSintCertificadohtml = controller._metodosGerais.RemoveTag(htmlSintCertificadohtml, "<!DOCTYPE", ">");
                        htmlSintCertificadohtml = controller._metodosGerais.RemoveTag(htmlSintCertificadohtml, "<html", ">");
                    }

                    FornecedorRoboVM fornecedorRoboVM = new FornecedorRoboVM
                    {
                        ID = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.ID,
                        Rf_Certificado_Html = htmlRfCertificadoHtml
                            .Replace("<html>", "")
                            .Replace("</html>", "")
                            .Replace("</body>", ""),
                        Robo_Dt_Exec = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.ROBO_DT_EXEC,
                        Sint_Certificado_Html = htmlSintCertificadohtml
                            .Replace("<center>", "")
                            .Replace("</center>", ""),
                        Solicitacao_ID = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_ROBO.SOLICITACAO_ID
                    };

                    ficha.FornecedorRobo = fornecedorRoboVM;

                }
            }
            //}
        }

        public static void FornecedorRobo(this FornecedoresController controller, FichaCadastralWebForLinkVM ficha, Fornecedor fornecedor)
        {
            //ficha.FornecedorRobo = new FornecedorRoboVM();
            string htmlRfCertificadoHtml = string.Empty;
            string htmlSintCertificadohtml = string.Empty;
            if (fornecedor.ROBO != null)
            {
                if (fornecedor.ROBO.RF_CERTIFICADO_HTML != null)
                {
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(fornecedor.ROBO.RF_CERTIFICADO_HTML, "<head>", "</head>");
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<!--<OBJECT", "</OBJECT>-->");
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<title", "</title>");
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<head", "</head>");
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<body", ">");
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "<form", "</form>");
                    htmlRfCertificadoHtml = controller._metodosGerais.RemoveTag(htmlRfCertificadoHtml, "A RFB agradece", "sua página");
                }

                if (fornecedor.ROBO.SINT_CERTIFICADO_HTML != null)
                {
                    htmlSintCertificadohtml = controller._metodosGerais.RemoveTag(fornecedor.ROBO.SINT_CERTIFICADO_HTML, "<title", "</title>");
                    htmlSintCertificadohtml = controller._metodosGerais.RemoveTag(htmlSintCertificadohtml, "<!DOCTYPE", ">");
                    htmlSintCertificadohtml = controller._metodosGerais.RemoveTag(htmlSintCertificadohtml, "<html", ">");
                }

                ficha.FornecedorRobo.ID = fornecedor.ROBO.ID;
                ficha.FornecedorRobo.Rf_Certificado_Html = htmlRfCertificadoHtml
                        .Replace("<html>", "")
                        .Replace("</html>", "")
                        .Replace("</body>", "");
                ficha.FornecedorRobo.Robo_Dt_Exec = fornecedor.ROBO.ROBO_DT_EXEC;
                ficha.FornecedorRobo.Sint_Certificado_Html = htmlSintCertificadohtml
                        .Replace("<center>", "")
                        .Replace("</center>", "");
                ficha.FornecedorRobo.Solicitacao_ID = fornecedor.ROBO.SOLICITACAO_ID;

            }
        }

        public static SOLICITACAO_MENSAGEM PopularSolicitacaoMensagem(FornecedoresSolicitacaoDocumentosVM model, SOLICITACAO solicitacao)
        {
            return new SOLICITACAO_MENSAGEM()
            {
                ASSUNTO = model.Assunto,
                MENSAGEM = model.Mensagem,
                SOLICITACAO_ID = solicitacao.ID,
                DT_ENVIO = DateTime.Now
            };
        }
        public static void PopularListaSolicitacaoDocumento(string[] doc, string[] hdnObrigatorio, string[] hdnTipoAtualizacao, List<SolicitacaoDeDocumentos> listaSolicitacaoDocumentos, SOLICITACAO solicitacao, WFD_CONTRATANTE_PJPF contratantePJPF, int i, DocumentosDoFornecedor documentoExistente, DateTime dataExpiracaoDocumento)
        {
            int docId = 0;
            int.TryParse(doc[i], out docId);

            bool obrigatorio = hdnObrigatorio[i].ToUpper() == "TRUE" ? true : false;
            var strPeriodicidade = hdnTipoAtualizacao[i].Split('|');
            bool? exigeValidade = null;
            int? periodicidadeId = null;

            if (strPeriodicidade[0] == "2")
                exigeValidade = true;

            if (strPeriodicidade[0] == "3")
                periodicidadeId = int.Parse(strPeriodicidade[1]);

            listaSolicitacaoDocumentos.Add(new SolicitacaoDeDocumentos()
            {
                WFD_SOLICITACAO = solicitacao,
                PJPF_DOCUMENTO_ID = documentoExistente != null ? (int?)documentoExistente.ID : null, // Se não for um novo documento é passado o ID do documento existente para update
                DESCRICAO_DOCUMENTO_ID = int.Parse(doc[i]),
                OBRIGATORIO = obrigatorio,
                EXIGE_VALIDADE = exigeValidade,
                PERIODICIDADE_ID = periodicidadeId,
                DATA_VENCIMENTO = dataExpiracaoDocumento
            });
        }


        //--------------------------------//

        public static IList<SolicitacaoDeDocumentos> CriarSolicitacoesDocumentos(this FornecedoresController controller, int solicitacaoCriacaoID, ICollection<ListaDeDocumentosDeFornecedor> documentos)
        {
            return controller.CriarSolicitacoesDocumentos(solicitacaoCriacaoID, documentos, null);
        }
               
        public static IList<SolicitacaoDeDocumentos> CriarSolicitacoesDocumentos(this FornecedoresController controller, int solicitacaoCriacaoID, ICollection<ListaDeDocumentosDeFornecedor> documentos, SOLICITACAO_MENSAGEM solicitacaoMensagem)
        {
            var solicitacoesDocumentos = documentos.Select(x =>
                new SolicitacaoDeDocumentos
                {
                    SOLICITACAO_ID = solicitacaoCriacaoID,
                    DESCRICAO_DOCUMENTO_ID = x.DESCRICAO_DOCUMENTO_ID,
                    WFD_SOL_MENSAGEM = solicitacaoMensagem,
                    LISTA_DOCUMENTO_ID = x.ID
                }).ToList();

            controller._pjPfSolicitacaoDocumentosService.InserirSolicitacoes(solicitacoesDocumentos);

            return solicitacoesDocumentos;
        }
               
        public static bool FinalizarFichaCadastral(this FornecedoresController controller, FichaCadastralWebForLinkVM model)
        {
            var preenchimentoValido = false;

            #region Validar Dados do Questionario Dinâmico
            List<WFD_INFORM_COMPL> informacoesComplementar = new List<WFD_INFORM_COMPL>();
            if (model.Questionarios != null)
            {
                if (model.Questionarios.QuestionarioDinamicoList != null)
                {
                    foreach (var questionario in model.Questionarios.QuestionarioDinamicoList)
                    {
                        foreach (var aba in questionario.AbaList)
                        {
                            foreach (var pergunta in aba.PerguntaList)
                            {
                                #region Validar se os campos obrigatórios estão preenchidos
                                //if (pergunta.Obrigatorio)
                                //{
                                //    if (string.IsNullOrEmpty(pergunta.Resposta))
                                //    {
                                //        ModelState.AddModelError("QuestionarioDinamicoValidation", string.Format("Campo {0} obrigatório!", pergunta.Titulo));
                                //    }
                                //}
                                #endregion

                                //if (ModelState.IsValid)
                                //{
                                WFD_INFORM_COMPL infoCompleRespondida = new WFD_INFORM_COMPL()
                                {
                                    SOLICITACAO_ID = model.Solicitacao.ID,
                                    PERG_ID = pergunta.Id,
                                    RESPOSTA = pergunta.Resposta
                                };

                                switch (pergunta.TipoDado)
                                {
                                    case "RadioButton":
                                        {
                                            if (pergunta.DominioListId != 0)
                                            {
                                                infoCompleRespondida.RESPOSTA = pergunta.DominioListId.ToString();
                                            }
                                        }
                                        break;
                                    case "Checkbox":
                                        {
                                            string respostaIdentada = string.Empty;
                                            foreach (string resposta in pergunta.RespostaCheckbox)
                                            {
                                                if (!resposta.Equals("false"))
                                                {
                                                    respostaIdentada = string.Concat(respostaIdentada, "^", resposta);
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(respostaIdentada))
                                                infoCompleRespondida.RESPOSTA = respostaIdentada.Remove(0, 1);
                                        }
                                        break;
                                }
                                informacoesComplementar.Add(infoCompleRespondida);
                                //}
                            }
                        }
                    }
                }
            }
            #endregion

            FORNECEDOR_CATEGORIA categoria = controller._fornecedorCategoriaService.BuscarPorId((int)model.CategoriaId);

            if (model.TipoFornecedor != 1)
            {
                if (model.DadosEnderecos == null || !model.DadosEnderecos.Any())
                {
                    controller.ModelState.AddModelError("DadosEnderecoValidation", "Informar ao menos um Endereço!");
                }
                else if (model.DadosEnderecos.Any(x => x.TipoEnderecoId == 0 || String.IsNullOrEmpty(x.Endereco) || String.IsNullOrEmpty(x.Numero) || String.IsNullOrEmpty(x.CEP)))
                {
                    controller.ModelState.AddModelError("DadosEnderecoValidation", "Dados incompletos no Endereço!");
                }
            }

            if (!categoria.ISENTO_DADOSBANCARIOS && !model.ApenasSalvar)
            {
                if (!model.DadosBancarios.Any())
                {
                    controller.ModelState.AddModelError("DadosBancariosValidation", "Informar ao menos um Dado Bancário!");
                    model.DadosBancarios.Add(new DadosBancariosVM());
                }
            }
            else
            {
                //REMOVE AS CRITICAS DOS DADOS BANCARIOS CASO A CATEGORIA SEJA ISENTA
                while (controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Banco")).Value != null)
                    controller.ModelState.Remove(controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Banco")));

                while (controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Agencia")).Value != null)
                    controller.ModelState.Remove(controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Agencia")));

                while (controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrente")).Value != null)
                    controller.ModelState.Remove(controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrente")));

                while (controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrenteDigito")).Value != null)
                    controller.ModelState.Remove(controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrenteDigito")));
            }

            if (!categoria.ISENTO_CONTATOS && !model.ApenasSalvar)
            {
                if (model.DadosContatos == null || !model.DadosContatos.Any())
                {
                    controller.ModelState.AddModelError("DadosContatosValidation", "Informar os Dados do Contato!");
                    model.DadosContatos = new List<DadosContatoVM>();
                    model.DadosContatos.Add(new DadosContatoVM());
                }
            }
            else
            {
                //REMOVE AS CRITICAS DOS DADOS DE CONTATOS CASO A CATEGORIA SEJA ISENTA
                while (controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosContatos") && ms.Key.Contains("EmailContato")).Value != null)
                    controller.ModelState.Remove(controller.ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosContatos") && ms.Key.Contains("EmailContato")));
            }

            if (!categoria.ISENTO_DOCUMENTOS && !model.ApenasSalvar && model.TipoFornecedor != 2)
            {
                List<SolicitacaoDocumentosVM> docsObrigatorios = model.SolicitacaoFornecedor.Documentos.Where(x => x.Obrigatorio == true && x.ArquivoSubido == null).ToList();
                if (docsObrigatorios.Any())
                {
                    controller.ModelState.AddModelError("AnexosValidation", "Favor subir os arquivos dos documentos Exigíveis!");
                }
            }

            controller.ModelState.Remove("SolicitacaoFornecedor.Assunto");
            controller.ModelState.Remove("SolicitacaoFornecedor.DescricaoSolicitacao");
            controller.ModelState.Remove("SolicitacaoFornecedor.Mensagem");
            controller.ModelState.Remove("SolicitacaoFornecedor.DadosEnderecos.T_UF.UF_NM");

            int tipoPapel = model.TipoPreenchimento.Equals((int)EnumTiposPreenchimento.Fornecedor) ? (int)EnumTiposPapel.Fornecedor : (int)EnumTiposPapel.Solicitante;
            int papelAtual = controller._papelBP.BuscarPorContratanteETipoPapel(model.ContratanteID, tipoPapel).ID;
            foreach (var modeloErrado in controller.ModelState.Values)
            {
                if (modeloErrado.Errors.Count != 0)
                {

                }
            }

            if (controller.ModelState.IsValid)
            {
                try
                {
                    SolicitacaoCadastroFornecedor solicitacaoCadastroPJPF = controller._solicitacaoCadastroFornecedorService.BuscarPorSolicitacaoId(model.Solicitacao.ID);
                    controller.CompletarSolicitacaoCadastroPJPF(ref solicitacaoCadastroPJPF, model);
                    controller._solicitacaoCadastroFornecedorService.AtualizarSolicitacao(solicitacaoCadastroPJPF);

                    controller.ManterDadosBancarios(model.DadosBancarios.Where(w => w.Banco != null && w.Agencia != null && w.ContaCorrente != null).ToList(), model.Solicitacao.ID, model.ContratanteID);
                    controller.ManterDadosContatos(model.DadosContatos.Where(w => w.EmailContato != null).ToList(), solicitacaoCadastroPJPF.SOLICITACAO_ID);
                    controller.ManterDadosEnderecos(model.DadosEnderecos.Where(x => x.TipoEnderecoId > 0).ToList(), solicitacaoCadastroPJPF.SOLICITACAO_ID);

                    controller.ManterUnspsc(model.FornecedoresUnspsc.ToList(), model.Solicitacao.ID);

                    if (model.TipoFornecedor != (int)EnumTiposFornecedor.EmpresaEstrangeira)
                        controller.ManterDocumentos(model.SolicitacaoFornecedor.Documentos, model.Solicitacao.ID, model.ContratanteID);

                    controller._informacaoComplementarBP.InsertAll(informacoesComplementar);


                    model.ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
                    if (solicitacaoCadastroPJPF.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.Count > 0)
                    {
                        //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                        model.ProrrogacaoPrazo = Mapper.Map<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>(solicitacaoCadastroPJPF.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.OrderBy(o => o.ID).LastOrDefault());
                    }
                    model.ProrrogacaoPrazo.PrazoPreenchimento = controller._contratanteConfiguracaoService.BuscarPrazo(solicitacaoCadastroPJPF.WFD_SOLICITACAO);
                    if (model.ProrrogacaoPrazo.Aprovado != null)
                    {
                        if ((bool)model.ProrrogacaoPrazo.Aprovado)
                            model.ProrrogacaoPrazo.Status = "Aprovado";
                        else
                            model.ProrrogacaoPrazo.Status = "Reprovado";
                    }
                    else
                    {
                        model.ProrrogacaoPrazo.Status = "Aguardando Aprovação...";
                    }


                    if (!model.ApenasSalvar)
                    {
                        controller._tramite.AtualizarTramite(model.ContratanteID, model.Solicitacao.ID, model.Solicitacao.Fluxo.ID, papelAtual, (int)EnumStatusTramite.Aprovado, null);

                        controller.ViewBag.MensagemSucesso = "Dados Enviados com Sucesso!";
                        controller.ViewBag.StatusTramite = (int)EnumStatusTramite.Aprovado;
                    }
                    else
                    {
                        controller.ViewBag.MensagemSucesso = "Dados Salvos com Sucesso!";
                        controller.ViewBag.StatusTramite = (int)EnumStatusTramite.Aguardando;
                    }
                    preenchimentoValido = true;
                }
                catch (Exception e)
                {
                    controller.ViewBag.MensagemErro = "Erro ao tentar Salvar a ficha cadastral!";
                    controller.ViewBag.StatusTramite = (int)EnumStatusTramite.Aguardando;
                    controller.Log.Error(e);
                }
            }
            else
            {
                controller.ViewBag.MensagemErro = "Não foi possível enviar a Ficha Cadastral! Existem dados incompletos abaixo.";
                controller.ViewBag.StatusTramite = (int)EnumStatusTramite.Aguardando;
            }

            model.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
            Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                controller._cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                {
                    //PapelId = papelAtual,
                    UF = "RJ",
                    ContratanteId = model.ContratanteID,
                    PapelId = papelAtual,
                    CategoriaId = categoria.ID,
                    Alteracao = true,
                    SolicitacaoId = model.Solicitacao.ID
                })
                    )
            };
            
            controller.PersistirDadosEnderecoEmMemoria();

            return preenchimentoValido;
        }
               
        public static List<SelectListItem> MontarDescricaoDocumento(this FornecedoresController controller, int? tipoDocumento, string tipo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            tipoDocumento = tipoDocumento ?? 0;

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<DescricaoDeDocumentos> descricaoDocumentos =

                controller.Db.WFD_DESCRICAO_DOCUMENTOS.Where(e => e.ATIVO 
                && e.CONTRATANTE_ID == contratanteId 
                && e.TIPO_DOCUMENTOS_ID == tipoDocumento)
                .OrderBy(e => e.DESCRICAO).ToList();

            listDocumentos.Add(tipo == "Lst"
                ? new SelectListItem() { Text = "Todos", Value = "" }
                : new SelectListItem() { Text = "Selecione...", Value = "" });

            listDocumentos.AddRange(
                descricaoDocumentos.Select(dd => new SelectListItem()
                {
                    Text = dd.DESCRICAO,
                    Value = dd.ID.ToString()
                }));

            return listDocumentos;
        }
               
        public static List<SelectListItem> MontarDescricaoDocumentoSemContratante(this FornecedoresController controller, int? tipoDocumento, string tipo)
        {
            tipoDocumento = tipoDocumento ?? 0;

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<DescricaoDeDocumentos> descricaoDocumentos =

                controller.Db.WFD_DESCRICAO_DOCUMENTOS.Where(e => e.ATIVO && e.TIPO_DOCUMENTOS_ID == tipoDocumento).OrderBy(e => e.DESCRICAO).ToList();

            listDocumentos.Add(tipo == "Lst"
                ? new SelectListItem() { Text = "Todos", Value = "" }
                : new SelectListItem() { Text = "Selecione...", Value = "" });

            listDocumentos.AddRange(
                descricaoDocumentos.Select(dd => new SelectListItem()
                {
                    Text = dd.DESCRICAO,
                    Value = dd.ID.ToString()
                }));

            return listDocumentos;
        }
               
        public static List<SelectListItem> MontarTipoDocumento(this FornecedoresController controller, string tipo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<TipoDeDocumento> tipoDocumentos = controller._tipoDocumentosBP.ListarPorIdContratante(contratanteId);

            listDocumentos.Add(tipo == "Lst"
                ? new SelectListItem() { Text = "Todos", Value = "" }
                : new SelectListItem() { Text = "Selecione...", Value = "" });

            listDocumentos.AddRange(
                tipoDocumentos.Select(td => new SelectListItem()
                {
                    Text = td.DESCRICAO,
                    Value = td.ID.ToString()
                }));

            return listDocumentos;
        }
               
        public static SolicitacaoFornecedorVM PopularSolicitacaoCadastroPJPF(this FornecedoresController controller, int contratanteId, SolicitacaoFornecedorVM modelo)
        {
            var usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            var solicitacao = controller._solicitacaoService.BuscarPorIdComDocumentos((int)modelo.SolicitacaoCriacaoID);
            var solforn = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();
            var configEmail = controller._contratanteConfiguracaoEmailBP.BuscarPorContratanteETipo(contratanteId, 1);
            var usuario = controller.Db.WFD_USUARIO.Single(x => x.ID == usuarioId);
            var contratante = controller.Db.Contratante.Single(x => x.ID == contratanteId);

            modelo.Fornecedor.ID = solforn.ID;
            modelo.Fornecedor.NomeFornecedor = solforn.PJPF_TIPO == 1 ? solforn.RAZAO_SOCIAL : solforn.NOME;
            modelo.Fornecedor.CNPJ = solforn.PJPF_TIPO == 1 ? solforn.CNPJ : solforn.CPF;
            modelo.Documentos = solicitacao.SolicitacaoDeDocumentos.Select(x => new SolicitacaoDocumentosVM
            {
                ID = x.DESCRICAO_DOCUMENTO_ID,
                ListaDocumentosID = x.LISTA_DOCUMENTO_ID,
                DescricaoDocumentoId = x.DESCRICAO_DOCUMENTO_ID,
                GrupoDocumento = x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO,
                Documento = x.DescricaoDeDocumentos.DESCRICAO,
                PorValidade = x.LISTA_DOCUMENTO_ID != null ? x.ListaDeDocumentosDeFornecedor.EXIGE_VALIDADE : false
            }).ToList();
            modelo.Assunto = configEmail.ASSUNTO;
            modelo.Mensagem = configEmail.CORPO
                .Replace("^NomeEmpresa^", contratante.NOME_FANTASIA ?? contratante.RAZAO_SOCIAL)
                .Replace("^NomeUsuario^", usuario.NOME)
                .Replace("^FixoUsuario1^", usuario.FIXO)
                .Replace("^CelularUsuario1^", usuario.CELULAR)
                .Replace("^EmailUsuario^", usuario.EMAIL);
            modelo.PassoAtual = 3;

            return modelo;
        }

        public static void PreencherFichaCadastral(this FornecedoresController controller, SOLICITACAO solicitacao, FichaCadastralWebForLinkVM ficha, int TpPapel)
        {
            Contratante contratante = solicitacao.Contratante;
            SolicitacaoCadastroFornecedor solicitacaoCadastroPJPF = solicitacao.SolicitacaoCadastroFornecedor.First();
            SolicitacaoFornecedorVM solicitacaoFornecedorVM = new SolicitacaoFornecedorVM();

            ficha.TipoFornecedor = solicitacaoCadastroPJPF.PJPF_TIPO;
            ficha.ContratanteID = contratante.ID;
            ficha.CategoriaId = solicitacaoCadastroPJPF.CATEGORIA_ID;
            ficha.ContratanteFornecedorID = solicitacao.CONTRATANTE_ID;
            ficha.SolicitacaoID = solicitacao.ID;

            ficha.Categoria = new CategoriaFichaVM
            {
                Id = solicitacaoCadastroPJPF.CATEGORIA_ID,
                Nome = solicitacaoCadastroPJPF.WFD_PJPF_CATEGORIA.CODIGO,
            };

            ficha.Solicitacao = new SolicitacaoVM
            {
                ID = solicitacao.ID,
                Fluxo = new FluxoVM
                {
                    ID = solicitacao.FLUXO_ID
                }
            };

            switch ((EnumTiposFornecedor)solicitacaoCadastroPJPF.PJPF_TIPO)
            {
                case EnumTiposFornecedor.EmpresaNacional:
                    ficha.CNPJ_CPF = Convert.ToUInt64(solicitacaoCadastroPJPF.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                    ficha.RazaoSocial = solicitacaoCadastroPJPF.RAZAO_SOCIAL;
                    break;
                case EnumTiposFornecedor.EmpresaEstrangeira:
                    ficha.RazaoSocial = solicitacaoCadastroPJPF.RAZAO_SOCIAL;
                    break;
                case EnumTiposFornecedor.PessoaFisica:
                    ficha.CNPJ_CPF = Convert.ToUInt64(solicitacaoCadastroPJPF.CPF).ToString(@"000\.000\.000\-00");
                    ficha.RazaoSocial = solicitacaoCadastroPJPF.NOME;
                    break;
            }

            ficha.NomeFantasia = solicitacaoCadastroPJPF.NOME_FANTASIA;
            //ficha.CNAE = solicitacaoCadastroPJPF.CNAE;
            ficha.InscricaoEstadual = solicitacaoCadastroPJPF.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = solicitacaoCadastroPJPF.INSCR_MUNICIPAL;
            ficha.TipoLogradouro = solicitacaoCadastroPJPF.TP_LOGRADOURO;
            ficha.Endereco = solicitacaoCadastroPJPF.ENDERECO;
            ficha.Numero = solicitacaoCadastroPJPF.NUMERO;
            ficha.Complemento = solicitacaoCadastroPJPF.COMPLEMENTO;
            ficha.Cep = solicitacaoCadastroPJPF.CEP;
            ficha.Bairro = solicitacaoCadastroPJPF.BAIRRO;
            ficha.Cidade = solicitacaoCadastroPJPF.CIDADE;
            ficha.Estado = solicitacaoCadastroPJPF.UF;
            ficha.Pais = solicitacaoCadastroPJPF.PAIS;
            ficha.Observacao = solicitacaoCadastroPJPF.OBSERVACAO;

            //Mapear Dados Bancários
            var solicitacoesModBanco = solicitacao.SolicitacaoModificacaoDadosBancario.ToList();

            ficha.DadosBancarios = solicitacoesModBanco.Any()
                ? Mapper.Map<List<SolicitacaoModificacaoDadosBancario>, List<DadosBancariosVM>>(solicitacoesModBanco)
                : new List<DadosBancariosVM>();

            //Mapear Dados de Endereço
            var solicitacoesModEndereco = solicitacao.WFD_SOL_MOD_ENDERECO.ToList();

            if (solicitacaoCadastroPJPF.PJPF_TIPO == 1)
            {
                ficha.DadosEnderecos = solicitacoesModEndereco.Any()
                    ? Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacoesModEndereco)
                    : new List<DadosEnderecosVM>();
            }
            else
            {
                ficha.DadosEnderecos = solicitacoesModEndereco.Any()
                    ? Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacoesModEndereco)
                    : new List<DadosEnderecosVM> { new DadosEnderecosVM { } };
            }

            //Mapear Dados Contatos
            var solicitacoesModContato = solicitacao.SolicitacaoModificacaoDadosContato.ToList();

            ficha.DadosContatos = solicitacoesModContato.Any()
                ? Mapper.Map<List<SolicitacaoModificacaoDadosContato>, List<DadosContatoVM>>(solicitacoesModContato)
                : new List<DadosContatoVM> { new DadosContatoVM() };

            if (solicitacao.WFD_SOL_MENSAGEM.Any())
            {
                solicitacaoFornecedorVM.Assunto = solicitacao.WFD_SOL_MENSAGEM.First().ASSUNTO;
                solicitacaoFornecedorVM.Mensagem = solicitacao.WFD_SOL_MENSAGEM.First().MENSAGEM;
            }

            solicitacaoFornecedorVM.Fornecedores = new List<SolicitacaoFornecedoresVM>();
            solicitacaoFornecedorVM.SolicitacaoCriacaoID = solicitacao.ID;

            solicitacaoFornecedorVM.Fornecedores = solicitacao.SolicitacaoCadastroFornecedor.Select(x => new SolicitacaoFornecedoresVM
            {
                NomeFornecedor = x.RAZAO_SOCIAL,
                CNPJ = x.CNPJ
            }).ToList();

            ficha.SolicitacaoFornecedor = solicitacaoFornecedorVM;

            //Mapear os Documentos
            solicitacaoFornecedorVM.Documentos =
                Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacao.SolicitacaoDeDocumentos.ToList());

            //Mapear UNSPSC
            ficha.FornecedoresUnspsc =
                Mapper.Map<List<SOLICITACAO_UNSPSC>, List<FornecedorUnspscVM>>(solicitacao.WFD_SOL_UNSPSC.ToList());

            var papel = controller._papelBP.BuscarPorContratanteETipoPapel(contratante.ID, TpPapel).ID;

            //Mapear Questionários
            ficha.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                    controller._cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                    {
                        //PapelId = papelAtual,
                        UF = "RJ",
                        ContratanteId = contratante.ID,
                        PapelId = papel,
                        CategoriaId = solicitacaoCadastroPJPF.CATEGORIA_ID,
                        Alteracao = true,
                        SolicitacaoId = solicitacao.ID
                    })
                    )
            };

            ficha.ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
            if (solicitacao.WFD_SOLICITACAO_PRORROGACAO.Count > 0)
            {
                //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                ficha.ProrrogacaoPrazo =
                    Mapper.Map<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>(solicitacao.WFD_SOLICITACAO_PRORROGACAO.OrderBy(o => o.ID).LastOrDefault());
            }
            ficha.ProrrogacaoPrazo.PrazoPreenchimento = controller._contratanteConfiguracaoService.BuscarPrazo(solicitacao);
            if (ficha.ProrrogacaoPrazo.Aprovado != null)
            {
                if ((bool)ficha.ProrrogacaoPrazo.Aprovado)
                    ficha.ProrrogacaoPrazo.Status = "Aprovado";
                else
                    ficha.ProrrogacaoPrazo.Status = "Reprovado";
            }
            else
                ficha.ProrrogacaoPrazo.Status = "Aguardando Aprovação...";
        }

        public static void CriarEntidadePartialDadosCadastro(this FornecedoresController controller, int fornecedorID, Fornecedor fornecedor, Contratante contratante, FichaCadastralWebForLinkVM ficha)
        {
            ficha.ID = fornecedorID;
            ficha.ContratanteID = contratante.ID;
            ficha.NomeEmpresa = contratante.RAZAO_SOCIAL;
            ficha.CNPJ_CPF = fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;
            ficha.NomeFantasia = fornecedor.NOME_FANTASIA;
            //ficha.CNAE = fornecedor.CNAE;
            ficha.InscricaoEstadual = fornecedor.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL;
            ficha.FornecedorRobo = new FornecedorRoboVM();

            if (fornecedor.ROBO != null)
            {
                ficha.FornecedorRobo.SimplesNacionalSituacao = fornecedor.ROBO.SIMPLES_NACIONAL_SITUACAO == null ? "" : fornecedor.ROBO.SIMPLES_NACIONAL_SITUACAO;
            }
        }
        
        public static void EnviarEmailContratantes(this FornecedoresController controller, FornecedoresSolicitacaoDocumentosVM model, int contratanteId, Fornecedor item)
        {
            item.WFD_CONTRATANTE_PJPF
                .FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId)
                .WFD_PJPF_CONTATOS
                .ToList()
                .ForEach(x =>
                {
                    controller._metodosGerais.EnviarEmail(x.EMAIL, model.Assunto, model.Mensagem);
                });
        }
        
        public static DateTime CalculaDataVencimento(this FornecedoresController controller, int periodoId)
        {
            DateTime vencimento = DateTime.MinValue;
            switch (periodoId)
            {
                case 1:
                    vencimento = DateTime.Now.AddDays(1);
                    break;
                case 2:
                    vencimento = DateTime.Now.AddDays(15);
                    break;
                case 3:
                    vencimento = DateTime.Now.AddMonths(1);
                    break;
                case 4:
                    vencimento = DateTime.Now.AddMonths(2);
                    break;
                case 5:
                    vencimento = DateTime.Now.AddMonths(3);
                    break;
                case 6:
                    vencimento = DateTime.Now.AddMonths(6);
                    break;
                case 7:
                    vencimento = DateTime.Now.AddYears(1);
                    break;

            }

            return vencimento;
        }
        
        public static void CompletarSolicitacaoCadastroPJPF(this FornecedoresController controller, ref SolicitacaoCadastroFornecedor cadPJPF, FichaCadastralWebForLinkVM ficha)
        {

            if (ficha.TipoFornecedor != 1)
            {
                cadPJPF.NOME = ficha.RazaoSocial;
                cadPJPF.NOME_FANTASIA = ficha.NomeFantasia;
                //cadPJPF.CNAE = ficha.CNAE;

                //Transformar em Enum
                if (ficha.TipoFornecedor == 3)
                {
                    cadPJPF.CPF = ficha.CNPJ_CPF.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");
                    cadPJPF.INSCR_ESTADUAL = ficha.InscricaoEstadual;
                }

                cadPJPF.TP_LOGRADOURO = ficha.TipoLogradouro;
                cadPJPF.ENDERECO = ficha.Endereco;
                cadPJPF.NUMERO = ficha.Numero;
                cadPJPF.COMPLEMENTO = ficha.Complemento;
                cadPJPF.CEP = ficha.Cep;
                cadPJPF.BAIRRO = ficha.Bairro;
                cadPJPF.CIDADE = ficha.Cidade;
                cadPJPF.UF = ficha.Estado;
                cadPJPF.PAIS = ficha.Pais;
            }

            //Transformar em Enum
            if (ficha.TipoFornecedor != 2)
                cadPJPF.INSCR_MUNICIPAL = ficha.InscricaoMunicipal;

            cadPJPF.OBSERVACAO = ficha.Observacao;
        }
        
        public static SOLICITACAO CriarSolicitacao(this FornecedoresController controller, FichaCadastralWebForLinkVM model, int tipoFluxoId)
        {
            SOLICITACAO solicitacao = new SOLICITACAO();

            controller.PopularSolicitacaoEmAprovacao(model.ContratanteID,
                model.ID,
                (int)Geral.PegaAuthTicket("UsuarioId"),
                controller._fluxoBP.BuscarPorTipoEContratante(tipoFluxoId, model.ContratanteID).ID,
                solicitacao);

            return controller._solicitacaoService.InserirSolicitacao(solicitacao);
        }
        
        public static SOLICITACAO CriarSolicitacaoDocumentos(this FornecedoresController controller, FichaCadastralWebForLinkVM model, int tipoFluxoId)
        {
            var solicitacao = new SOLICITACAO
            {
                CONTRATANTE_ID = model.ContratanteID,
                FLUXO_ID = controller._fluxoBP.BuscarPorTipoEContratante(tipoFluxoId, model.ContratanteID).ID,
                USUARIO_ID = (int)Geral.PegaAuthTicket("UsuarioId"),
                SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.Aguardando,
                PJPF_ID = model.PJPFID,
            };

            List<SolicitacaoDeDocumentos> documentosList = new List<SolicitacaoDeDocumentos>();
            model.SolicitacaoFornecedor.Documentos
                .ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ArquivoSubido))
                    {
                        var arquivoId = controller._fornecedorArquivoService.GravarArquivoSolicitacao(model.ContratanteID, x.ArquivoSubido, x.TipoArquivoSubido);

                        SolicitacaoDeDocumentos solicitacaoDeDocumentos = new SolicitacaoDeDocumentos()
                        {
                            DATA_UPLOAD = DateTime.Now,
                            NOME_ARQUIVO = controller._fornecedorArquivoService.PegaNomeArquivoSubido(x.ArquivoSubido),
                            EXTENSAO_ARQUIVO = x.TipoArquivoSubido,
                            ARQUIVO_ID = arquivoId,
                            DATA_VENCIMENTO = x.PorValidade != null ? ((bool)x.PorValidade ? x.DataValidade : null) : null,
                            DESCRICAO_DOCUMENTO_ID = x.DescricaoDocumentoId,
                            LISTA_DOCUMENTO_ID = x.ListaDocumentosID
                        };
                        documentosList.Add(solicitacaoDeDocumentos);
                    }
                });

            var soldocumentos =controller._solicitacaoService.InserirSolicitacaoDocumentos(solicitacao, documentosList);

            var papelAtual =controller._papelBP.BuscarPorContratanteETipoPapel(model.ContratanteID, (int)EnumTiposPapel.Solicitante).ID;

            controller._tramite.AtualizarTramite(model.ContratanteID, soldocumentos.ID, soldocumentos.FLUXO_ID, papelAtual, 2, soldocumentos.USUARIO_ID);
            return soldocumentos;

        }
        
        public static void EnviarSolicitacaoFornecedor(this FornecedoresController controller, int contratanteId, FornecedoresVM model, int? solicitacaoId)
        {
            FORNECEDOR_CATEGORIA categoria =controller._fornecedorCategoriaService.BuscarEmSolicitacaoFornecedor(contratanteId, model.Categoria);

            SolicitacaoFornecedorVM solicitacaoFornecedor = new SolicitacaoFornecedorVM
            {
                PassoAtual = 3,
                Fornecedores = new List<SolicitacaoFornecedoresVM>()
                {
                    new SolicitacaoFornecedoresVM
                    {
                        ID = model.ID,
                        NomeFornecedor = model.RazaoSocial,
                        CNPJ = model.CNPJ,
                        Emails = new List<SolicitacaoFornecedoresContatosVM>
                        {
                            new SolicitacaoFornecedoresContatosVM{
                                Nome = model.NomeContato, Email = model.Email
                            }
                        }
                    }
                },
                Documentos = categoria.ListaDeDocumentosDeFornecedor.Select(d => new SolicitacaoDocumentosVM
                {
                    ID = d.DESCRICAO_DOCUMENTO_ID,
                    ListaDocumentosID = d.ID,
                    Documento = d.DescricaoDeDocumentos.DESCRICAO,
                    PorValidade = d.EXIGE_VALIDADE
                }).ToList()
            };

            if (solicitacaoId != null)
                solicitacaoFornecedor.SolicitacaoCriacaoID = (int)solicitacaoId;

            controller.Session["Solicitacao"] = solicitacaoFornecedor;
        }
        
        public static void IncluirSolicitacaoDocumentoEnviarEmailContratantes(this FornecedoresController controller, FornecedoresSolicitacaoDocumentosVM model, string[] doc, string[] hdnObrigatorio, string[] hdnTipoAtualizacao, int contratanteId, int usuarioId, DateTime dtPrazo, int fluxoId, int papelAtual, Fornecedor item)
        {
            SOLICITACAO solicitacao = new SOLICITACAO()
            {
                CONTRATANTE_ID = contratanteId,
                DT_PRAZO = dtPrazo,
                FLUXO_ID = fluxoId,
                PJPF_ID = item.ID,
                SOLICITACAO_STATUS_ID = 5,
                SOLICITACAO_DT_CRIA = DateTime.Now,
                USUARIO_ID = usuarioId,
            };


           controller.Db.Entry(solicitacao).State = EntityState.Added;

            WFD_CONTRATANTE_PJPF contratantePJPF =controller.Db.WFD_CONTRATANTE_PJPF.Single(x =>
                x.PJPF_ID == item.ID &&
                x.CONTRATANTE_ID == contratanteId);

            List<SolicitacaoDeDocumentos> listaSolicitacoesDeDocumentos = new List<SolicitacaoDeDocumentos>();

            for (int i = 0; i < doc.Length; i++)
            {
                int docId = 0;
                int.TryParse(doc[i], out docId);

                /* Verifica se o fornecedor já possui o documento que foi solicitado */
                var documentoExistente =controller.Db.WFD_PJPF_DOCUMENTOS.FirstOrDefault(x =>
                    x.CONTRATANTE_PJPF_ID == contratantePJPF.ID &&
                    x.DESCRICAO_DOCUMENTO_ID == docId);

                FornecedoresExtensions.PopularListaSolicitacaoDocumento(doc, hdnObrigatorio, hdnTipoAtualizacao, listaSolicitacoesDeDocumentos, solicitacao, contratantePJPF, i, documentoExistente, model.ExpiracaoDocumento);

                //Verificar se esse AddRange não deveria estar fora do For
               controller.Db.WFD_SOL_DOCUMENTOS.AddRange(listaSolicitacoesDeDocumentos);
            }

            /* Gravação da Mensagem */
            SOLICITACAO_MENSAGEM mensagem = FornecedoresExtensions.PopularSolicitacaoMensagem(model, solicitacao);
           controller.Db.Entry(mensagem).State = EntityState.Added;

           controller.Db.SaveChanges();

            int arquivoId = 0;

            if (!String.IsNullOrEmpty(model.ArquivoSubido))
            {
                arquivoId = controller._fornecedorArquivoService.GravarArquivoSolicitacao(contratanteId, model.ArquivoSubido, model.TipoArquivoSubido);
            }

            controller._tramite.AtualizarTramite(contratanteId, solicitacao.ID, fluxoId, papelAtual, (int)EnumStatusTramite.Aprovado, usuarioId);

            if (arquivoId != 0)
                controller.ValidarArquivoEnviaEmailAnexo(model, contratanteId, item, arquivoId);
            else
                controller.EnviarEmailContratantes(model, contratanteId, item);
        }
        
        public static List<SelectListItem> ListarContratantesCombo(this FornecedoresController controller, List<WFD_CONTRATANTE_PJPF> contratantes)
        {
            var listaBanco = Mapper.Map<List<SelectListItem>>(contratantes);
            listaBanco.Insert(0, new SelectListItem { Text = "--Selecione--", Value = null });
            return listaBanco;
        }

        //public static void ListarFornecedoresComFichaCadastral(this FornecedoresController controller, int? CategoriaSelecionada, string Fornecedor, string CNPJ, int grupoId, int pagina, List<FornecedoresVM> fornecedoresVM, PesquisaFornecedoresVM modelo, out int totalRegistro, out int totalPaginas)
        //{
        //    modelo.CNPJ = Mascara.RemoverMascaraCpfCnpj(modelo.CNPJ);
        //    modelo.CPF = Mascara.RemoverMascaraCpfCnpj(modelo.CPF);
        //    CNPJ = Mascara.RemoverMascaraCpfCnpj(CNPJ);

        //    Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtro = Predicativos.FiltrarFornecedoresPesquisaGrid(modelo, grupoId);
        //    RetornoPesquisa<WFD_CONTRATANTE_PJPF> listaPesquisa = new RetornoPesquisa<WFD_CONTRATANTE_PJPF>();
        //    Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao = (WFD_CONTRATANTE_PJPF a) => a.ID;

        //    using (var bpContratantePjPf = new ContratantePjpfBP())
        //        listaPesquisa = bpContratantePjPf.BuscarPesquisa(filtro, controller.TamanhoPagina, pagina, ordenacao);

        //    totalRegistro = listaPesquisa.TotalRegistros;
        //    totalPaginas = listaPesquisa.TotalPaginas;
        //    controller.ViewBag.TotalPaginas = totalPaginas;
        //    controller.ViewBag.TotalRegistros = totalRegistro;
        //    var fornecedores = listaPesquisa.RegistrosPagina;

        //    var fornecedoresId = fornecedores.Select(f => f.PJPF_ID).Distinct().ToList();
        //    var contratantesGp = controller.Db.Contratante.Where(x => x.WFD_GRUPO.Any(y => y.ID == grupoId)).Select(z => z.ID).ToList();
        //    var fornecedoresContratantes = controller.Db.WFD_CONTRATANTE_PJPF.Where(c => fornecedoresId.Contains(c.PJPF_ID)).ToList();

        //    fornecedores.ForEach(contratantePjpf =>
        //    {
        //        bool tudoAmpliado = true;

        //        contratantesGp.ForEach(item =>
        //        {
        //            if (!fornecedoresContratantes.Any(x => x.PJPF_ID == contratantePjpf.PJPF_ID && x.CONTRATANTE_ID == item))
        //                tudoAmpliado = false;
        //        });

        //        fornecedoresVM.Add(new FornecedoresVM
        //        {
        //            ID = contratantePjpf.ID,
        //            ContratanteID = contratantePjpf.CONTRATANTE_ID,
        //            Categoria = (int)contratantePjpf.CATEGORIA_ID,
        //            CodigoERP = contratantePjpf.PJPF_COD_ERP,
        //            RazaoSocial = contratantePjpf.Fornecedor.TIPO_PJPF_ID == 3 ? contratantePjpf.Fornecedor.NOME : contratantePjpf.Fornecedor.RAZAO_SOCIAL,
        //            NomeEmpresa = contratantePjpf.Contratante.RAZAO_SOCIAL,
        //            CNPJ = contratantePjpf.Fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(contratantePjpf.Fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(contratantePjpf.Fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00"),
        //            UrlEditar = controller.Cripto.Criptografar(string.Format("FornecedorID={0}&ContratanteID={1}", contratantePjpf.PJPF_ID, contratantePjpf.CONTRATANTE_ID), controller.Key),
        //            Status = contratantePjpf.WFD_PJPF_STATUS.STATUS_NM,
        //            Bloqueado = ((contratantePjpf.PJPF_STATUS_ID == null) ? 0 : (contratantePjpf.PJPF_STATUS_ID == 2 ? 1 : 0)),
        //            TudoAmpliado = tudoAmpliado ? 1 : 0,
        //            Solicitacao = 0
        //        });
        //    });
        //}

        public static void ListarFornecedoresSemFichaCadastral(this FornecedoresController controller, int? CategoriaSelecionada, string Fornecedor, string CNPJ, string CPF, int grupoId, int pagina, List<FornecedoresVM> fornecedoresVM, out int totalRegistro, out int totalPaginas)
        {
            //BUSCA FORNECEDORES E MONTA PAGINAÇÃO
            var filtro = PredicateBuilder.New<FORNECEDORBASE>();
            filtro = filtro.And(d => d.WFD_CONTRATANTE.WFD_GRUPO.Any(g => g.ID == grupoId));

            if (CategoriaSelecionada != null)
                filtro = filtro.And(f => f.CATEGORIA_ID == CategoriaSelecionada);

            if (!string.IsNullOrEmpty(Fornecedor))
                filtro = filtro.And(c => c.RAZAO_SOCIAL.Contains(Fornecedor));

            if (!string.IsNullOrEmpty(CNPJ))
                filtro = filtro.And(c => c.CNPJ == CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", ""));

            if (!string.IsNullOrEmpty(CNPJ))
                filtro = filtro.And(c => c.CNPJ == CPF.Replace(".", "").Replace("-", ""));

            totalRegistro = controller.Db.WFD_PJPF_BASE.AsExpandable().Count(filtro);
            List<FORNECEDORBASE> fornecedores = controller.Db.WFD_PJPF_BASE
                .Include("Contratante.WFD_GRUPO")
                .AsExpandable()
                .Where(filtro)
                .OrderBy(d => d.ID)
                .Skip(controller.TamanhoPagina * (pagina - 1))
                .Take(controller.TamanhoPagina)
                .ToList();

            totalPaginas = (int)Math.Ceiling(totalRegistro / (double)controller.TamanhoPagina);
            controller.ViewBag.TotalPaginas = totalPaginas;
            controller.ViewBag.TotalRegistros = totalRegistro;

            fornecedoresVM.AddRange(
                fornecedores.Select(f => new FornecedoresVM
                {
                    ID = f.ID,
                    Categoria = (f.CATEGORIA_ID.HasValue) ? f.CATEGORIA_ID.Value : 0,
                    RazaoSocial = f.RAZAO_SOCIAL,
                    Telefone = f.TELEFONE,
                    CNPJ = Convert.ToUInt64(f.CNPJ).ToString(@"00\.000\.000\/0000\-00"),
                    Ativo = f.ATIVO,
                    UrlEditar = controller.Cripto.Criptografar(string.Format("id={0}", f.ID), controller.Key)
                }));
        }

        public static void ManterDadosBancarios(this FornecedoresController controller, List<DadosBancariosVM> dadosBancarios, int solicitacaoCriacaoID, int contratanteID)
        {
            foreach (var item in dadosBancarios)
            {
                item.SolicitacaoID = solicitacaoCriacaoID;
                item.ContratanteID = contratanteID;

                var arquivoId = 0;
                if (string.IsNullOrEmpty(item.NomeArquivo))
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = controller._fornecedorArquivoService.GravarArquivoSolicitacao(contratanteID, item.ArquivoSubido, item.TipoArquivoSubido);
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = controller._fornecedorArquivoService.SubstituirArquivoSolicitacaoBancario(contratanteID, (int)item.BancoSolicitacaoID, (int)item.ArquivoID, item.ArquivoSubido, item.TipoArquivoSubido, item.NomeArquivo);
                }


                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.ArquivoID = arquivoId;
                    item.NomeArquivo =controller._fornecedorArquivoService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
            }

            List<SolicitacaoModificacaoDadosBancario> solicitacoesModBancoMapeadas = Mapper.Map<List<DadosBancariosVM>, List<SolicitacaoModificacaoDadosBancario>>(dadosBancarios);

           controller._solicitacaoModificacaoBancoService.ManterBancoCadastroFornecedor(solicitacoesModBancoMapeadas, solicitacaoCriacaoID);
        }

        public static void ManterDadosContatos(this FornecedoresController controller, List<DadosContatoVM> dadosContatos, int solicitacaoCriacaoID)
        {
            List<SolicitacaoModificacaoDadosContato> solicitacoesModContatoMapeadas = Mapper.Map<List<DadosContatoVM>, List<SolicitacaoModificacaoDadosContato>>(dadosContatos)
                .Select(x =>
                {
                    x.SOLICITACAO_ID = solicitacaoCriacaoID;
                    return x;
                }).ToList();

           controller._solicitacaoModificacaoContatoService.ManterContatoCadastroFornecedor(solicitacoesModContatoMapeadas, solicitacaoCriacaoID);
        }

        public static void ManterDadosEnderecos(this FornecedoresController controller, List<DadosEnderecosVM> dadosEnderecos, int solicitacaoCriacaoID)
        {
            var solicitacoesModEndereco =controller._solicitacaoModificacaoEnderecoService.ListarPorSolicitacaoId(solicitacaoCriacaoID).ToList();
            var solicitacoesModEnderecoPostadas = dadosEnderecos.Select(x => x.ID).ToArray();
            var solicitacoesModContatoExcluidas = solicitacoesModEndereco.Where(x => !solicitacoesModEnderecoPostadas.Contains(x.ID)).ToList();

           controller._solicitacaoModificacaoEnderecoService.ExcluirSolicitacoes(solicitacoesModContatoExcluidas);

            List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoesModEnderecoMapeadas = Mapper.Map<List<DadosEnderecosVM>, List<SOLICITACAO_MODIFICACAO_ENDERECO>>(dadosEnderecos)
                .Select(x =>
                {
                    x.SOLICITACAO_ID = solicitacaoCriacaoID;
                    return x;
                }).ToList();
           controller._solicitacaoModificacaoEnderecoService.InserirOuAtualizarSolicitacoes(solicitacoesModEnderecoMapeadas);
        }

        public static void ManterDocumentos(this FornecedoresController controller, List<SolicitacaoDocumentosVM> solicitacoesDocumentosVM, int solicitacaoCriacaoID, int contratanteId)
        {
            var solicitacoesDocumentos = controller._pjPfSolicitacaoDocumentosService.ListarPorSolicitacaoId(solicitacaoCriacaoID);

            foreach (var item in solicitacoesDocumentosVM)
            {
                var arquivoId = 0;

                var solicitacaoDocumentos = solicitacoesDocumentos.FirstOrDefault(x => x.ID == item.ID);
                if (string.IsNullOrEmpty(item.NomeArquivo))
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        arquivoId = controller._fornecedorArquivoService.GravarArquivoSolicitacao(contratanteId, item.ArquivoSubido, item.TipoArquivoSubido);
                        solicitacaoDocumentos.ARQUIVO_ID = arquivoId;
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        arquivoId = controller._fornecedorArquivoService.SubstituirArquivoSolicitacaoDocumento(contratanteId, item.ID, (int)item.ArquivoID, item.ArquivoSubido, item.TipoArquivoSubido, item.NomeArquivo);
                        solicitacaoDocumentos.ARQUIVO_ID = arquivoId;
                    }
                }

                if (item.Periodicidade != null)
                {
                    if (item.Periodicidade > 0)
                        solicitacaoDocumentos.DATA_VENCIMENTO = controller.CalculaDataVencimento((int)item.Periodicidade);
                }
                else
                {
                    if (item.PorValidade != null)
                        if ((bool)item.PorValidade)
                            solicitacaoDocumentos.DATA_VENCIMENTO = item.DataValidade;
                }

                controller._pjPfSolicitacaoDocumentosService.AtualizarSolicitacao(solicitacaoDocumentos);

                item.ID = solicitacaoDocumentos.ID;
                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.NomeArquivo = controller._fornecedorArquivoService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoID = arquivoId;
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
                item.SolicitacaoID = (int)solicitacaoDocumentos.SOLICITACAO_ID;

            }
        }

        public static void ManterUnspsc(this FornecedoresController controller, List<FornecedorUnspscVM> unspsc, int solicitacaoCriacaoID)
        {
            List<SOLICITACAO_UNSPSC> wfd_sol_unspsc = new List<SOLICITACAO_UNSPSC>();

            unspsc.ForEach(x => wfd_sol_unspsc.Add(new SOLICITACAO_UNSPSC
            {
                SOLICITACAO_ID = solicitacaoCriacaoID,
                UNSPSC_ID = x.UsnpscId
            }));
            controller.SolicitacaoMaterialEServicoService.ManterUnspscSolicitacao(wfd_sol_unspsc, solicitacaoCriacaoID);
        }

        public static void PersistirDadosEmMemoria(this FornecedoresController controller)
        {
            //ViewBag.Bancos
            if (controller.TempData["Bancos"] == null)
                controller.TempData["Bancos"] = new SelectList(controller._bancoBP.ListarTodosPorNome(), "ID", "BANCO_NM");

            controller.ViewBag.Bancos = controller.TempData["Bancos"] as SelectList;
            controller.TempData.Keep("Bancos");
        }

        public static void PersistirDadosEnderecoEmMemoria(this FornecedoresController controller)
        {
            if (controller.TempData["UF"] == null)
                controller.TempData["UF"] = new SelectList(controller._enderecoBP.ListarTodosPorNome(), "UF_SGL", "UF_NM");

            controller.ViewBag.UF = controller.TempData["UF"] as SelectList;
            controller.TempData.Keep("UF");

            if (controller.TempData["TipoEndereco"] == null)
                controller.TempData["TipoEndereco"] = new SelectList(controller._enderecoBP.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");

            controller.ViewBag.TipoEndereco = controller.TempData["TipoEndereco"] as SelectList;
            controller.TempData.Keep("TipoEndereco");
        }

        public static void PopularSolicitacaoEmAprovacao(this FornecedoresController controller, int contratanteId, int fornecedorId, int? usuarioId, int fluxoId, SOLICITACAO solicitacao)
        {
            if (contratanteId != 0)
                solicitacao.CONTRATANTE_ID = contratanteId;

            solicitacao.FLUXO_ID = fluxoId; // Bloqueio
            solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
            solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
            solicitacao.USUARIO_ID = usuarioId;
            solicitacao.PJPF_ID = fornecedorId;
        }

        public static void ValidarArquivoEnviaEmailAnexo(this FornecedoresController controller, FornecedoresSolicitacaoDocumentosVM model, int contratanteId, Fornecedor item, int arquivoId)
        {
            var arquivo = controller.Db.WFD_ARQUIVOS.FirstOrDefault(x => x.ID == arquivoId);

            string caminhoComArquivo = string.Empty;
            caminhoComArquivo = string.Format("{0}{1}##{2}", arquivo.CAMINHO, arquivo.ID, arquivo.NOME_ARQUIVO);

            string nomeArquivo = arquivo.NOME_ARQUIVO;

            byte[] btArquivo = System.IO.File.ReadAllBytes(caminhoComArquivo);

            Stream st = new MemoryStream(btArquivo);

            item.WFD_CONTRATANTE_PJPF
                .FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId)
                .WFD_PJPF_CONTATOS
                .ToList()
                .ForEach(x =>
                {
                    controller._metodosGerais.EnviarEmail(x.EMAIL, model.Assunto, model.Mensagem, st, nomeArquivo);
                });
        }

        public static void ValidarFormularioCriacaoSolicitacao(this FornecedoresController controller, FornecedoresVM model, string Acao, int contratante)
        {
            //if (!string.IsNullOrEmpty(model.Email))
            //    if (!Validacao.ValidarEmail(model.Email))
            //        controller.ModelState.AddModelError("Contato.Email", "O e-mail informado não está em um formato válido.");

            //if (model.Categoria == 0)
            //    controller.ModelState.AddModelError("Categoria", "Informe a Categoria!");

            //if (model.TipoFornecedor == 1 || model.TipoFornecedor == 3)
            //{
            //    if (model.CNPJ == null)
            //        controller.ModelState.AddModelError("CNPJ", "CNPJ/CPF Obrigatório");
            //    else
            //    {
            //        if (model.TipoFornecedor == 1)
            //        {
            //            if (!Validacao.ValidaCNPJ(model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
            //                controller.ModelState.AddModelError("CNPJ", "CNPJ Inválido");
            //        }
            //        else
            //        {
            //            if (!Validacao.ValidaCPF(model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
            //                controller.ModelState.AddModelError("CNPJ", "CPF Inválido");
            //        }
            //    }
            //}

            if (Acao == "Incluir")
            {
                if (model.TipoFornecedor != 2)
                {
                    var cnpjteste = model.CNPJ == null
                        ? string.Empty
                        : model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

                    if (controller._fornecedorService.BuscarPorCnpj(cnpjteste, contratante) != 0)
                        controller.ModelState.AddModelError("CNPJ", "Já existe um fornecedor cadastrado com esse CNPJ/CPF!");

                    if (controller._solicitacaoCadastroFornecedorService.ValidarSolicitacaoCriacao(6, cnpjteste, contratante) != null)
                        controller.ModelState.AddModelError("CNPJ", "Já existe uma solicitação de criação para este CNPJ/CPF!");
                }
                else
                {
                    if (controller._fornecedorService.BuscarPorRazaoSocial(model.RazaoSocial) != null)
                        controller.ModelState.AddModelError("RazaoSocial", "Já existe um fornecedor cadastrado com esta Razão Social");

                    if (controller._solicitacaoCadastroFornecedorService.BuscarPorRazaoSocial(model.RazaoSocial) != null)
                        controller.ModelState.AddModelError("RazaoSocial", "Já existe uma solicitação de criação para esta Razão Social");
                }
            }

            // SE TIPO ESTRANGEIRO RETIRA A VALIDACAO DO CNPJ
            if (model.TipoFornecedor == 2)
            {
                controller.ModelState.Remove("CNPJ");
                controller.ModelState.Remove("CONTATO.EMAIL");
            }

            // SE EMPRESA NACIONAL OU PESSOA FÍSICA, RETIRA A VALIDAÇÃO DA RAZÃO SOCIAL
            if (model.TipoFornecedor == 1 || model.TipoFornecedor == 3)
                controller.ModelState.Remove("RAZAOSOCIAL");

            if (model.Categoria > 0)
            {
                FORNECEDOR_CATEGORIA categoria = controller._fornecedorCategoriaService.BuscarPorId(model.Categoria);
                int totalDocCatecoria = categoria.ListaDeDocumentosDeFornecedor.Count;

                if (categoria.ISENTO_DOCUMENTOS && categoria.ISENTO_DADOSBANCARIOS && categoria.ISENTO_CONTATOS)
                {
                    if (model.TipoCadastro == 1)
                        controller.ModelState.AddModelError("", "A Categoria escolhida é isenta de Documentação, Dados Bancários e Contato, no entanto você está tentando enviar a solicitação para o fornecedor sem um e-mail de contato. Para continuar sem contato marque a opção \"Eu preencherei os dados\".");
                    else
                        controller.ModelState.Remove("Email");
                }
                else
                    if (totalDocCatecoria == 0)
                    controller.ModelState.AddModelError("", "Não é possível enviar esta solicitação. A Categoria/Grupo de Contas selecionado não possue Lista de Documento para solicitação!");
            }
        }
    }
}