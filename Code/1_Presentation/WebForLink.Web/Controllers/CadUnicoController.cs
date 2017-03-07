using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class CadUnicoController : ControllerPadrao
    {
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly IBancoWebForLinkAppService _bancoBP;
        private readonly ICadastroUnicoWebForLinkAppService _cadastroUnicoBP;
        private readonly IInformacaoComplementarWebForLinkAppService _informacaoComplementarBP;
        public CadUnicoController(ISolicitacaoWebForLinkAppService solicitacao, 
            IBancoWebForLinkAppService bancoBP, 
            ICadastroUnicoWebForLinkAppService cadastroUnicoBP,
            IInformacaoComplementarWebForLinkAppService informacaoComplementarBP)
            : base()
        {
            _solicitacaoService = solicitacao;
            _bancoBP = bancoBP;
            _cadastroUnicoBP = cadastroUnicoBP;
            _informacaoComplementarBP = informacaoComplementarBP;
        }

        #region JSONs

        public JsonResult LiberarProximasRespostas(int idPerguntaPai, int idRespostaPai)
        {
            List<PerguntaModeloList> resultadoList = new List<PerguntaModeloList>();
            var perguntas = Db.QIC_QUEST_ABA_PERG.Where(x => x.PERG_PAI == idPerguntaPai);
            var perguntasDisponiveis = (from a in Db.QIC_QUEST_ABA_PERG
                from b in a.QIC_QUEST_ABA_PERG_PAPEL
                where a.PERG_PAI == idPerguntaPai && b.PAPEL_ID == 1 && b.ESCRITA
                select a.ID).ToList();
            foreach (var pergunta in perguntasDisponiveis)
            {
                var respostasPerguntas = Db
                    .QIC_QUEST_ABA_PERG_RESP
                    .Where(x =>
                        x.PERG_ID == pergunta &&
                        x.RESP_PAI_ID == idRespostaPai);
                var resultado = new PerguntaModeloList
                {
                    perguntaId = pergunta,
                    respostas = respostasPerguntas.Select(x => new SelectListItem
                    {
                        Text = string.Concat(x.RESP_COD, "-", x.RESP_DSC),
                        Value = x.ID.ToString(),
                    }).ToList()
                };
                resultado.respostas.Insert(0, new SelectListItem() {Value = "0", Text = "--Selecione", Selected = true});
                resultadoList.Add(resultado);
            }
            return Json(resultadoList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [Authorize]
        public ActionResult Index(int solicitacaoID)
        {
            try
            {
                //TODO: Nelson Neto - RETIRAR OS COMENTÁRIOS
                //int idSolicitacao = 124;
                int solicitacaoTipoID = 1;
                int papelID = 1;
                var contratanteID = (int)Geral.PegaAuthTicket("ContratanteId");

                ViewBag.Fluxo = solicitacaoTipoID;
                ViewBag.Bancos = _bancoBP.ListarTodosPorNome();
                
                #region Ficha Cadastral

                FichaCadastralWebForLinkVM modelo = new FichaCadastralWebForLinkVM
                {
                    DadosBancarios = new List<DadosBancariosVM>(),
                    DadosContatos = new List<DadosContatoVM>(),
                    SolicitacaoFornecedor =
                        new SolicitacaoFornecedorVM {Documentos = new List<SolicitacaoDocumentosVM>()},
                    Aprovacao = new AprovacaoVM(),
                    Solicitacao = new SolicitacaoVM {Fluxo = new FluxoVM()},
                    DadosBloqueio = new DadosBloqueioVM(),
                    FornecedorRobo = new FornecedorRoboVM(),
                    FornecedoresUnspsc = new List<FornecedorUnspscVM>()
                };

                #region BPs

                SOLICITACAO solicitacao = _solicitacaoService.BuscarPorSolicitacaoId(solicitacaoID);
                if (solicitacao == null)
                    throw new Exception(string.Format("Solicitação com id {0} inexistente.", solicitacaoID));
                SolicitacaoCadastroFornecedor cadastroPJPF = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();
                List<SolicitacaoModificacaoDadosBancario> cadastroBanco = solicitacao.SolicitacaoModificacaoDadosBancario.ToList();
                List<SolicitacaoModificacaoDadosContato> cadastroContatos = solicitacao.SolicitacaoModificacaoDadosContato.ToList();

                #endregion

                FichaCadastralWebForLinkVM fichaCadastralVM = new FichaCadastralWebForLinkVM()
                {
                    ID = solicitacao.ID,
                    ContratanteID = solicitacao.CONTRATANTE_ID,
                    ContratanteFornecedorID = 0,
                    NomeEmpresa = cadastroPJPF != null
                        ? cadastroPJPF.RAZAO_SOCIAL
                        : string.Empty,
                    RazaoSocial = cadastroPJPF.RAZAO_SOCIAL,
                    NomeFantasia = cadastroPJPF.NOME_FANTASIA,
                    //CNAE = cadastroPJPF.CNAE,
                    CNPJ_CPF = cadastroPJPF.CNPJ ?? cadastroPJPF.CPF,
                    InscricaoEstadual = cadastroPJPF.INSCR_ESTADUAL,
                    InscricaoMunicipal = cadastroPJPF.INSCR_MUNICIPAL,
                    TipoLogradouro = cadastroPJPF.TP_LOGRADOURO,
                    Endereco = cadastroPJPF.ENDERECO,
                    Numero = cadastroPJPF.NUMERO,
                    Complemento = cadastroPJPF.COMPLEMENTO,
                    Cep = cadastroPJPF.CEP,
                    Bairro = cadastroPJPF.BAIRRO,
                    Cidade = cadastroPJPF.CIDADE,
                    Estado = cadastroPJPF.UF,
                    Pais = cadastroPJPF.PAIS,
                    Observacao = cadastroPJPF.OBSERVACAO,
                    TipoFornecedor = cadastroPJPF.PJPF_TIPO,
                    DadosBancarios = cadastroBanco.Select(x => new DadosBancariosVM()
                    {
                        Agencia = x.AGENCIA,
                        Digito = x.AG_DV,
                        Banco = x.BANCO_ID,
                        ContaCorrente = x.CONTA,
                        ContaCorrenteDigito = x.CONTA_DV,
                        NomeBanco = x.T_BANCO.BANCO_NM,
                    }).ToList(),
                    DadosContatos = cadastroContatos.Select(x => new DadosContatoVM()
                    {
                        Celular = x.CELULAR,
                        ContatoID = x.CONTATO_PJPF_ID ?? 0,
                        EmailContato = x.EMAIL,
                        NomeContato = x.NOME,
                        Telefone = x.TELEFONE
                    }).ToList()
                };

                #endregion


                List<QUESTIONARIO> questionarioList = _cadastroUnicoBP.BuscarPorIdContratante(contratanteID);
                foreach (QUESTIONARIO questionario in questionarioList)
                {
                    List<QUESTIONARIO_ABA> abasQuestionario = questionario
                        .QIC_QUEST_ABA
                        .OrderBy(x => x.ORDEM)
                        .ToList();
                    List<AbaVM> questionarioAbas = new List<AbaVM>();
                    foreach (QUESTIONARIO_ABA aba in abasQuestionario)
                    {
                        List<QUESTIONARIO_PERGUNTA> perguntasAba = aba
                            .QIC_QUEST_ABA_PERG
                            .OrderBy(x => x.ORDEM)
                            .ToList();
                        List<PerguntaVM> abaPerguntas = new List<PerguntaVM>();
                        foreach (QUESTIONARIO_PERGUNTA pergunta in perguntasAba)
                        {
                            QUESTIONARIO_PAPEL papelPergunta = pergunta
                                .QIC_QUEST_ABA_PERG_PAPEL
                                .FirstOrDefault(x => x.PAPEL_ID == papelID);
                            WFD_INFORM_COMPL resposta = pergunta
                                .WFD_INFORM_COMPL
                                .FirstOrDefault(x => x.PERG_ID == pergunta.ID);
                            if (papelPergunta.LEITURA)
                            {
                                PerguntaVM perguntaVM = Mapper.Map<PerguntaVM>(pergunta);
                                Mapper.Map<PerguntaVM>(papelPergunta);
                                
                                if (pergunta.TP_DADO == "DOMINIO" && pergunta.DOMINIO == true)
                                {
                                    perguntaVM.DominioList =
                                        Mapper.Map<List<QUESTIONARIO_RESPOSTA>, List<SelectListItem>>(pergunta
                                            .QIC_QUEST_ABA_PERG_RESP
                                            .OrderBy(x => x.ORDEM).ToList());

                                    perguntaVM.DominioList.Insert(0,
                                        new SelectListItem()
                                        {
                                            Value = "0",
                                            Text = "--Selecione",
                                            Selected = true
                                        });
                                    perguntaVM.DominioListId = !string.IsNullOrEmpty(perguntaVM.Resposta)
                                        ? int.Parse(perguntaVM.Resposta)
                                        : 0;
                                }

                                if (pergunta.PERG_PAI != null)
                                {
                                    int pergPai = pergunta.PERG_PAI ?? 0;
                                    WFD_INFORM_COMPL informaCompl = _informacaoComplementarBP.BuscarPorPerguntaId(pergPai);

                                    //TODO: Validar LEITURA
                                    ///Validar se pode ser desbloqueado por resposta do pai
                                    /// pelo papel
                                    /// pela resposta anterior
                                    perguntaVM.Leitura = informaCompl != null;

                                    perguntaVM.PaiRespondido = informaCompl != null
                                        ? informaCompl.PERG_ID
                                        : 0;
                                }

                                //perguntaVM.Leitura = papelPergunta.
                                    abaPerguntas.Add(perguntaVM);
                            }
                        }
                        //Mapear Lista de aba com Modelo de aba
                        AbaVM abaModelo = Mapper.Map<QUESTIONARIO_ABA, AbaVM>(aba);
                        abaModelo.PerguntaList = abaPerguntas;
                        questionarioAbas.Add(abaModelo);
                    }
                    //Mapear Lista de Questionários com Modelo de Questionários
                    QuestionarioVM questionarioModelo = Mapper.Map<QUESTIONARIO, QuestionarioVM>(questionario);
                    questionarioModelo.AbaList = questionarioAbas;
                    fichaCadastralVM.Questionarios.QuestionarioDinamicoList.Add(questionarioModelo);
                }
                return View(fichaCadastralVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ViewBag.Message = ex.Message;
                return View(new FichaCadastralWebForLinkVM());
            }
        }

        [HttpPost]
        public ActionResult Index(FichaCadastralWebForLinkVM model)
        {
            try
            {
                int solicitacaoId = model.ID;
                List<PerguntaVM> perguntaVms = (from questionario in model.Questionarios.QuestionarioDinamicoList
                    from aba in questionario.AbaList
                    from pergunta in aba.PerguntaList
                    select pergunta).ToList();

                List<WFD_INFORM_COMPL> informacaoComplementarList = perguntaVms
                    .Where(x => x.Resposta != null || x.DominioListId != 0)
                    .Select(x => new WFD_INFORM_COMPL()
                    {
                        SOLICITACAO_ID = solicitacaoId,
                        PERG_ID = x.Id,
                        RESPOSTA = string.IsNullOrEmpty(x.Resposta) ? x.DominioListId.ToString() : x.Resposta,
                        ID = x.RespostaId
                    }).ToList();

                _informacaoComplementarBP.InsertAll(informacaoComplementarList);
                return RedirectToAction("index", new {idSolicitacao = solicitacaoId});
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ViewBag.Message = ex.Message;
                return View(new FichaCadastralWebForLinkVM());
            }
        }
    }
}