using AutoMapper;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DocumentoController : ControllerPadrao
    {
        private readonly ISolicitacaoDocumentoWebForLinkAppService _solicitacaoDocumentoService;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly IFornecedorWebForLinkAppService _fornecedorService;
        private readonly ISolicitacaoCadastroFornecedorWebForLinkAppService _solicitacaoCadastroFornecedorService;
        private readonly ISolicitacaoTramiteWebForLinkAppService _solicitacaoTramiteService;
        private readonly IUsuarioWebForLinkAppService _usuarioBP;
        private readonly IBancoWebForLinkAppService _bancoBP;
        private readonly IEnderecoWebForLinkAppService _enderecoBP;
        private readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoBP;
        private readonly IFornecedorArquivoWebForLinkAppService _fornecedorArquivoService;
        private readonly IPapelWebForLinkAppService _papelBP;
        private readonly ICadastroUnicoWebForLinkAppService _cadastroUnicoBP;
        private readonly IFornecedorCategoriaWebForLinkAppService _fornecedorCategoriaService;
        private readonly IInformacaoComplementarWebForLinkAppService _informacaoComplementarBP;
        private readonly ISolicitacaoModificacaoBancoWebForLinkAppService _solicitacaoModificacaoBancoBP;
        private readonly ISolicitacaoDocumentosFornecedorWebForLinkAppService _pjpfSolicitacaoDocumentosBP;
        private readonly IConfiguracaoEmailContratanteWebForLinkAppService _contratanteConfiguracaoEmailBP;
        private readonly ISolicitacaoModificacaoContatoWebForLinkAppService _solicitacaoModificacaoContatoBP;
        private readonly ISolicitacaoModificacaoEnderecoWebForLinkAppService _solicitacaoModificacaoEnderecoBP;
        private readonly IServicosMateriaisWebForLinkAppService _unspscBP;
        private readonly ISolicitacaoMaterialEServicoWebForLinkAppService SolicitacaoMaterialEServico;
        private readonly ITramiteWebForLinkAppService _tramite;

        public DocumentoController(
            ISolicitacaoDocumentoWebForLinkAppService _solicitacaoDocumento,
            ISolicitacaoWebForLinkAppService _solicitacao,
            IFornecedorWebForLinkAppService fornecedor,
            ISolicitacaoCadastroFornecedorWebForLinkAppService solicitacaoCadastroFornecedor,
            ISolicitacaoTramiteWebForLinkAppService solicitacaoTramite,
            IUsuarioWebForLinkAppService usuario,
            IBancoWebForLinkAppService banco,
            IEnderecoWebForLinkAppService endereco,
            IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracao,
            IFornecedorArquivoWebForLinkAppService fornecedorArquivo,
            IPapelWebForLinkAppService papel,
            ICadastroUnicoWebForLinkAppService cadastroUnico,
            IFornecedorCategoriaWebForLinkAppService fornecedorCategoria,
            IInformacaoComplementarWebForLinkAppService informacaoComplementar,
            ISolicitacaoModificacaoBancoWebForLinkAppService solicitacaoModificacaoBanco,
            ISolicitacaoDocumentosFornecedorWebForLinkAppService pJpFSolicitacaoDocumentos,
            IConfiguracaoEmailContratanteWebForLinkAppService contratanteConfiguracaoEmail,
            ISolicitacaoModificacaoContatoWebForLinkAppService solicitacaoModificacaoContato,
            ISolicitacaoModificacaoEnderecoWebForLinkAppService solicitacaoModificacaoEndereco,
            IServicosMateriaisWebForLinkAppService servicosMateriais,
            ISolicitacaoMaterialEServicoWebForLinkAppService solicitacaoMaterialEServico,
            ITramiteWebForLinkAppService tramite
        )
            : base()
        {
            _unspscBP = servicosMateriais;
            _solicitacaoDocumentoService = _solicitacaoDocumento;
            _solicitacaoService = _solicitacao;
            _fornecedorService = fornecedor;
            _solicitacaoCadastroFornecedorService = solicitacaoCadastroFornecedor;
            _solicitacaoTramiteService = solicitacaoTramite;
            _usuarioBP = usuario;
            _bancoBP = banco;
            _enderecoBP = endereco;
            _contratanteConfiguracaoBP = contratanteConfiguracao;
            _fornecedorArquivoService = fornecedorArquivo;
            _papelBP = papel;
            _cadastroUnicoBP = cadastroUnico;
            _fornecedorCategoriaService = fornecedorCategoria;
            _informacaoComplementarBP = informacaoComplementar;
            _solicitacaoModificacaoBancoBP = solicitacaoModificacaoBanco;
            _pjpfSolicitacaoDocumentosBP = pJpFSolicitacaoDocumentos;
            _contratanteConfiguracaoEmailBP = contratanteConfiguracaoEmail;
            _solicitacaoModificacaoContatoBP = solicitacaoModificacaoContato;
            _solicitacaoModificacaoEnderecoBP = solicitacaoModificacaoEndereco;
            SolicitacaoMaterialEServico = solicitacaoMaterialEServico;
            _tramite = tramite;

        }

        #region SOLICITACAO DE DOCUMENTOS
        //SOLICITACAO DE DOCUMENTOS
        [Authorize]
        public ActionResult SolicitacaoFornecedores(int? Pagina, int? Categorias, string Fornecedores, string FornecedoresSelecionados, int[] FornecedoresRemover, string MensagemSucesso, string Acao)
        {
            // Adicionar ou remover
            if (Acao != "Proximo")
            {
                int passoAtual = 1;
                ViewBag.PassoAtual = passoAtual;
                int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

                int pagina = Pagina ?? 1;
                ViewBag.Pagina = pagina;
                ViewBag.MensagemSucesso = MensagemSucesso ?? "";
                int totalRegistro = 0;
                int totalPaginas;
                SolicitacaoFornecedorVM solicitacao;

                if (Session["Solicitacao"] == null)
                {
                    solicitacao = new SolicitacaoFornecedorVM
                    {
                        PassoAtual = passoAtual
                    };
                }
                else
                {
                    solicitacao = (SolicitacaoFornecedorVM)Session["Solicitacao"];
                }

                solicitacao.PassoAtual = passoAtual;

                List<FORNECEDOR_CATEGORIA> categorias = _fornecedorCategoriaService.ListarTodosPorIdContratanteAtivo(contratanteId);

                if (categorias != null)
                    ViewBag.Categorias = new SelectList(categorias, "ID", "DESCRICAO", solicitacao.Categoria);
                else
                    ViewBag.Categorias = new SelectList(new List<FORNECEDOR_CATEGORIA>(), "ID", "DESCRICAO");


                if (Acao == "AdicionarFornecedoresPorCategoria")
                {
                    if (Categorias == null)
                    {
                        ModelState.AddModelError("Categorias", "Selecione uma Categoria");
                    }
                    else
                    {
                        solicitacao.Categoria = (int)Categorias;
                        List<FORNECEDORBASE> fornecedores = Db.WFD_PJPF_BASE
                            .Include("WFD_PJPF_BASE_CONTATOS")
                            .Where(f => f.CONTRATANTE_ID == contratanteId && f.CATEGORIA_ID == (int)Categorias).ToList();

                        if (fornecedores != null)
                        {
                            foreach (FORNECEDORBASE f in fornecedores)
                            {
                                if (solicitacao.Fornecedores.All(ff => ff.ID != f.ID))
                                {
                                    solicitacao.Fornecedores.Add(new SolicitacaoFornecedoresVM
                                    {
                                        ID = f.ID,
                                        NomeFornecedor = f.RAZAO_SOCIAL,
                                        CNPJ = f.CNPJ,
                                        Emails = f.WFD_PJPF_BASE_CONTATOS
                                            .Select(contato => new SolicitacaoFornecedoresContatosVM
                                            {
                                                ID = contato.ID,
                                                Nome = contato.NOME,
                                                Email = contato.EMAIL
                                            }).ToList()
                                    });
                                }
                            }
                        }
                    }
                }

                if (Acao == "AdicionarFornecedoresPorNome")
                {
                    if (Fornecedores == null || string.IsNullOrEmpty(Fornecedores))
                    {
                        ModelState.AddModelError("Fornecedores", "Selecione um ou mais Fornecedores!");
                    }
                    else
                    {
                        int[] vfornecedoresSelecionados = Array.ConvertAll(Fornecedores.Split(new Char[] { ',' }), int.Parse);

                        List<FORNECEDORBASE> fornecedores = Db.WFD_PJPF_BASE.Include("WFD_PJPF_BASE_CONTATOS").Where(f => f.CONTRATANTE_ID == contratanteId && vfornecedoresSelecionados.Contains(f.ID)).ToList();
                        if (fornecedores != null)
                        {
                            foreach (FORNECEDORBASE f in fornecedores)
                            {
                                if (solicitacao.Fornecedores.All(ff => ff.ID != f.ID))
                                {
                                    solicitacao.Fornecedores.Add(new SolicitacaoFornecedoresVM
                                    {
                                        ID = f.ID,
                                        NomeFornecedor = f.RAZAO_SOCIAL,
                                        CNPJ = f.CNPJ,
                                        Emails = f.WFD_PJPF_BASE_CONTATOS.Select(contato => new SolicitacaoFornecedoresContatosVM
                                        {
                                            ID = contato.ID,
                                            Nome = contato.NOME,
                                            Email = contato.EMAIL
                                        }).ToList()
                                    });
                                }
                            }
                        }
                    }
                }

                if (Acao == "Remover")
                {
                    if (FornecedoresRemover == null)
                    {
                        ModelState.AddModelError("Remocao", "Selecione um ou mais Fornecedores para remoção");
                    }
                    else
                    {
                        solicitacao.Fornecedores.RemoveAll(d => FornecedoresRemover.Contains(d.ID));
                    }
                }

                if (Acao == "RemoverTodos")
                {
                    solicitacao.Fornecedores.RemoveAll(d => d.ID > 0);
                }

                totalPaginas = (int)Math.Ceiling(totalRegistro / (double)TamanhoPagina);
                ViewBag.TotalPaginas = totalPaginas;
                ViewBag.TotalRegistros = totalRegistro;

                ViewBag.Fornecedores = solicitacao.Fornecedores;

                Session["Solicitacao"] = solicitacao;
                return View(solicitacao);
            }
            else
            {
                return RedirectToAction("SolicitacaoDocumentos");
            }
        }

        [Authorize]
        public JsonResult BuscaFornecedores(string chave)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            List<Fornecedor> fornecedores = _fornecedorService.ListarTodosPorContratanteIdAtivoChave(contratanteId, chave);
            var fornecedoresJson = fornecedores.Select(fornecedor => new JsonClass
            {
                value = fornecedor.ID.ToString(),
                text = fornecedor.RAZAO_SOCIAL + " - CNPJ: " + Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00")
            }).ToList();

            return Json(fornecedoresJson, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult SolicitacaoDocumentos(int? Pagina, string chaveurl, int? Categorias, string Documentos, string DocumentosSelecionados, int[] DocumentosRemover, string Acao)
        {
            int solicitacaoId = 0;
            int fornecedorId = 0;
            int contratanteId = 0;
            FornecedoresSolicitacaoDocumentosVM fornecedorSolicitacao = new FornecedoresSolicitacaoDocumentosVM();

            try
            {
                fornecedorSolicitacao.ChaveUrl = chaveurl;

                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out solicitacaoId);
                    //Só serão usados no caso de Usuários fornecedores
                    Int32.TryParse(param.First(p => p.Name == "FornecedorID").Value, out fornecedorId);
                    Int32.TryParse(param.First(p => p.Name == "ContratanteID").Value, out contratanteId);

                    // Este parametro é utilizado quando o fornecedor atualiza algum documento e o retorno é sucesso para apresentar alerta.
                    if (param.Any(p => p.Name == "RetSucessoDocs"))
                        ViewBag.RetSucessoDocs = param.First(p => p.Name == "RetSucessoDocs").Value;

                    // Este parametro é utilizado quando o fornecedor atualiza algum comprovante e o retorno é sucesso para apresentar alerta.
                    if (param.Any(p => p.Name == "RetSucessoBancos"))
                        ViewBag.RetSucessoBancos = param.First(p => p.Name == "RetSucessoBancos").Value;

                }

                var fornecedores = _fornecedorService.BuscarPorIdCompleto(fornecedorId);
                WFD_CONTRATANTE_PJPF contratantePJPF;

                if (contratanteId == 0)
                    contratantePJPF = fornecedores.FirstOrDefault();
                else
                    contratantePJPF = fornecedores.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId);

                Contratante contratante = contratantePJPF.WFD_CONTRATANTE;

                PersistirDadosEmMemoria(contratante, contratantePJPF.WFD_PJPF);

                int pagina = 1;
                int tamanhoPagina = 10;
                List<SOLICITACAO> listSolicitacao = Db.WFD_SOLICITACAO
                    .Include(x => x.Fluxo)
                    .Include(x => x.Contratante)
                    .Include(x => x.Fornecedor)
                    //.AsExpandable()
                    .Where(x =>
                        x.CONTRATANTE_ID == contratante.ID &&
                        x.Fluxo.FLUXO_TP_ID == 130 &&
                        x.PJPF_ID == fornecedorId)
                    .OrderBy(x => x.ID)
                    .Skip(tamanhoPagina * (pagina - 1))
                    .ToList();

                //fornecedorSolicitacao.Solicitacoes = listSolicitacao.Take(tamanhoPagina).ToList();

                fornecedorSolicitacao.Solicitacoes = listSolicitacao;



                ViewBag.Pagina = pagina;
                ViewBag.TotalRegistros = listSolicitacao.Count();
                ViewBag.TotalPaginas = (int)Math.Ceiling(listSolicitacao.Count() / (double)tamanhoPagina);

                if (fornecedorId != 0)
                    ViewBag.EhFornecedor = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ViewBag.ChaveConferida = true;
            }

            return View(fornecedorSolicitacao);
        }

        [Authorize]
        public PartialViewResult PartialViewDocumento(int id, int contratanteId, int fornecedorId)
        {
            var fornecedores = _fornecedorService.BuscarPorIdCompleto(fornecedorId);

            WFD_CONTRATANTE_PJPF contratantePJPF = contratanteId == 0
                ? fornecedores.FirstOrDefault()
                : fornecedores.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId);

            List<SolicitacaoDeDocumentos> solicitacoesDocumento = _solicitacaoDocumentoService.ListarPorIdSolicitacao(id);

            FichaCadastralWebForLinkVM ficha = Mapper.Map<Fornecedor, FichaCadastralWebForLinkVM>(contratantePJPF.WFD_PJPF);
            ficha.ActionOrigem = "SolicitacaoDocumentos";
            ficha.ControllerOrigem = "Documento";
            ficha.ContratanteID = contratanteId;
            ficha.ID = fornecedorId;
            ficha.ContratanteFornecedorID = contratantePJPF.ID;
            ficha.HabilitaEdicaoUnspsc = true;
            ficha.PJPFID = fornecedorId;
            ficha.CategoriaId = contratantePJPF.CATEGORIA_ID ?? 0;
            ficha.SolicitacaoFornecedor = new SolicitacaoFornecedorVM();
            ficha.SolicitacaoFornecedor.Documentos = Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacoesDocumento);
            ficha.AtualizacaoDocumento = true;
            ficha.SolicitacaoID = id;

            var solicitacao = Db.WFD_SOLICITACAO
                //.Include("WFD_SOL_DATA_PRORROGACAO")
                .Include("WFD_SOLICITACAO_PRORROGACAO")
                .FirstOrDefault(x => x.ID == id);

            ficha.DataProrrogacao = solicitacao.DT_PRORROGACAO_PRAZO ?? solicitacao.DT_PRAZO;

            ficha.HabilitaEdicao = Db.WFD_SOLICITACAO_TRAMITE
                .Any(x => x.SOLICITACAO_ID == id &&
                x.PAPEL_ID == (int)EnumPapeisWorkflow.Fornecedor &&
                x.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.Aguardando);

            ficha.ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
            var prorrogacao = solicitacao.WFD_SOLICITACAO_PRORROGACAO.Where(o => o.APROVADO == null).LastOrDefault();
            if (prorrogacao != null)
            {
                //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                ficha.ProrrogacaoPrazo =
                    Mapper.Map<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>(prorrogacao);
            }



            return PartialView("_ListarDocumentosSolicitacao", ficha);
        }

        [Authorize]
        public JsonResult BuscaDocumentos(string chave)
        {
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            List<ListaDeDocumentosDeFornecedor> documentos = Db.WFD_PJPF_LISTA_DOCUMENTOS.Where(d => d.CONTRATANTE_ID == ContratanteId && d.ATIVO && d.DescricaoDeDocumentos.DESCRICAO.Contains(chave)).ToList();
            List<JsonClass> DocumentoJson = documentos.Select(documento => new JsonClass
            {
                value = documento.ID.ToString(),
                text = documento.DescricaoDeDocumentos.DESCRICAO,
            }).ToList();

            return Json(DocumentoJson, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult SolicitacaoEnviar(string chaveUrl, string nomeContato, string emailContato, int contratanteId, string documentoPfPj)
        {
            int solicitacaoId = 0;
            var solicitacaoFornecedorVM = new SolicitacaoFornecedorVM();

            try
            {
                if (!string.IsNullOrEmpty(chaveUrl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveUrl, Key);
                    Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out solicitacaoId);
                }
                if (solicitacaoId > 0)
                {
                    ViewBag.TipoDocumentos = new SelectList(Db.WFD_TIPO_DOCUMENTOS.Where(e => e.ATIVO && e.CONTRATANTE_ID == contratanteId).OrderBy(e => e.DESCRICAO), "ID", "DESCRICAO");
                    ViewBag.DescricaoDocumentos = new SelectList(new List<DescricaoDeDocumentos>(), "ID", "DESCRICAO");

                    solicitacaoFornecedorVM.SolicitacaoCriacaoID = solicitacaoId;
                    solicitacaoFornecedorVM.ContratanteSelecionado = contratanteId;
                    solicitacaoFornecedorVM.Fornecedor = new SolicitacaoFornecedoresVM
                    {
                        Contato = new SolicitacaoFornecedoresContatosVM
                        {
                            Nome = nomeContato,
                            Email = emailContato
                        },
                        CNPJ = documentoPfPj
                    };

                    PopularSolicitacaoCadastroPJPF((int)Geral.PegaAuthTicket("ContratanteId"), solicitacaoFornecedorVM);

                    ViewBag.PassoAtual = 3;
                }

                return View(solicitacaoFornecedorVM);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SolicitacaoEnviar(SolicitacaoFornecedorVM model, string Email)
        {
            var solicitacao = _solicitacaoService.BuscarPorIdComDocumentos((int)model.SolicitacaoCriacaoID);
            ViewBag.PassoAtual = model.PassoAtual;

            if (!solicitacao.SolicitacaoDeDocumentos.Any())
                ModelState.AddModelError("", "Não é possível enviar a solicitação sem a lista de Documentos");

            if (ModelState.IsValid)
            {
                try
                {
                    SOLICITACAO_MENSAGEM solicitacaoMensagem = new SOLICITACAO_MENSAGEM
                    {
                        SOLICITACAO_ID = (int)model.SolicitacaoCriacaoID,
                        ASSUNTO = model.Assunto,
                        MENSAGEM = model.Mensagem,
                        DT_ENVIO = DateTime.Now,
                    };
                    foreach (var item in solicitacao.SolicitacaoDeDocumentos)
                    {
                        item.WFD_SOL_MENSAGEM = solicitacaoMensagem;
                    }
                    _solicitacaoService.AlterarSolicitacaoMensagem(solicitacao);
                    //_solicitacaoService.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Erro ao tentar salvar a Solicitação de Documentos. A solicitação não foi enviada!");
                    return View(model);
                }

                //se não for primeiro acesso enviar para tela de acesso
                string url = Url.Action("Index", "Home", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}&Login={1}&TravaLogin=1", model.SolicitacaoCriacaoID, model.Fornecedor.CNPJ), Key)
                }, Request.Url.Scheme);

                //se for primeiro acesso enviar para criação de usuário
                #region BuscarPorEmail
                //validar se o e-mail já existe na tabela de Usuarios
                if (!_usuarioBP.ValidarPorCnpj(model.Fornecedor.CNPJ))
                {
                    url = Url.Action("CadastrarUsuarioFornecedor", "Home", new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("Login={0}&SolicitacaoID={1}&Email={2}",
                        model.Fornecedor.CNPJ,
                        model.SolicitacaoCriacaoID,
                        model.Fornecedor.Contato.Email), Key)
                    }, Request.Url.Scheme);
                }
                #endregion
                model.Mensagem = string.Concat(model.Mensagem, "<p><a href='", url, "'>Link</a>:", url, "</p>");

                bool emailEnviadoComSucesso = _metodosGerais.EnviarEmail(model.Fornecedor.Contato.Email, model.Assunto, model.Mensagem);

                if (!emailEnviadoComSucesso)
                {
                    return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = "As tentativas de envio de e-mail falharam, favor reenviar a solicitação através da tela de acompanhamento." });
                }

                return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação Nº {0} enviada ao Fornecedor com sucesso!", model.SolicitacaoCriacaoID) });
            }

            return View(model);
        }

        [Authorize]
        public JsonResult AdicionarDocumento(int SolicitacaoId, int DescricaoDocumentoId)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int adicionado = 0;
            string msg = "";

            if (!_pjpfSolicitacaoDocumentosBP.DocumentoDuplicado(SolicitacaoId, DescricaoDocumentoId))
            {
                adicionado = _pjpfSolicitacaoDocumentosBP.AdicionaDocumentosSolicitacao(SolicitacaoId, DescricaoDocumentoId);
                if (adicionado != -1)
                    msg = "Documento adicionado com Sucesso!";
                else
                    msg = "Não foi possível Adicionar um novo documento a Solicitação!";
            }
            else
            {
                adicionado = -1;
                msg = "O Documento selecionado já existe na solicitação!";
            }

            var jsonClass = new List<JsonClass>()
            {
                new JsonClass{
                    value = adicionado.ToString(),
                    text = msg
                }
            };

            return Json(jsonClass, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult RemoverDocumento(int SolicitacaoId, int DescricaoDocumentoId)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int removido = 0;
            string msg = "";

            removido = _pjpfSolicitacaoDocumentosBP.RemoverDocumentosSolicitacao(SolicitacaoId, DescricaoDocumentoId);
            if (removido != -1)
                msg = "Documento removido com Sucesso!";
            else
                msg = "Não foi possível Remover o documento da Solicitação!";

            var jsonClass = new List<JsonClass>()
            {
                new JsonClass{
                    value = removido.ToString(),
                    text = msg
                }
            };

            return Json(jsonClass, JsonRequestBehavior.AllowGet);
        }

        #endregion SOLICITACAO DE DOCUMENTOS

        #region FICHA CADASTRAL
        [Authorize]
        [HttpGet]
        public ActionResult FichaCadastral(string chaveurl)
        {
            int solicitacaoId = 0;
            int fornecedorId = 0;
            int contratanteId = 0;

            try
            {
                FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
                ficha.ChaveUrl = chaveurl;

                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out solicitacaoId);
                    //Só serão usados no caso de Usuários fornecedores
                    Int32.TryParse(param.First(p => p.Name == "FornecedorID").Value, out fornecedorId);
                    Int32.TryParse(param.First(p => p.Name == "ContratanteID").Value, out contratanteId);

                    // Este parametro é utilizado quando o fornecedor atualiza algum documento e o retorno é sucesso para apresentar alerta.
                    if (param.Any(p => p.Name == "RetSucessoDocs"))
                        ViewBag.RetSucessoDocs = param.First(p => p.Name == "RetSucessoDocs").Value;

                    // Este parametro é utilizado quando o fornecedor atualiza algum comprovante e o retorno é sucesso para apresentar alerta.
                    if (param.Any(p => p.Name == "RetSucessoBancos"))
                        ViewBag.RetSucessoBancos = param.First(p => p.Name == "RetSucessoBancos").Value;

                }
                //SOLICITACAO
                #region Usuario De Solicitacao
                if (solicitacaoId > 0)
                {
                    var solicitacao = _solicitacaoService.BuscarPorIdDocumentosSolicitados(solicitacaoId);

                    bool aprovado = _solicitacaoTramiteService.SolicitacaoFornecedorFinalizou(solicitacaoId);
                    if (aprovado)
                        ViewBag.StatusTramite = 2;
                    else
                        ViewBag.StatusTramite = 1;

                    ficha.ChaveUrl = chaveurl;
                    ficha.PJPFID = fornecedorId;
                    ficha.HabilitaEdicao = true;
                    if (fornecedorId == 0 && contratanteId == 0)
                        ficha.HabilitaEdicao = false;
                    //Termo de aceite
                    ficha.TermoAceite = false;
                    ficha.TextoTermoAceite = "<p>Aqui estará o texto do termo de aceite.<br/>A linha debaixo deverá está assim</p>";

                    if (solicitacao != null)
                    {

                        PreencherFichaCadastral(solicitacao, ficha, 50);
                        #region Preenchimento Específico
                        ficha.SolicitacaoFornecedor.Solicitacao = true;
                        ficha.TipoPreenchimento = (int)EnumTiposPreenchimento.Fornecedor;
                        #endregion

                        PersistirDadosEmMemoria(solicitacao.Contratante, solicitacao);
                        PersistirDadosEnderecoEmMemoria();

                        ficha.PrazoEntrega = new PrazoEntregaVM(DateTime.Now);
                        return View(ficha);
                    }

                    return RedirectToAction("Alerta", "Home");
                } 
                #endregion
                //FORNECEDOR CADASTRADO
                else if (fornecedorId > 0)
                {
                    ViewBag.StatusTramite = 2; //Dado Obrigatório - Concluído
                    var fornecedores = _fornecedorService.BuscarPorIdCompleto(fornecedorId);
                    WFD_CONTRATANTE_PJPF contratantePJPF;

                    if (contratanteId == 0)
                        contratantePJPF = fornecedores.FirstOrDefault();
                    else
                        contratantePJPF = fornecedores.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId);

                    ficha = Mapper.Map<Fornecedor, FichaCadastralWebForLinkVM>(contratantePJPF.WFD_PJPF);
                    ficha.ActionOrigem = "FichaCadastral";
                    ficha.ControllerOrigem = "Documento";

                    ficha.ChaveUrl = chaveurl;
                    ficha.PJPFID = fornecedorId;
                    ficha.ID = fornecedorId;
                    ficha.ContratanteFornecedorID = contratantePJPF.ID;
                    ficha.SolicitacaoFornecedor = new SolicitacaoFornecedorVM();
                    ficha.HabilitaEdicao = true;
                    ficha.HabilitaEdicaoUnspsc = true;
                    ficha.CategoriaId = contratantePJPF.CATEGORIA_ID ?? 0;
                    ViewBag.DataAtuUnspsc = contratantePJPF.WFD_PJPF.DT_ATUALIZACAO_UNSPSC;

                    ficha.Contratantes = new List<ContratantesVM>();
                    ficha.Contratantes = Mapper.Map<List<Contratante>, List<ContratantesVM>>(fornecedores.Select(x => x.WFD_CONTRATANTE).ToList()).ToArray();

                    ficha.Contratantes.ForEach(x => x.ParamCripto = Cripto.Criptografar(String.Format("SolicitacaoID=0&FornecedorID={0}&ContratanteID={1}", fornecedorId, x.Id), Key));

                    var ContratanteSelecionado = ficha.Contratantes.FirstOrDefault(x => x.Id == contratanteId);
                    ViewBag.Contratantes = new SelectList(ficha.Contratantes, "ParamCripto", "RazaoSocial", ContratanteSelecionado != null ? ContratanteSelecionado.ParamCripto : "");

                    //Mapear Dados Bancários
                    var bancosFornecedor = contratantePJPF.BancoDoFornecedor.ToList();
                    var enderecosFornecedor = contratantePJPF.WFD_PJPF_ENDERECO.ToList();

                    ficha.DadosBancarios = bancosFornecedor.Any()
                        ? Mapper.Map<List<BancoDoFornecedor>, List<DadosBancariosVM>>(bancosFornecedor)
                        : new List<DadosBancariosVM> { new DadosBancariosVM { Bancos = ViewBag.Bancos } };

                    ficha.DadosEnderecos = enderecosFornecedor.Any()
                        ? Mapper.Map<List<FORNECEDOR_ENDERECO>, List<DadosEnderecosVM>>(enderecosFornecedor)
                        : new List<DadosEnderecosVM> { new DadosEnderecosVM { Endereco = ViewBag.Enderecos } };

                    //Mapear Dados Contatos
                    var contatosFornecedor = contratantePJPF.WFD_PJPF_CONTATOS.ToList();

                    ficha.DadosContatos = contatosFornecedor.Any()
                        ? Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(contatosFornecedor)
                        : new List<DadosContatoVM> { new DadosContatoVM() };

                    ficha.SolicitacaoFornecedor.Documentos = Mapper.Map<List<DocumentosDoFornecedor>, List<SolicitacaoDocumentosVM>>(contratantePJPF.WFD_PJPF_DOCUMENTOS.ToList());

                    ficha.FornecedoresUnspsc = Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(contratantePJPF.WFD_PJPF.FornecedorServicoMaterialList.Where(x => x.DT_EXCLUSAO == null).ToList());

                    #region Preenchimento Específico

                    ficha.SolicitacaoFornecedor.Solicitacao = true;
                    ficha.TipoPreenchimento = (int)EnumTiposPreenchimento.Fornecedor;

                    #endregion

                    Contratante contratante = contratantePJPF.WFD_CONTRATANTE;

                    ficha.ContratanteID = contratante.ID;

                    int papelAtual = _papelBP.BuscarPorContratanteETipoPapel(contratantePJPF.CONTRATANTE_ID, (int)EnumTiposPapel.Fornecedor).ID;


                    //Mapear Questionários
                    ficha.Questionarios = new RetornoQuestionario<QuestionarioVM>
                    {
                        QuestionarioDinamicoList =
                        Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                            _cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                            {
                                UF = "RJ",
                                ContratanteId = contratante.ID,
                                PapelId = papelAtual,
                                CategoriaId = ficha.CategoriaId,
                                Alteracao = true,
                                SolicitacaoId = 0
                            })
                            )
                    };

                    PersistirDadosEmMemoria(contratante, contratantePJPF.WFD_PJPF);

                    ViewBag.EhFornecedor = true;
                    return View(ficha);
                }

                return RedirectToAction("Alerta", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ViewBag.ChaveConferida = true;
            }

            return View();
        }

        private void PreencherFichaCadastral(object solicitacao, FichaCadastralWebForLinkVM ficha, int v)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FichaCadastral(FichaCadastralWebForLinkVM model, string estilo, string ServicosSelecionados, string MateriaisSelecionados)
        {
            ViewBag.Estilo = estilo;

            PersistirDadosEmMemoria();

            model.SolicitacaoFornecedor.Solicitacao = true;
            if (model.SolicitacaoFornecedor.SolicitacaoCriacaoID.HasValue)
                model.Solicitacao.ID = model.SolicitacaoFornecedor.SolicitacaoCriacaoID.Value;

            //UNSPSC
            model.FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            FornecedorUnspscVM unspscVM = new FornecedorUnspscVM();

            var unspscs = _unspscBP.BuscarListaPorID(ServicosSelecionados.Split(new Char[] { '|' }), MateriaisSelecionados.Split(new Char[] { '|' }));

            model.FornecedoresUnspsc = unspscVM.PreencheModelUnspsc(model.PJPFID, model.SolicitacaoID, unspscs);

            FinalizarFichaCadastral(model);

            if (model.ApenasSalvar)
                ModelState.Clear();

            return View(model);
        }
        #endregion

        #region LISTA DE DOCUMENTOS

        [Authorize]
        public ActionResult ListaDocumentosLst(int? Pagina, int? CategoriaSelecionada, string CategoriaSelecionadaNome, int?[] ListaDocumentoId, string removerDoc, string Acao, string MensagemSucesso)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            int pagina = Pagina ?? 1;
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;

            ViewBag.CategoriaSelecionada = CategoriaSelecionada;
            ViewBag.CategoriaSelecionadaNome = CategoriaSelecionadaNome;

            //BUSCA LISTA DE DOCUMENTOS E MONTA PAGINAÇÃO
            int totalRegistro = Db.WFD_PJPF_LISTA_DOCUMENTOS.Count(d => d.CONTRATANTE_ID == contratanteId);
            List<ListaDeDocumentosDeFornecedor> listaDocumentos = Db.WFD_PJPF_LISTA_DOCUMENTOS
                .Include(x => x.DescricaoDeDocumentos.TipoDeDocumento)
                //.Include("PeriodicidadeDoDocumento")
                .Where(d => d.CONTRATANTE_ID == contratanteId)
                .OrderBy(d => d.ID)
                .Skip(TamanhoPagina * (pagina - 1))
                .Take(TamanhoPagina)
                .ToList();

            int totalPaginas = (int)Math.Ceiling(totalRegistro / (double)TamanhoPagina);
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.TotalRegistros = totalRegistro;

            List<ListaDocumentosVM> listaDocumentosVM = ListaDocumentosVM.ModelToViewModel(listaDocumentos, Url);

            List<FORNECEDOR_CATEGORIA> categorias = Db.WFD_PJPF_CATEGORIA
                .Include("WFD_PJPF_CATEGORIA1")
                .Where(c => c.CONTRATANTE_ID == contratanteId && c.CATEGORIA_PAI_ID == null)
                .OrderBy(c => c.DESCRICAO)
                .ToList();

            List<CategoriaVM> modelo = CategoriaVM.ModelToViewModel(categorias, Url);
            ViewBag.Categorias = modelo;

            if (Acao == "Adicionar")
            {
                if (CategoriaSelecionada != null)
                {
                    if (ListaDocumentoId != null)
                    {
                        AdicionarCategoriaAListaDocumentos(CategoriaSelecionada, ListaDocumentoId, listaDocumentos);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Selecione ao menos um documento para adicionar a categoria selecionada!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Informe a Categoria!");
                }
            }
            else if (!string.IsNullOrEmpty(removerDoc))
            {
                string[] rem = removerDoc.Split(new char[] { '|' });
                int catId = Convert.ToInt32(rem[0]);
                int listaId = Convert.ToInt32(rem[1]);
                FORNECEDOR_CATEGORIA cat = Db.WFD_PJPF_CATEGORIA.Include(x => x.ListaDeDocumentosDeFornecedor).Single(c => c.ID == catId);
                cat.ListaDeDocumentosDeFornecedor.Remove(cat.ListaDeDocumentosDeFornecedor.Single(d => d.ID == listaId));
                Db.SaveChanges();
            }

            List<FORNECEDOR_CATEGORIA> categoriasDoc = Db.WFD_PJPF_CATEGORIA
                .Include("ListaDeDocumentosDeFornecedor.DescricaoDeDocumentos")
                .Where(c => c.CONTRATANTE_ID == contratanteId && c.ListaDeDocumentosDeFornecedor.Any())
                .OrderBy(c => c.DESCRICAO).ToList();
            ViewBag.CategoriasComDoc = categoriasDoc.ToList();

            return View(listaDocumentosVM);
        }

        [Authorize]
        public ActionResult ListaDocumentosFrm(string chaveurl)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int id = 0;
            string acao = "";
            int tipoDocumento = 0;
            int descricaoDocumento = 0;

            if (!string.IsNullOrEmpty(chaveurl))
            {

                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
                acao = param.First(p => p.Name == "Acao").Value;
            }
            ListaDocumentosVM listaDocumentosVM = new ListaDocumentosVM();

            // INCLUSAO
            if (id == 0)
            {
                listaDocumentosVM.Ativo = true;
                listaDocumentosVM.Periodicidade = 1;
            }
            else
            {
                ViewBag.Acao = acao;

                ListaDeDocumentosDeFornecedor ld = Db.WFD_PJPF_LISTA_DOCUMENTOS
                    .Include("DescricaoDeDocumentos.TipoDeDocumento")
                    .FirstOrDefault(c => c.CONTRATANTE_ID == contratanteId && c.ID == id);
                listaDocumentosVM.ID = ld.ID;
                listaDocumentosVM.ExigeValidade = ld.EXIGE_VALIDADE;
                listaDocumentosVM.Periodicidade = ld.PERIODICIDADE_ID;

                if (listaDocumentosVM.ExigeValidade)
                    listaDocumentosVM.TipoAtualizacao = 2;
                else if (listaDocumentosVM.Periodicidade != null)
                    listaDocumentosVM.TipoAtualizacao = 3;
                else
                    listaDocumentosVM.TipoAtualizacao = 1;

                listaDocumentosVM.Ativo = ld.ATIVO;
                listaDocumentosVM.Obrigatorio = ld.OBRIGATORIO;

                tipoDocumento = ld.DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID;
                descricaoDocumento = ld.DescricaoDeDocumentos.ID;
            }

            ViewBag.TipoDocumentos = new SelectList(Db.WFD_TIPO_DOCUMENTOS.Where(e => e.ATIVO && e.CONTRATANTE_ID == contratanteId).OrderBy(e => e.DESCRICAO), "ID", "DESCRICAO", tipoDocumento);
            ViewBag.DescricaoDocumentos = new SelectList(MontarDescricaoDocumento(tipoDocumento, "Frm"), "Value", "Text", descricaoDocumento);
            ViewBag.Periodicidade = new SelectList(Db.WFD_TIPO_PERIODICIDADE.OrderBy(x => x.ID).ToList(), "ID", "PERIODICIDADE_NM", listaDocumentosVM.Periodicidade);

            return View(listaDocumentosVM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ListaDocumentosFrm(ListaDocumentosVM model, int? TipoDocumentos, int? DescricaoDocumentos, int? TipoAtualizacao, string Acao)
        {
            if (TipoDocumentos == null)
            {
                ModelState.AddModelError("TipoDocumento", "Tipo de documento obrigatório");
            }
            if (DescricaoDocumentos == null)
            {
                ModelState.AddModelError("DescricaoDocumento", "Descricao do documento obrigatório");
            }
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            ViewBag.TipoDocumentos = new SelectList(Db.WFD_TIPO_DOCUMENTOS.Where(e => e.ATIVO == true && e.CONTRATANTE_ID == ContratanteId).OrderBy(e => e.DESCRICAO), "ID", "DESCRICAO", TipoDocumentos);
            ViewBag.DescricaoDocumentos = new SelectList(MontaDescricaoDocumento(TipoDocumentos, "Frm"), "Value", "Text", DescricaoDocumentos);
            ViewBag.Periodicidade = new SelectList(Db.WFD_TIPO_PERIODICIDADE.OrderBy(x => x.ID).ToList(), "ID", "PERIODICIDADE_NM", model.Periodicidade);

            if (Acao == "Excluir")
            {
                ModelState.Remove("Descricao");
            }

            if (ModelState.IsValid)
            {
                ListaDeDocumentosDeFornecedor ld;
                //INCLUSÃO DE DOCUMENTO
                if (model.ID == 0)
                {
                    ld = new ListaDeDocumentosDeFornecedor();
                    ld.CONTRATANTE_ID = ContratanteId;

                    if (TipoAtualizacao == 2)
                        ld.EXIGE_VALIDADE = true;
                    else if (TipoAtualizacao == 3)
                        ld.PERIODICIDADE_ID = model.Periodicidade;

                    ld.ATIVO = model.Ativo;
                    ld.DESCRICAO_DOCUMENTO_ID = (int)DescricaoDocumentos;
                    ld.OBRIGATORIO = model.Obrigatorio;
                    Db.Entry(ld).State = System.Data.Entity.EntityState.Added;
                    Db.SaveChanges();

                    return RedirectToAction("ListaDocumentosLst", "Documento", new { MensagemSucesso = "Inclusão realizada com sucesso!" });
                }
                else if (Acao == "Alterar")
                {
                    ld = Db.WFD_PJPF_LISTA_DOCUMENTOS.FirstOrDefault(d => d.CONTRATANTE_ID == ContratanteId && d.ID == model.ID);

                    if (ld != null)
                    {
                        ld.ATIVO = model.Ativo;
                        ld.EXIGE_VALIDADE = model.ExigeValidade;
                        ld.OBRIGATORIO = model.Obrigatorio;
                        ld.DESCRICAO_DOCUMENTO_ID = (int)DescricaoDocumentos;

                        if (TipoAtualizacao == 2)
                        {
                            ld.EXIGE_VALIDADE = model.ExigeValidade;
                            ld.PERIODICIDADE_ID = null;
                        }
                        else if (TipoAtualizacao == 3)
                        {
                            ld.EXIGE_VALIDADE = false;
                            ld.PERIODICIDADE_ID = model.Periodicidade;
                        }
                        else
                        {
                            ld.EXIGE_VALIDADE = false;
                            ld.PERIODICIDADE_ID = null;
                        }

                        Db.Entry(ld).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();

                        return RedirectToAction("ListaDocumentosLst", "Documento", new { MensagemSucesso = "Alteração realizada com sucesso!" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro ao tentar alterar o documento, identificador não encontrado!");
                    }
                }
                if (Acao == "Excluir")
                {

                    ld = Db.WFD_PJPF_LISTA_DOCUMENTOS.FirstOrDefault(d => d.CONTRATANTE_ID == ContratanteId && d.ID == model.ID);
                    Db.Entry(ld).State = System.Data.Entity.EntityState.Deleted;
                    Db.SaveChanges();

                    return RedirectToAction("ListaDocumentosLst", "Documento", new { MensagemSucesso = "Exclusão realizada com sucesso!" });
                }
            }

            return View(model);
        }

        #endregion

        public JsonResult BuscaServicosPorChave(string chave)
        {
            if (Session["GrupoServico1"] == null)
                Session["GrupoServico1"] = _unspscBP.BuscarServicoGrupo1();

            if (Session["GrupoServico2"] == null)
                Session["GrupoServico2"] = _unspscBP.BuscarServicoGrupo2();

            if (Session["GrupoServico3"] == null)
                Session["GrupoServico3"] = _unspscBP.BuscarServicoGrupo3();


            var unspscs = _unspscBP.BuscarServicoPorDescricao(chave, (List<TIPO_UNSPSC>)Session["GrupoServico1"], (List<TIPO_UNSPSC>)Session["GrupoServico2"], (List<TIPO_UNSPSC>)Session["GrupoServico3"]);
            List<JsonClassM> servicos = new List<JsonClassM>();
            unspscs.ForEach(x =>
                servicos.Add(new JsonClassM
                {
                    text = x.UNSPSC_DSC,
                    value1 = x.ID.ToString(),
                    value2 = x.UNSPSC_COD.ToString(),
                    value3 = x.NIV.ToString(),
                })
            );

            return Json(servicos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaMateriaisPorChave(string chave)
        {
            if (Session["GrupoMaterial1"] == null)
                Session["GrupoMaterial1"] = _unspscBP.BuscarMaterialGrupo1();

            if (Session["GrupoMaterial2"] == null)
                Session["GrupoMaterial2"] = _unspscBP.BuscarMaterialGrupo2();

            if (Session["GrupoMaterial3"] == null)
                Session["GrupoMaterial3"] = _unspscBP.BuscarMaterialGrupo3();


            var unspscs = _unspscBP.BuscarMaterialPorDescricao(chave, (List<TIPO_UNSPSC>)Session["GrupoMaterial1"], (List<TIPO_UNSPSC>)Session["GrupoMaterial2"], (List<TIPO_UNSPSC>)Session["GrupoMaterial3"]);
            List<JsonClassM> materiais = new List<JsonClassM>();
            unspscs.ForEach(x =>
                materiais.Add(new JsonClassM
                {
                    text = x.UNSPSC_DSC,
                    value1 = x.ID.ToString(),
                    value2 = x.UNSPSC_COD.ToString(),
                    value3 = x.NIV.ToString(),
                })
            );

            return Json(materiais, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaServicos(int Codigo, int Niv)
        {
            var unspscs = _unspscBP.BuscarServico(Codigo, Niv);
            List<JsonClassM> servicos = new List<JsonClassM>();
            unspscs.ForEach(x =>
                servicos.Add(new JsonClassM
                {
                    text = x.UNSPSC_DSC,
                    value1 = x.ID.ToString(),
                    value2 = x.UNSPSC_COD.ToString(),
                    value3 = x.NIV.ToString(),
                })
            );

            return Json(servicos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaMateriais(int Codigo, int Niv)
        {
            var unspscs = _unspscBP.BuscarMaterial(Codigo, Niv);
            List<JsonClassM> materiais = new List<JsonClassM>();
            unspscs.ForEach(x =>
                materiais.Add(new JsonClassM
                {
                    text = x.UNSPSC_DSC,
                    value1 = x.ID.ToString(),
                    value2 = x.UNSPSC_COD.ToString(),
                    value3 = x.NIV.ToString(),
                })
            );

            return Json(materiais, JsonRequestBehavior.AllowGet);
        }

        #region  Compartilhados

        private void AdicionarCategoriaAListaDocumentos(int? CategoriaSelecionada, int?[] ListaDocumentoId, List<ListaDeDocumentosDeFornecedor> listaDocumentos)
        {
            bool adicionou = false;
            //using (var bpCategoria = new PJPFCategoriaBP())
            //{
            //    bpCategoria.AdicionarCategoriaAListaDocumentos(ListaDocumentoId, (int)CategoriaSelecionada, listaDocumentos);

            //}
            foreach (int docID in ListaDocumentoId)
            {
                FORNECEDOR_CATEGORIA cat = Db.WFD_PJPF_CATEGORIA.Include(x => x.ListaDeDocumentosDeFornecedor).Single(c => c.ID == (int)CategoriaSelecionada);
                if (cat.ListaDeDocumentosDeFornecedor.All(d => d.ID != docID))
                {
                    cat.ListaDeDocumentosDeFornecedor.Add(listaDocumentos.Single(d => d.ID == docID));
                    adicionou = true;
                }
            }

            if (adicionou)
            {
                Db.SaveChanges();
                ViewBag.CategoriaSelecionada = null;
                ViewBag.CategoriaSelecionadaNome = null;
            }
        }

        private void PersistirDadosEmMemoria()
        {
            PersistirDadosEmMemoria(new Contratante(), new SOLICITACAO());
        }

        private void PersistirDadosEnderecoEmMemoria()
        {
            if (TempData["UF"] == null)
                TempData["UF"] = new SelectList(_enderecoBP.ListarTodosPorNome(), "UF_SGL", "UF_NM");

            ViewBag.UF = TempData["UF"] as SelectList;
            TempData.Keep("UF");

            if (TempData["TipoEndereco"] == null)
                TempData["TipoEndereco"] = new SelectList(_enderecoBP.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");

            ViewBag.TipoEndereco = TempData["TipoEndereco"] as SelectList;
            TempData.Keep("TipoEndereco");
        }

        private void PersistirDadosEmMemoria(Contratante contratante, SOLICITACAO solicitacao)
        {
            //ViewBag.Nome
            if (TempData["Nome"] == null)
                TempData["Nome"] = contratante.RAZAO_SOCIAL;

            ViewBag.Nome = TempData["Nome"] as String;
            TempData.Keep("Nome");

            //ViewBag.Estilo
            if (TempData["Estilo"] == null)
                TempData["Estilo"] = contratante.ESTILO;

            ViewBag.Estilo = TempData["Estilo"] as String;
            TempData.Keep("Estilo");

            //ViewBag.SolicitaDocumentos
            if (TempData["SolicitaDocumentos"] == null)
                TempData["SolicitaDocumentos"] = contratante.WFD_CONTRATANTE_CONFIG.SOLICITA_DOCS;

            ViewBag.SolicitaDocumentos = (bool)TempData["SolicitaDocumentos"];
            TempData.Keep("SolicitaDocumentos");

            //ViewBag.SolicitaFichaCadastral
            if (TempData["SolicitaFichaCadastral"] == null)
                TempData["SolicitaFichaCadastral"] = contratante.WFD_CONTRATANTE_CONFIG.SOLICITA_FICHA_CAD;

            ViewBag.SolicitaFichaCadastral = (bool)TempData["SolicitaFichaCadastral"];
            TempData.Keep("SolicitaFichaCadastral");

            //ViewBag.Imagem
            if (TempData["Imagem"] == null)
            {
                var caminhoFisico = Server.MapPath("/ImagensUsuarios");
                var arquivo = string.Concat("ImagemContratante", contratante.ID, ".png");

                if (System.IO.File.Exists(string.Concat(caminhoFisico, "/", arquivo)))
                    TempData["Imagem"] = arquivo;
                else
                    TempData["Imagem"] = ((int)contratante.TIPO_CADASTRO_ID == (int)EnumTiposCadastro.Cliente) ? "semfoto.png" : "semlogo.png";
            }

            ViewBag.Imagem = TempData["Imagem"] as String;
            TempData.Keep("Imagem");

            //ViewBag.Bancos
            if (TempData["Bancos"] == null)
                TempData["Bancos"] = new SelectList(_bancoBP.ListarTodosPorNome().ToList(), "ID", "BANCO_NM");

            ViewBag.Bancos = TempData["Bancos"] as SelectList;
            TempData.Keep("Bancos");
        }

        private void PersistirDadosEmMemoria(Contratante contratante, Fornecedor fornecedor)
        {
            ViewBag.Nome = contratante.RAZAO_SOCIAL;
            ViewBag.Estilo = contratante.ESTILO;
            ViewBag.SolicitaDocumentos = contratante.WFD_CONTRATANTE_CONFIG.SOLICITA_DOCS;
            ViewBag.SolicitaFichaCadastral = contratante.WFD_CONTRATANTE_CONFIG.SOLICITA_FICHA_CAD;

            var caminhoFisico = Server.MapPath("/ImagensUsuarios");
            var arquivo = string.Concat("ImagemContratante", contratante.ID, ".png");
            if (System.IO.File.Exists(string.Concat(caminhoFisico, "/", arquivo)))
                ViewBag.Imagem = arquivo;
            else
                ViewBag.Imagem = ((int)contratante.TIPO_CADASTRO_ID == (int)EnumTiposCadastro.Cliente) ? "semfoto.png" : "semlogo.png";

            ViewBag.Bancos = new SelectList(_bancoBP.ListarTodosPorNome().ToList(), "ID", "BANCO_NM");
        }


        public List<SelectListItem> MontaTipoDocumento(string Tipo)
        {
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<TipoDeDocumento> tipoDocumentos = Db.WFD_TIPO_DOCUMENTOS.Where(e => e.ATIVO == true && e.CONTRATANTE_ID == ContratanteId).OrderBy(e => e.DESCRICAO).ToList();

            if (Tipo == "Lst")
                listDocumentos.Add(new SelectListItem() { Text = "Todos", Value = "" });
            else
                listDocumentos.Add(new SelectListItem() { Text = "Selecione...", Value = "" });

            foreach (TipoDeDocumento td in tipoDocumentos)
            {
                listDocumentos.Add(new SelectListItem() { Text = td.DESCRICAO, Value = td.ID.ToString() });
            }

            return listDocumentos;
        }

        public List<SelectListItem> MontaDescricaoDocumento(int? TipoDocumento, string Tipo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            TipoDocumento = (TipoDocumento != null) ? (int)TipoDocumento : 0;

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<DescricaoDeDocumentos> descricaoDocumentos = Db.WFD_DESCRICAO_DOCUMENTOS
                .Where(e => e.ATIVO == true &&
                    e.CONTRATANTE_ID == contratanteId &&
                    e.TIPO_DOCUMENTOS_ID == TipoDocumento)
                .OrderBy(e => e.DESCRICAO)
                .ToList();

            if (Tipo == "Lst")
                listDocumentos.Add(new SelectListItem() { Text = "Todos", Value = "" });
            else
                listDocumentos.Add(new SelectListItem() { Text = "Selecione...", Value = "" });

            descricaoDocumentos.ForEach(dd =>
            {
                listDocumentos.Add(new SelectListItem() { Text = dd.DESCRICAO, Value = dd.ID.ToString() });
            });

            return listDocumentos;
        }

        private string DescricaoTipoAtualizacao(ListaDeDocumentosDeFornecedor ld)
        {
            if (ld.EXIGE_VALIDADE == false && ld.PERIODICIDADE_ID == null)
                return "Sem Atualização";
            else if (ld.EXIGE_VALIDADE == true && ld.PERIODICIDADE_ID == null)
                return "Por Validade";
            else if (ld.EXIGE_VALIDADE == false && ld.PERIODICIDADE_ID != null)
                return "Por Período (" + ld.WFD_T_PERIODICIDADE.PERIODICIDADE_NM + ")";
            else
                return null;
        }
        #endregion

        public SolicitacaoFornecedorVM PopularSolicitacaoCadastroPJPF(int contratanteId, SolicitacaoFornecedorVM modelo)
        {
            var usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            var solicitacao = _solicitacaoService.BuscarPorIdComDocumentos((int)modelo.SolicitacaoCriacaoID);
            var solforn = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();
            //var solicitacaoCadastroPJPF = SolicitacaoCadastroPJPFBP.BuscarPorSolicitacaoIDComDocumentos((int)solicitacaoFornecedorVM.SolicitacaoCriacaoID);
            var configEmail = _contratanteConfiguracaoEmailBP.BuscarPorContratanteETipo(contratanteId, 1);
            var usuario = Db.WFD_USUARIO.Single(x => x.ID == usuarioId);
            var contratante = Db.Contratante.Single(x => x.ID == contratanteId);

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

        public void PreencherFichaCadastral(SOLICITACAO solicitacao, FichaCadastralWebForLinkVM ficha, int TpPapel)
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

            var papel = _papelBP.BuscarPorContratanteETipoPapel(contratante.ID, TpPapel).ID;

            //Mapear Questionários
            ficha.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                    _cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
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
            ficha.ProrrogacaoPrazo.PrazoPreenchimento = _contratanteConfiguracaoBP.BuscarPrazo(solicitacao);
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

        public bool FinalizarFichaCadastral(FichaCadastralWebForLinkVM model)
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

            #region Validar PjPf Categoria
            FORNECEDOR_CATEGORIA categoria = _fornecedorCategoriaService.Buscar(x => x.ID == (int)model.CategoriaId);

            if (model.TipoFornecedor != 1)
            {
                if (model.DadosEnderecos == null || !model.DadosEnderecos.Any())
                {
                    ModelState.AddModelError("DadosEnderecoValidation", "Informar ao menos um Endereço!");
                }
                else if (model.DadosEnderecos.Any(x => x.TipoEnderecoId == 0 || String.IsNullOrEmpty(x.Endereco) || String.IsNullOrEmpty(x.Numero) || String.IsNullOrEmpty(x.CEP)))
                {
                    ModelState.AddModelError("DadosEnderecoValidation", "Dados incompletos no Endereço!");
                }
            }

            if (!categoria.ISENTO_DADOSBANCARIOS && !model.ApenasSalvar)
            {
                if (model.DadosBancarios == null || !model.DadosBancarios.Any())
                {
                    ModelState.AddModelError("DadosBancariosValidation", "Informar ao menos um Dado Bancário!");
                    model.DadosBancarios = new List<DadosBancariosVM>();
                    model.DadosBancarios.Add(new DadosBancariosVM());
                }
            }
            else
            {
                //REMOVE AS CRITICAS DOS DADOS BANCARIOS CASO A CATEGORIA SEJA ISENTA
                while (ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Banco")).Value != null)
                    ModelState.Remove(ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Banco")));

                while (ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Agencia")).Value != null)
                    ModelState.Remove(ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Agencia")));

                while (ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrente")).Value != null)
                    ModelState.Remove(ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrente")));

                while (ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrenteDigito")).Value != null)
                    ModelState.Remove(ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrenteDigito")));
            }

            if (!categoria.ISENTO_CONTATOS && !model.ApenasSalvar)
            {
                if (model.DadosContatos == null || !model.DadosContatos.Any())
                {
                    ModelState.AddModelError("DadosContatosValidation", "Informar os Dados do Contato!");
                    model.DadosContatos = new List<DadosContatoVM>();
                    model.DadosContatos.Add(new DadosContatoVM());
                }
            }
            else
            {
                //REMOVE AS CRITICAS DOS DADOS DE CONTATOS CASO A CATEGORIA SEJA ISENTA
                while (ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosContatos") && ms.Key.Contains("EmailContato")).Value != null)
                    ModelState.Remove(ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosContatos") && ms.Key.Contains("EmailContato")));
            }

            if (!categoria.ISENTO_DOCUMENTOS && !model.ApenasSalvar && model.TipoFornecedor != 2)
            {
                List<SolicitacaoDocumentosVM> docsObrigatorios = model.SolicitacaoFornecedor.Documentos.Where(x => x.Obrigatorio == true && x.ArquivoSubido == null).ToList();
                if (docsObrigatorios.Any())
                {
                    ModelState.AddModelError("AnexosValidation", "Favor subir os arquivos dos documentos Exigíveis!");
                }
            }

            ModelState.Remove("SolicitacaoFornecedor.Assunto");
            ModelState.Remove("SolicitacaoFornecedor.DescricaoSolicitacao");
            ModelState.Remove("SolicitacaoFornecedor.Mensagem");
            ModelState.Remove("SolicitacaoFornecedor.DadosEnderecos.T_UF.UF_NM");

            int tipoPapel = model.TipoPreenchimento.Equals((int)EnumTiposPreenchimento.Fornecedor) ? (int)EnumTiposPapel.Fornecedor : (int)EnumTiposPapel.Solicitante;
            int papelAtual = _papelBP.BuscarPorContratanteETipoPapel(model.ContratanteID, tipoPapel).ID;
            foreach (var modeloErrado in ModelState.Values)
            {
                if (modeloErrado.Errors.Count != 0)
                {

                }
            }
            #endregion

            if (model.DadosEnderecos == null)
                model.DadosEnderecos = new List<DadosEnderecosVM>();

            if (ModelState.IsValid)
            {
                try
                {
                    SolicitacaoCadastroFornecedor solicitacaoCadastroPJPF = _solicitacaoCadastroFornecedorService.BuscarPorSolicitacaoId(model.Solicitacao.ID);
                    CompletarSolicitacaoCadastroPJPF(ref solicitacaoCadastroPJPF, model);
                    _solicitacaoCadastroFornecedorService.AtualizarSolicitacao(solicitacaoCadastroPJPF);

                    if (model.DadosBancarios != null)
                        ManterDadosBancarios(model.DadosBancarios.Where(w => w.Preenchido(w)).ToList(), model.Solicitacao.ID, model.ContratanteID);
                    else
                        model.DadosBancarios = new List<DadosBancariosVM>();

                    if (model.DadosContatos != null)
                        ManterDadosContatos(model.DadosContatos.Where(w => w.EmailContato != null).ToList(), solicitacaoCadastroPJPF.SOLICITACAO_ID);
                    else
                        model.DadosContatos = new List<DadosContatoVM>();

                    if (model.DadosEnderecos != null)
                        ManterDadosEnderecos(model.DadosEnderecos.Where(x => x.TipoEnderecoId > 0).ToList(), solicitacaoCadastroPJPF.SOLICITACAO_ID);
                    else
                        model.DadosEnderecos = new List<DadosEnderecosVM>();

                    ManterUnspsc(model.FornecedoresUnspsc.ToList(), model.Solicitacao.ID);

                    if (model.TipoFornecedor != (int)EnumTiposFornecedor.EmpresaEstrangeira)
                        ManterDocumentos(model.SolicitacaoFornecedor.Documentos, model.Solicitacao.ID, model.ContratanteID);

                    _informacaoComplementarBP.InsertAll(informacoesComplementar);

                    model.ProrrogacaoPrazo = new ProrrogacaoPrazoVM();

                    if (solicitacaoCadastroPJPF.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.Any())
                        //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                        model.ProrrogacaoPrazo = Mapper.Map<ProrrogacaoPrazoVM>(solicitacaoCadastroPJPF
                            .WFD_SOLICITACAO
                            .WFD_SOLICITACAO_PRORROGACAO
                            .OrderBy(o => o.ID)
                            .LastOrDefault());

                    model.ProrrogacaoPrazo.PrazoPreenchimento = _contratanteConfiguracaoBP.BuscarPrazo(solicitacaoCadastroPJPF.WFD_SOLICITACAO);

                    if (model.ProrrogacaoPrazo.Aprovado != null)
                        if ((bool)model.ProrrogacaoPrazo.Aprovado)
                            model.ProrrogacaoPrazo.Status = "Aprovado";
                        else
                            model.ProrrogacaoPrazo.Status = "Reprovado";
                    else
                        model.ProrrogacaoPrazo.Status = "Aguardando Aprovação...";


                    if (!model.ApenasSalvar)
                    {
                        _solicitacaoService.Dispose(); //.Repositorio.Finalizar();
                        int usuarioId = Geral.PegaAuthTicket("UsuarioId") != null ? (int)Geral.PegaAuthTicket("UsuarioId") : 0;
                        _tramite.AtualizarTramite(model.ContratanteID, model.Solicitacao.ID, model.Solicitacao.Fluxo.ID, papelAtual, (int)EnumStatusTramite.Aprovado, usuarioId);

                        ViewBag.MensagemSucesso = "Dados Enviados com Sucesso!";
                        ViewBag.StatusTramite = (int)EnumStatusTramite.Aprovado;
                    }
                    else
                    {
                        _solicitacaoService.Dispose();// .Repositorio.Finalizar();
                        ViewBag.MensagemSucesso = "Dados Salvos com Sucesso!";
                        ViewBag.StatusTramite = (int)EnumStatusTramite.Aguardando;
                    }
                    preenchimentoValido = true;
                }
                catch (Exception e)
                {
                    ViewBag.MensagemErro = "Erro ao tentar Salvar a ficha cadastral!";
                    ViewBag.StatusTramite = (int)EnumStatusTramite.Aguardando;
                    Log.Error(e);
                }
            }
            else
            {
                ViewBag.MensagemErro = "Não foi possível enviar a Ficha Cadastral! Existem dados incompletos abaixo.";
                ViewBag.StatusTramite = (int)EnumStatusTramite.Aguardando;
            }

            model.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
            Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                _cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
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

            PersistirDadosEnderecoEmMemoria();

            return preenchimentoValido;
        }

        private void ManterDadosBancarios(List<DadosBancariosVM> dadosBancarios, int solicitacaoCriacaoID, int contratanteID)
        {
            foreach (var item in dadosBancarios)
            {
                item.SolicitacaoID = solicitacaoCriacaoID;
                item.ContratanteID = contratanteID;

                var arquivoId = 0;
                if (string.IsNullOrEmpty(item.NomeArquivo))
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(contratanteID, item.ArquivoSubido, item.TipoArquivoSubido);
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = _fornecedorArquivoService.SubstituirArquivoSolicitacaoBancario(contratanteID, (int)item.BancoSolicitacaoID, (int)item.ArquivoID, item.ArquivoSubido, item.TipoArquivoSubido, item.NomeArquivo);
                }


                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.ArquivoID = arquivoId;
                    item.NomeArquivo = _fornecedorArquivoService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
            }

            List<SolicitacaoModificacaoDadosBancario> solicitacoesModBancoMapeadas = Mapper.Map<List<DadosBancariosVM>, List<SolicitacaoModificacaoDadosBancario>>(dadosBancarios);

            _solicitacaoModificacaoBancoBP.ManterBancoCadastroFornecedor(solicitacoesModBancoMapeadas, solicitacaoCriacaoID);
        }

        private void ManterDadosContatos(List<DadosContatoVM> dadosContatos, int solicitacaoCriacaoID)
        {
            List<SolicitacaoModificacaoDadosContato> solicitacoesModContatoMapeadas = Mapper.Map<List<DadosContatoVM>, List<SolicitacaoModificacaoDadosContato>>(dadosContatos)
                .Select(x =>
                {
                    x.SOLICITACAO_ID = solicitacaoCriacaoID;
                    return x;
                }).ToList();

            _solicitacaoModificacaoContatoBP.ManterContatoCadastroFornecedor(solicitacoesModContatoMapeadas, solicitacaoCriacaoID);
        }

        private void ManterDadosEnderecos(List<DadosEnderecosVM> dadosEnderecos, int solicitacaoCriacaoID)
        {
            var solicitacoesModEndereco = _solicitacaoModificacaoEnderecoBP.ListarPorSolicitacaoId(solicitacaoCriacaoID).ToList();
            var solicitacoesModEnderecoPostadas = dadosEnderecos.Select(x => x.ID).ToArray();
            var solicitacoesModContatoExcluidas = solicitacoesModEndereco.Where(x => !solicitacoesModEnderecoPostadas.Contains(x.ID)).ToList();

            _solicitacaoModificacaoEnderecoBP.ExcluirSolicitacoes(solicitacoesModContatoExcluidas);

            List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoesModEnderecoMapeadas = Mapper.Map<List<DadosEnderecosVM>, List<SOLICITACAO_MODIFICACAO_ENDERECO>>(dadosEnderecos)
                .Select(x =>
                {
                    x.SOLICITACAO_ID = solicitacaoCriacaoID;
                    return x;
                }).ToList();
            _solicitacaoModificacaoEnderecoBP.InserirOuAtualizarSolicitacoes(solicitacoesModEnderecoMapeadas);
        }

        private void ManterDocumentos(List<SolicitacaoDocumentosVM> solicitacoesDocumentosVM, int solicitacaoCriacaoID, int contratanteId)
        {
            var solicitacoesDocumentos = _pjpfSolicitacaoDocumentosBP.ListarPorSolicitacaoId(solicitacaoCriacaoID);

            foreach (var item in solicitacoesDocumentosVM)
            {
                var arquivoId = 0;

                var solicitacaoDocumentos = solicitacoesDocumentos.FirstOrDefault(x => x.ID == item.ID);
                if (string.IsNullOrEmpty(item.NomeArquivo))
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(contratanteId, item.ArquivoSubido, item.TipoArquivoSubido);
                        solicitacaoDocumentos.ARQUIVO_ID = arquivoId;
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        arquivoId = _fornecedorArquivoService.SubstituirArquivoSolicitacaoDocumento(contratanteId, item.ID, (int)item.ArquivoID, item.ArquivoSubido, item.TipoArquivoSubido, item.NomeArquivo);
                        solicitacaoDocumentos.ARQUIVO_ID = arquivoId;
                    }
                }

                if (item.Periodicidade != null)
                {
                    if (item.Periodicidade > 0)
                        solicitacaoDocumentos.DATA_VENCIMENTO = CalculaDataVencimento((int)item.Periodicidade);
                }
                else
                {
                    if (item.PorValidade != null)
                        if ((bool)item.PorValidade)
                            solicitacaoDocumentos.DATA_VENCIMENTO = item.DataValidade;
                }

                _pjpfSolicitacaoDocumentosBP.AtualizarSolicitacao(solicitacaoDocumentos);

                item.ID = solicitacaoDocumentos.ID;
                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.NomeArquivo = _fornecedorArquivoService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoID = arquivoId;
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
                item.SolicitacaoID = (int)solicitacaoDocumentos.SOLICITACAO_ID;

            }
        }

        private void ManterUnspsc(List<FornecedorUnspscVM> unspsc, int solicitacaoCriacaoID)
        {
            List<SOLICITACAO_UNSPSC> wfd_sol_unspsc = new List<SOLICITACAO_UNSPSC>();

            unspsc.ForEach(x => wfd_sol_unspsc.Add(new SOLICITACAO_UNSPSC
            {
                SOLICITACAO_ID = solicitacaoCriacaoID,
                UNSPSC_ID = x.UsnpscId
            }));

            SolicitacaoMaterialEServico.ManterUnspscSolicitacao(wfd_sol_unspsc, solicitacaoCriacaoID);
        }

        private DateTime CalculaDataVencimento(int periodoId)
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

        private void CompletarSolicitacaoCadastroPJPF(ref SolicitacaoCadastroFornecedor cadPJPF, FichaCadastralWebForLinkVM ficha)
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

        public List<SelectListItem> MontarDescricaoDocumento(int? tipoDocumento, string tipo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            tipoDocumento = tipoDocumento ?? 0;

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<DescricaoDeDocumentos> descricaoDocumentos =

                Db.WFD_DESCRICAO_DOCUMENTOS.Where(e => e.ATIVO && e.CONTRATANTE_ID == contratanteId && e.TIPO_DOCUMENTOS_ID == tipoDocumento).OrderBy(e => e.DESCRICAO).ToList();

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

    }
}