using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class PreCadastroFornecedorController : ControllerPadrao
    {
        private readonly IPreCadastroWebForLinkAppService _preCadastroService;
        private readonly IContratanteWebForLinkAppService _contratanteService;
        private readonly IFornecedorCategoriaWebForLinkAppService _fornecedorCategoriaService;
        private readonly IFornecedorBaseWebForLinkAppService _fornecedorBaseService;
        private readonly IFornecedorWebForLinkAppService _fornecedorService;
        private readonly ISolicitacaoCadastroFornecedorWebForLinkAppService _solicitacaoCadastroService;
        private readonly IServicosMateriaisWebForLinkAppService _servicosMateriaisService;
        private readonly IConfiguracaoEmailContratanteWebForLinkAppService _configuracaoEmailContratanteService;
        private readonly ITramiteWebForLinkAppService _tramite;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly ISolicitacaoModificacaoContatoWebForLinkAppService _solicitacaoContatoService;
        private readonly ITipoDocumentosWebForLinkAppService _tipoDocumentoService;
        private readonly ISolicitacaoMensagemWebForLinkAppService _solicitacaoMensagemService;
        private readonly IUsuarioWebForLinkAppService _usuarioService;
        private readonly IPapelWebForLinkAppService _papelService;
        private readonly IOrganizacaoComprasWebForLinkAppService _organizacaoComprasService;
        private readonly IEnderecoWebForLinkAppService _enderecoService;


        public PreCadastroFornecedorController(
            IPreCadastroWebForLinkAppService preCadastro,
            IEnderecoWebForLinkAppService endereco,
            IContratanteWebForLinkAppService contratante,
            IFornecedorCategoriaWebForLinkAppService fornecedorCategoria,
            IFornecedorBaseWebForLinkAppService fornecedorBase,
            IFornecedorWebForLinkAppService fornecedor,
            ISolicitacaoCadastroFornecedorWebForLinkAppService solicitacaoCadastro,
            IServicosMateriaisWebForLinkAppService servicosMateriais,
            IConfiguracaoEmailContratanteWebForLinkAppService configuracaoEmailContratanteConfiguracao,
            ITramiteWebForLinkAppService tramite,
            ISolicitacaoWebForLinkAppService solicitacao,
            ISolicitacaoModificacaoContatoWebForLinkAppService solicitacaoContato,
            ITipoDocumentosWebForLinkAppService tipoDocumento,
            IUsuarioWebForLinkAppService usuario,
            ISolicitacaoMensagemWebForLinkAppService solicitacaoMensagem,
            IPapelWebForLinkAppService papel,
            IOrganizacaoComprasWebForLinkAppService organizacaoCompras)
            : base()
        {
            _servicosMateriaisService = servicosMateriais;
            _preCadastroService = preCadastro;
            _enderecoService = endereco;
            _contratanteService = contratante;
            _fornecedorCategoriaService = fornecedorCategoria;
            _fornecedorBaseService = fornecedorBase;
            _fornecedorService = fornecedor;
            _solicitacaoCadastroService = solicitacaoCadastro;
            _configuracaoEmailContratanteService = configuracaoEmailContratanteConfiguracao;
            _tramite = tramite;
            _solicitacaoService = solicitacao;
            _solicitacaoContatoService = solicitacaoContato;
            _tipoDocumentoService = tipoDocumento;
            _usuarioService = usuario;
            _solicitacaoMensagemService = solicitacaoMensagem;
            _papelService = papel;
            _organizacaoComprasService = organizacaoCompras;

            ViewBag.TipoEndereco = new SelectList(_enderecoService.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");
            ViewBag.UF = new SelectList(_enderecoService.ListarTodosPorNome(), "UF_SGL", "UF_NM");
            ViewBag.Imagem = null;
        }

        [Authorize]
        public ActionResult PreCadastroLst(PreCadastroListaVM modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            ViewBag.Empresa = new SelectList(_contratanteService.ListarTodosPorGrupo(grupoId), "ID", "RAZAO_SOCIAL");

            int pagina = modelo.Pagina ?? 1;
            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.MensagemError = modelo.MensagemError ?? "";
            ViewBag.Pagina = pagina;

            List<FORNECEDOR_CATEGORIA> categorias = _fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList();
            ViewBag.Categoria = Mapper.Map<List<CategoriaVM>>(categorias, opt => opt.Items["Url"] = Url);
            ViewBag.CategoriaSelecionada = modelo.CategoriaSelecionada;
            ViewBag.CategoriaSelecionadaNome = modelo.CategoriaSelecionadaNome;
            if (modelo.FiltroPesquisa == null) modelo.FiltroPesquisa = new PreCadastroFiltrosPesquisaVM();
            
            PreCadastroFiltrosDTO filtros = new PreCadastroFiltrosDTO()
            {
                ContratanteId = modelo.Empresa,
                GrupoId = grupoId,
                RazaoSocial = modelo.FiltroPesquisa.Nome,
                CPF = !string.IsNullOrEmpty(modelo.FiltroPesquisa.CPF) ? modelo.FiltroPesquisa.CPF.Replace(".", "").Replace("-", "") : null,
                CNPJ = !string.IsNullOrEmpty(modelo.FiltroPesquisa.CNPJ) ? modelo.FiltroPesquisa.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "") : null,
                CategoriaId = modelo.CategoriaSelecionada ?? 0
            };
            var retornoList = _preCadastroService.ListarTodos(filtros, pagina, 10);

            modelo.ListaGrid = retornoList.RegistrosPagina.Select(x => new PreCadastroGridPesquisaVM
            {
                Nome = x.RAZAO_SOCIAL,
                Documento = _metodosGerais.FormatarCnpjCpf(x.CNPJ),
                Empresa = x.WFD_CONTRATANTE.RAZAO_SOCIAL,
                UrlEditar = Url.Action("Editar", "PreCadastroFornecedor",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("id={0}", x.ID), Key)
                    }, Request.Url.Scheme),
                Status = x.WFD_T_STATUS_PRECADASTRO != null ? x.WFD_T_STATUS_PRECADASTRO.STATUS_NM : string.Empty
            }).ToList();

            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = retornoList.TotalPaginas;
            ViewBag.TotalRegistros = retornoList.TotalRegistros;
            return View(modelo);
        }

        #region CriarPreCadastro
        [Authorize]
        public ActionResult CriarPreCadastro()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            FichaCadastralWebForLinkVM modelo = new FichaCadastralWebForLinkVM(contratanteId, CasosPreCadastroEnum.CadastradoPorContratante);
            modelo.DocumentoEditavel = true;

            if (modelo == null)
                return HttpNotFound();

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CriarPreCadastro(FichaCadastralWebForLinkVM modelo, string ServicosSelecionados, string MateriaisSelecionados, int acaoTxt)
        {
            int validacao = 0;
            modelo.FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            modelo.DocumentoEditavel = true;
            try
            {
                if (!string.IsNullOrEmpty(modelo.CNPJ_CPF))
                    modelo.CNPJ_CPF = modelo.CNPJ_CPF.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

                switch (acaoTxt)
                {
                    //Cadastro sem Solicitacao
                    case 1:
                        validacao = IncluirPjpfBaseAlterarFichaModel(modelo, ServicosSelecionados, MateriaisSelecionados, validacao, acaoTxt);
                        if (validacao != 0)
                            return Json(new
                            {
                                url = Url.Action("PreCadastroLst", "PreCadastroFornecedor", new PreCadastroListaVM(null, "Pré-cadastro incluído com sucesso"))
                            });
                        break;
                    //Cadastro com Solicitacao
                    case 2:
                        validacao = IncluirPjpfBaseAlterarFichaModel(modelo, ServicosSelecionados, MateriaisSelecionados, validacao, acaoTxt);
                        if (validacao != 0)
                            return Json(new
                            {
                                url = Url.Action("CriarSolicitacao", "PreCadastroFornecedor", new { chaveurl = Cripto.Criptografar(string.Format("id={0}", validacao), Key) })
                            });
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            modelo.DadosBancarios = new List<DadosBancariosVM>();
            modelo.DadosContatos = new List<DadosContatoVM>();
            modelo.DadosEnderecos = new List<DadosEnderecosVM>();
            modelo.FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            modelo.IsPjpfBaseProprio = false;
            modelo.IsPjpfProprio = false;
            modelo.DocumentoEditavel = true;
            return PartialView("~/Views/PreCadastroFornecedor/_PreCadastro_FichaCadastral.cshtml", modelo);
        }

        #endregion
        [Authorize]
        public ActionResult Editar(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            FichaCadastralWebForLinkVM modelo = Mapper.Map<FichaCadastralWebForLinkVM>(_fornecedorBaseService.BuscarPorIdPreCadastro(id));
            modelo.ChaveUrl = chaveurl;
            modelo.PreCadastroEnum = CasosPreCadastroEnum.CadastradoPorContratante;

            if (modelo == null)
                return HttpNotFound();

            return View(modelo);
        }

        #region CriarSolicitacao
        [Authorize]
        public ActionResult CriarSolicitacao(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int idPjpfBase;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out idPjpfBase);
            FornecedoresVM modelo = Mapper.Map<FornecedoresVM>(_fornecedorBaseService.BuscarPorIdPreCadastro(idPjpfBase));
            modelo.TipoFornecedor = modelo.CNPJ.Length == 14 ? 1 : 3;
            modelo.TipoCadastro = 1;

            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int? grupoId = (int?)Geral.PegaAuthTicket("Grupo");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            bool solicitaDocumentos = (bool)Geral.PegaAuthTicket("SolicitaDocumentos");
            bool solicitaFichaCadastral = (bool)Geral.PegaAuthTicket("SolicitaFichaCadastral");

            List<FORNECEDOR_CATEGORIA> categorias = _fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList();
            ViewBag.Categoria = Mapper.Map<List<CategoriaVM>>(categorias, opt => opt.Items["Url"] = Url);

            if (grupoId != null)
                ViewBag.Empresa = new SelectList(_contratanteService.ListarTodosPorUsuario(usuarioId), "ID", "RAZAO_SOCIAL", contratanteId);
            ViewBag.Compras = new SelectList(_organizacaoComprasService.ListarTodosPorIdContratante(contratanteId), "ID", "ORG_COMPRAS_DSC");
            ViewBag.SolicitaDocumentos = solicitaDocumentos;
            ViewBag.solicitaFichaCadastral = solicitaFichaCadastral;
            ViewBag.Robo = false;
            ViewBag.Acao = "Incluir";

            if (modelo == null)
                return HttpNotFound();

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CriarSolicitacao(FornecedoresVM model, int? CategoriaSelecionada, string CategoriaSelecionadaNome, int? SolicitacaoID, int Empresa, string Acao)
        {
            if (string.IsNullOrEmpty(Acao))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            bool solicitaDocumentos = (bool)Geral.PegaAuthTicket("SolicitaDocumentos");
            bool solicitaFichaCadastral = (bool)Geral.PegaAuthTicket("SolicitaFichaCadastral");
            int? grupoId = (int?)Geral.PegaAuthTicket("Grupo");

            ViewBag.Categoria = Mapper.Map<List<CategoriaVM>>(_fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList(), opt => opt.Items["Url"] = Url);
            ViewBag.CategoriaSelecionada = CategoriaSelecionada;
            ViewBag.CategoriaSelecionadaNome = CategoriaSelecionadaNome;
            if (CategoriaSelecionada != null)
                model.Categoria = (int)CategoriaSelecionada;

            if (grupoId != null)
                ViewBag.Empresa = new SelectList(_contratanteService.ListarTodosPorUsuario(usuarioId), "ID", "RAZAO_SOCIAL", Empresa);
            ViewBag.SolicitaDocumentos = solicitaDocumentos;
            ViewBag.solicitaFichaCadastral = solicitaFichaCadastral;
            ViewBag.Compras = new SelectList(_organizacaoComprasService.ListarTodosPorIdContratante(contratanteId), "ID", "ORG_COMPRAS_DSC");
            ViewBag.Robo = false;

            ValidarFormularioCriacaoSolicitacao(model, Acao);
            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
                SOLICITACAO solicitacao;
                var papelAtual = _papelService.BuscarPorContratanteETipoPapel(contratanteId, (int)EnumTiposPapel.Solicitante).ID;

                //INCLUSÃO DO FORNECEDOR
                switch (Acao)
                {
                    case "Incluir":
                        string cnpj = string.Empty;
                        if (model.TipoFornecedor != (int)EnumTiposFornecedor.EmpresaEstrangeira)
                            cnpj = model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

                        CadastrarSolicitacaoDTO modeloCadastro = new CadastrarSolicitacaoDTO()
                        {
                            TipoCadastro = model.TipoCadastro,
                            TipoFornecedor = model.TipoFornecedor,
                            ContratanteId = contratanteId,
                            UsuarioId = usuarioId,
                            CategoriaId = model.Categoria,
                            ComprasId = model.Compras,
                            CNPJ = cnpj,
                            ContatoNome = model.NomeContato,
                            ContatoEmail = model.Email,
                            Telefone = model.Telefone,
                            Assunto = System.Web.HttpContext.Current.Application["NomeSistema"].ToString(),
                            RazaoSocial = model.RazaoSocial,
                            CPF = cnpj,
                            DataNascimento = model.DataNascimento
                        };
                        solicitacao = _solicitacaoService.CadastrarSolicitacaoPreCadastro(model.ID, modeloCadastro);

                        ViewBag.SolicitacaoId = solicitacao.ID;

                        if (model.TipoFornecedor != (int)EnumTiposFornecedor.EmpresaEstrangeira) // SE NACIONAL
                        {
                            _tramite.AtualizarTramite(contratanteId, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aguardando, usuarioId);
                            ViewBag.Robo = true;

                            return View(model);
                        }

                        return RedirectToAction("FornecedoresDiretoFrm", "FornecedoresDireto", new { chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}", solicitacao.ID), Key) });
                    //break;

                    case "Continuar":
                        solicitacao = _solicitacaoService.BuscarPorIdComSolicitacaoCadastroFornecedor((int)SolicitacaoID);
                        var robo = solicitacao.ROBO.FirstOrDefault();

                        if (robo != null)
                        {
                            if (robo.RF_CONSULTA_DTHR != null && robo.SINT_CONSULTA_DTHR != null && robo.SN_CONSULTA_DTHR != null)
                            {
                                solicitacao.ROBO_EXECUTADO = true;
                                _solicitacaoService.Alterar(solicitacao);
                            }
                        }

                        if (model.TipoCadastro == (int)EnumTiposPreenchimento.Fornecedor)
                        {
                            _tramite.AtualizarTramite(contratanteId, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aprovado, usuarioId);
                            return RedirectToAction("EnviarSolicitacao", "PreCadastroFornecedor", new
                            {
                                chaveUrl = Cripto.Criptografar(string.Format("SolicitacaoID={0}", solicitacao.ID), Key),
                                nomeContato = model.NomeContato,
                                emailContato = model.Email,
                                contratanteId = Empresa,
                                documentoPfPj = model.CNPJ
                            });
                        }

                        return RedirectToAction("FornecedoresDiretoFrm", "FornecedoresDireto", new { chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}", solicitacao.ID), Key) });

                    //break;
                    case "Cancelar":
                        solicitacao = _solicitacaoService.BuscarPorIdIncluindoFluxo((int)SolicitacaoID);

                        _tramite.AtualizarTramite(contratanteId, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, 6, usuarioId);

                        return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = "Solicitação Cancelada!" });
                        //break;

                }
            }
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        #endregion

        #region EnviarSolicitacao
        [Authorize]
        public ActionResult EnviarSolicitacao(string chaveUrl, string nomeContato, string emailContato, int contratanteId, string documentoPfPj)
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
                    ViewBag.TipoDocumentos = new SelectList(_tipoDocumentoService.ListarPorIdContratante(contratanteId), "ID", "DESCRICAO");
                    ViewBag.DescricaoDocumentos = new SelectList(new List<DescricaoDeDocumentos>(), "ID", "DESCRICAO");

                    solicitacaoFornecedorVM.SolicitacaoCriacaoID = solicitacaoId;
                    solicitacaoFornecedorVM.ContratanteSelecionado = contratanteId;
                    solicitacaoFornecedorVM.Fornecedor = new SolicitacaoFornecedoresVM
                    {
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
        public ActionResult EnviarSolicitacao(SolicitacaoFornecedorVM model, string Email)
        {
            var solicitacao = _solicitacaoService.BuscarPorIdComDocumentos((int)model.SolicitacaoCriacaoID);
            var cadForn = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();

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
                    _solicitacaoMensagemService.InserirMensagem(solicitacaoMensagem, solicitacao.SolicitacaoDeDocumentos.ToList());
                }
                catch
                {
                    ModelState.AddModelError("", "Erro ao tentar salvar a Solicitação de Documentos. A solicitação não foi enviada!");
                    return View(model);
                }
                //se não for primeiro acesso enviar para tela de acesso
                string url = Url.Action("Index", "Home", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}&Login={1}&TravaLogin=1", model.SolicitacaoCriacaoID, model.Fornecedor.CNPJ), Key)
                }, Request.Url.Scheme);

                bool emailEnviadoComSucesso = false;

                _solicitacaoContatoService.ListarPorSolicitacaoId((int)model.SolicitacaoCriacaoID)
                    .ForEach(x =>
                    {
                        //se for primeiro acesso enviar para criação de usuário
                        #region BuscarPorEmail
                        //validar se o e-mail já existe na tabela de Usuarios
                        if (!_usuarioService.ValidarPorCnpj(model.Fornecedor.CNPJ))
                        {
                            url = Url.Action("CadastrarUsuarioFornecedor", "Home", new
                            {
                                chaveurl = Cripto.Criptografar(string.Format("Login={0}&SolicitacaoID={1}&Email={2}",
                                model.Fornecedor.CNPJ,
                                model.SolicitacaoCriacaoID,
                                x.EMAIL), Key)
                            }, Request.Url.Scheme);
                        }
                        #endregion
                        string mensagem = string.Concat(model.Mensagem, "<p><a href='", url, "'>Link</a>:", url, "</p>");
                        emailEnviadoComSucesso = _metodosGerais.EnviarEmail(x.EMAIL, model.Assunto, mensagem);
                    });
                if (!emailEnviadoComSucesso)
                {
                    return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = "As tentativas de envio de e-mail falharam, favor reenviar a solicitação através da tela de acompanhamento." });
                }

                return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação Nº {0} enviada ao Fornecedor com sucesso!", model.SolicitacaoCriacaoID) });
            }

            return View(model);
        }

        #endregion

        #region Métodos Privados
        private int IncluirPjpfBaseAlterarFichaModel(FichaCadastralWebForLinkVM modelo, string ServicosSelecionados, string MateriaisSelecionados, int validacao, int acao)
        {
            ////UNSPSC
            modelo.FornecedoresUnspsc = PreencheModelUnspsc(modelo.PJPFID, ServicosSelecionados, MateriaisSelecionados, modelo.SolicitacaoID);

            validacao = IncluirDadosTabelaPjPfBase(modelo, acao);
            ViewBag.ExibirFicha = true;
            modelo.PjpfBaseId = validacao;
            ViewBag.RetSucessoBancos = validacao != 0 ? 1 : -1;
            modelo.DocumentoEditavel = validacao == 0;
            return validacao;
        }

        public SolicitacaoFornecedorVM PopularSolicitacaoCadastroPJPF(int contratanteId, SolicitacaoFornecedorVM modelo)
        {
            var solicitacao = _solicitacaoService.BuscarPorIdComDocumentos((int)modelo.SolicitacaoCriacaoID);
            var solforn = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();
            var configEmail = _configuracaoEmailContratanteService.BuscarPorContratanteETipo(contratanteId, 1);

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
            modelo.Mensagem = configEmail.CORPO;
            modelo.PassoAtual = 3;

            return modelo;
        }

        private void ValidarFormularioCriacaoSolicitacao(FornecedoresVM model, string Acao)
        {
            if (!string.IsNullOrEmpty(model.Email))
                if (!Validacao.ValidarEmail(model.Email))
                    ModelState.AddModelError("Contato.Email", "O e-mail informado não está em um formato válido.");

            if (model.Categoria == 0)
                ModelState.AddModelError("Categoria", "Informe a Categoria!");

            if (model.TipoFornecedor == 1 || model.TipoFornecedor == 3)
            {
                if (model.CNPJ == null)
                    ModelState.AddModelError("CNPJ", "CNPJ/CPF Obrigatório");
                else
                {
                    if (model.TipoFornecedor == 1)
                    {
                        if (!Validacao.ValidaCNPJ(model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
                            ModelState.AddModelError("CNPJ", "CNPJ Inválido");
                    }
                    else
                    {
                        if (!Validacao.ValidaCPF(model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
                            ModelState.AddModelError("CNPJ", "CPF Inválido");
                    }
                }
            }

            if (Acao == "Incluir")
            {
                if (model.TipoFornecedor != 2)
                {
                    var cnpjteste = model.CNPJ == null
                        ? string.Empty
                        : model.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

                    if (_fornecedorService.BuscarIdFornecedorPorCnpj(cnpjteste) != 0)
                        ModelState.AddModelError("CNPJ", "Já existe um fornecedor cadastrado com esse CNPJ/CPF!");

                    if (_solicitacaoCadastroService.BuscarPorStatusId(cnpjteste, 6) != null)
                        ModelState.AddModelError("CNPJ", "Já existe uma solicitação de criação para este CNPJ/CPF!");
                }
                else
                {
                    if (_fornecedorService.BuscarPorRazaoSocial(model.RazaoSocial) != null)
                        ModelState.AddModelError("RazaoSocial", "Já existe um fornecedor cadastrado com esta Razão Social");

                    if (_solicitacaoCadastroService.BuscarPorRazaoSocial(model.RazaoSocial) != null)
                        ModelState.AddModelError("RazaoSocial", "Já existe uma solicitação de criação para esta Razão Social");
                }
            }

            // SE TIPO ESTRANGEIRO RETIRA A VALIDACAO DO CNPJ
            if (model.TipoFornecedor == 2)
            {
                ModelState.Remove("CNPJ");
                ModelState.Remove("CONTATO.EMAIL");
            }

            // SE EMPRESA NACIONAL OU PESSOA FÍSICA, RETIRA A VALIDAÇÃO DA RAZÃO SOCIAL
            if (model.TipoFornecedor == 1 || model.TipoFornecedor == 3)
                ModelState.Remove("RAZAOSOCIAL");

            if (model.Categoria > 0)
            {
                FORNECEDOR_CATEGORIA categoria = _fornecedorCategoriaService.Get(model.Categoria);
                int totalDocCatecoria = categoria.ListaDeDocumentosDeFornecedor.Count;

                if (categoria.ISENTO_DOCUMENTOS && categoria.ISENTO_DADOSBANCARIOS && categoria.ISENTO_CONTATOS)
                {
                    if (model.TipoCadastro == 1)
                        ModelState.AddModelError("", "A Categoria escolhida é isenta de Documentação, Dados Bancários e Contato, no entanto você está tentando enviar a solicitação para o fornecedor sem um e-mail de contato. Para continuar sem contato marque a opção \"Eu preencherei os dados\".");
                    else
                        ModelState.Remove("Email");
                }
                else
                    if (totalDocCatecoria == 0)
                    ModelState.AddModelError("", "Não é possível enviar esta solicitação. A Categoria/Grupo de Contas selecionado não possue Lista de Documento para solicitação!");
            }
        }

        private int IncluirDadosTabelaPjPfBase(FichaCadastralWebForLinkVM modelo, int acao)
        {
            try
            {
                modelo.CNPJ_CPF = modelo.CNPJ_CPF.Replace(".", "").Replace("-", "").Replace("/", "");
                _preCadastroService.FornecedorBase = Mapper.Map<FORNECEDORBASE>(modelo);
                modelo.DadosEnderecos.ToList().ForEach(
                    x =>
                    {
                        if (x.T_UF != null)
                            x.UF = x.T_UF.UF_SGL;
                    });
                _preCadastroService.FornecedorBaseEndereco = Mapper.Map<List<FORNECEDORBASE_ENDERECO>>(modelo.DadosEnderecos.ToList());
                _preCadastroService.FornecedorBaseContato = Mapper.Map<List<FORNECEDORBASE_CONTATOS>>(modelo.DadosContatos.ToList());
                _preCadastroService.FornecedoresBaseUnspsc = Mapper.Map<List<FORNECEDORBASE_UNSPSC>>(modelo.FornecedoresUnspsc.ToList());
                _preCadastroService.DocumentoFornecedor = _preCadastroService.FornecedorBase.CPF ?? _preCadastroService.FornecedorBase.CNPJ;
                _preCadastroService.ContratanteId = modelo.ContratanteID;
                _preCadastroService.PjpfBaseId = modelo.PjpfBaseId;
                try
                {
                    _preCadastroService.IncluirPreCadastro(modelo.PreCadastroEnum, acao);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return 0;
                }
                return modelo.PjpfBaseId == 0 ? _preCadastroService.PjpfBaseId : modelo.PjpfBaseId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<FornecedorUnspscVM> PreencheModelUnspsc(int FornecedorId, string ServicosSelecionados, string MateriaisSelecionados, int? idSolicitacao)
        {
            string[] servicos = ServicosSelecionados.Split(new Char[] { '|' });
            string[] materiais = MateriaisSelecionados.Split(new Char[] { '|' });
            int[] vUnspsc = servicos.Concat(materiais).Where(x => !String.IsNullOrEmpty(x)).Select(int.Parse).ToArray();

            var unspscs = _servicosMateriaisService.BuscarListaPorID(vUnspsc);

            var listaUnspsc = new List<FornecedorUnspscVM>();

            unspscs.ForEach(x => listaUnspsc.Add(new FornecedorUnspscVM
            {
                Niv = (int)x.NIV,
                SolicitacaoId = idSolicitacao,
                UsnpscCodigo = x.UNSPSC_COD,
                UsnpscDescricao = x.UNSPSC_DSC,
                UsnpscId = x.ID,
                FornecedorId = FornecedorId
            }));

            return listaUnspsc;
        }
        #endregion
    }
}
