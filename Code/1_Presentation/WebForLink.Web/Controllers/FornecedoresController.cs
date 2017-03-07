using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Exceptions;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.Fornecedores;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class FornecedoresController : ControllerPadrao
    {
        protected internal readonly IContratanteWebForLinkAppService _contratanteService;
        protected internal readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoService;
        protected internal readonly IContratanteOrganizacaoComprasWebForLinkAppService _contratanteOrganizacaoComprasService;
        protected internal readonly IFornecedorWebForLinkAppService _fornecedorService;
        protected internal readonly IFornecedorCategoriaWebForLinkAppService _fornecedorCategoriaService;
        protected internal readonly IFornecedorArquivoWebForLinkAppService _fornecedorArquivoService;
        protected internal readonly IFornecedorBaseWebForLinkAppService _fornecedorBaseBP;
        protected internal readonly ISolicitacaoDocumentosFornecedorWebForLinkAppService _pjPfSolicitacaoDocumentosService;
        protected internal readonly ISolicitacaoCadastroFornecedorWebForLinkAppService _solicitacaoCadastroFornecedorService;
        protected internal readonly ISolicitacaoModificacaoBancoWebForLinkAppService _solicitacaoModificacaoBancoService;
        protected internal readonly ISolicitacaoModificacaoContatoWebForLinkAppService _solicitacaoModificacaoContatoService;
        protected internal readonly ISolicitacaoModificacaoEnderecoWebForLinkAppService _solicitacaoModificacaoEnderecoService;
        protected internal readonly ISolicitacaoMaterialEServicoWebForLinkAppService SolicitacaoMaterialEServicoService;
        protected internal readonly ISolicitacaoWebForLinkAppService _solicitacaoService;

        protected internal readonly ITramiteWebForLinkAppService _tramite;
        protected internal readonly IPapelWebForLinkAppService _papelBP;
        protected internal readonly IBancoWebForLinkAppService _bancoBP;
        protected internal readonly ITipoDocumentosWebForLinkAppService _tipoDocumentosBP;
        protected internal readonly IConfiguracaoWebForLinkAppService _configService;
        protected internal readonly IEnderecoWebForLinkAppService _enderecoBP;
        protected internal readonly IOrganizacaoComprasWebForLinkAppService _organizacaoComprasBP;
        protected internal readonly IFluxoWebForLinkAppService _fluxoBP;
        protected internal readonly ICadastroUnicoWebForLinkAppService _cadastroUnicoBP;
        protected internal readonly IInformacaoComplementarWebForLinkAppService _informacaoComplementarBP;
        protected internal readonly IConfiguracaoEmailContratanteWebForLinkAppService _contratanteConfiguracaoEmailBP;

        //protected internal IGeral _geral;
        public FornecedoresController(
        IFornecedorWebForLinkAppService fornecedor,
        IContratanteWebForLinkAppService contratante,
        IFornecedorCategoriaWebForLinkAppService fornecedorCategoria,
        IContratanteOrganizacaoComprasWebForLinkAppService organizacaoCompras,
        ISolicitacaoWebForLinkAppService solicitacao,
        ISolicitacaoDocumentosFornecedorWebForLinkAppService pJpFSolicitacaoDocumentos,
        ISolicitacaoCadastroFornecedorWebForLinkAppService solicitacaoCadastroFornecedor,
        ITipoDocumentosWebForLinkAppService tipoDocumentos,
        ISolicitacaoModificacaoBancoWebForLinkAppService solicitacaoModificacaoBanco,
        ISolicitacaoModificacaoContatoWebForLinkAppService solicitacaoModificacaoContato,
        ISolicitacaoModificacaoEnderecoWebForLinkAppService solicitacaoModificacaoEndereco,
        IConfiguracaoWebForLinkAppService config,
        ISolicitacaoMaterialEServicoWebForLinkAppService solicitacaoMaterialEServico,
        ITramiteWebForLinkAppService tramite,
        IPapelWebForLinkAppService papelBP,
        IFornecedorArquivoWebForLinkAppService fornecedorArquivoService,
        IFornecedorBaseWebForLinkAppService fornecedorBaseBP,
        IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracaoBP,
        IOrganizacaoComprasWebForLinkAppService organizacaoComprasBP,
        IFluxoWebForLinkAppService fluxoBP,
        ICadastroUnicoWebForLinkAppService cadastroUnicoBP)
        {
            try
            {
                _papelBP = papelBP;
                _fornecedorArquivoService = fornecedorArquivoService;
                _fornecedorBaseBP = fornecedorBaseBP;
                _contratanteConfiguracaoService = contratanteConfiguracaoBP;
                _organizacaoComprasBP = organizacaoComprasBP;
                _fluxoBP = fluxoBP;
                _cadastroUnicoBP = cadastroUnicoBP;
                _fornecedorService = fornecedor;
                _contratanteService = contratante;
                _fornecedorCategoriaService = fornecedorCategoria;
                _contratanteOrganizacaoComprasService = organizacaoCompras;
                _solicitacaoService = solicitacao;
                _pjPfSolicitacaoDocumentosService = pJpFSolicitacaoDocumentos;
                _solicitacaoCadastroFornecedorService = solicitacaoCadastroFornecedor;
                _tipoDocumentosBP = tipoDocumentos;
                _solicitacaoModificacaoBancoService = solicitacaoModificacaoBanco;
                _solicitacaoModificacaoContatoService = solicitacaoModificacaoContato;
                _solicitacaoModificacaoEnderecoService = solicitacaoModificacaoEndereco;
                _configService = config;
                SolicitacaoMaterialEServicoService = solicitacaoMaterialEServico;
                _tramite = tramite;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        [Authorize]
        public ActionResult FornecedoresLst(PesquisaFornecedorVM model)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            bool solicitaFichaCadastral = (bool)Geral.PegaAuthTicket("SolicitaFichaCadastral");

            int paginaAtual = model.Filtro.Pagina ?? 1;

            Expression<Func<Fornecedor, bool>> filtro = Predicativos.FiltrarFornecedoresPesquisaGrid(model, grupoId, contratanteId);
            var listaFornecedores = _fornecedorService.PesquisarFornecedoresDisponiveis(filtro, paginaAtual, 10, x => x.ID, contratanteId);

            model.Grid = Mapper.Map<List<ListaPesquisaFornecedorVM>>(listaFornecedores.RegistrosPagina);
            model.Grid.ForEach(x =>
            {
                x.UrlEditar = Cripto.Criptografar(string.Format("FornecedorID={0}&ContratanteID={1}", x.FornecedorId, x.ContratanteId), Key);
            });


            List<FornecedoresVM> fornecedoresVM = new List<FornecedoresVM>();

            //// SE NAO FOR FICHA CADASTRAL
            //if (!solicitaFichaCadastral)
            //this.ListarFornecedoresSemFichaCadastral(model.CategoriaSelecionada, model.Fornecedor, model.CNPJ, model.CPF, grupoId, paginaAtual, fornecedoresVM, out totalRegistro, out totalPaginas);
            //else
            //this.ListarFornecedoresComFichaCadastral(model.CategoriaSelecionada, model.Fornecedor, model.CNPJ, grupoId, pagina, fornecedoresVM, model, out totalRegistro, out totalPaginas);
            model.Filtro.Paginacao = new PaginacaoModel(listaFornecedores.TotalRegistros, paginaAtual, 10);

            //model.Filtro.Categorias = Mapper.Map<List<CategoriaVM>>(_fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList(), opt => opt.Items["Url"] = Url);
            model.Filtro.Categorias = CategoriaVM.ModelToViewModel(_fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList(), Url);
            model.Filtro.Empresas = _contratanteService.ListarTodosPorUsuario(grupoId).Select(x => new SelectListItem
            {
                Text = x.RAZAO_SOCIAL,
                Value = x.ID.ToString()
            }).ToList();

            ViewBag.MensagemSucesso = model.Filtro.MensagemSucesso ?? "";
            ViewBag.MensagemError = model.Filtro.MensagemError ?? "";

            return View(model);
        }

        [Authorize]
        [WebForLinkFilter]
        [HttpGet]
        public ActionResult FornecedoresFrm(string chaveurl, string cnpj, string cpf)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int? grupoId = (int?)Geral.PegaAuthTicket("Grupo");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            bool solicitaDocumentos = (bool)Geral.PegaAuthTicket("SolicitaDocumentos");
            bool solicitaFichaCadastral = (bool)Geral.PegaAuthTicket("SolicitaFichaCadastral");

            List<FORNECEDOR_CATEGORIA> categorias = _fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList();
            List<CategoriaVM> modelo = Mapper.Map<List<CategoriaVM>>(categorias, x => x.Items["Url"] = Url);
            ViewBag.Categoria = modelo;

            if (grupoId != null)
                ViewBag.Empresa = new SelectList(_contratanteService.ListarTodosPorUsuario(usuarioId), "ID", "RAZAO_SOCIAL", contratanteId);
            ViewBag.Compras = new SelectList(_organizacaoComprasBP.ListarTodosPorIdContratante(contratanteId), "ID", "ORG_COMPRAS_DSC");
            ViewBag.SolicitaDocumentos = solicitaDocumentos;
            ViewBag.solicitaFichaCadastral = solicitaFichaCadastral;
            ViewBag.Robo = false;

            int id = 0;
            string acao = "";
            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
                acao = param.First(p => p.Name == "Acao").Value;
            }
            FornecedoresVM fornecedorVM = new FornecedoresVM();
            fornecedorVM.TipoFornecedor = 1;

            // INCLUSAO
            if (id == 0)
            {
                fornecedorVM.Contato = new FornecedorContatosVM { ID = 0, Email = "", Nome = "" };
                fornecedorVM.Contatos.Add(new FornecedorContatosVM { ID = 0, Email = "", Nome = "" });
                fornecedorVM.Ativo = true;

                if (!string.IsNullOrEmpty(cnpj))
                {
                    fornecedorVM.TipoFornecedor = 1;
                    fornecedorVM.CNPJ = cnpj;
                }
                else if (!string.IsNullOrEmpty(cpf))
                {
                    fornecedorVM.TipoFornecedor = 3;
                    fornecedorVM.CNPJ = cpf;
                }
                else
                {
                    fornecedorVM.TipoFornecedor = 1;
                }

                fornecedorVM.TipoCadastro = 1;

                ViewBag.Acao = "Incluir";
            }
            else
            {
                ViewBag.Acao = acao;

                FORNECEDORBASE f = _fornecedorBaseBP.BuscarPorIDContratanteID(id, contratanteId);
                fornecedorVM.ID = f.ID;
                fornecedorVM.RazaoSocial = f.RAZAO_SOCIAL;
                fornecedorVM.CNPJ = f.CNPJ;
                if (!string.IsNullOrEmpty(f.TELEFONE))
                    fornecedorVM.Telefone = f.TELEFONE.PadLeft(13, ' ');
                fornecedorVM.Ativo = f.ATIVO;
                foreach (FORNECEDORBASE_CONTATOS contato in f.WFD_PJPF_BASE_CONTATOS)
                {
                    fornecedorVM.Contatos.Add(new FornecedorContatosVM
                    {
                        ID = contato.ID,
                        Nome = contato.NOME,
                        Email = contato.EMAIL
                    });
                }
            }

            return View(fornecedorVM);
        }

        [Authorize]
        [WebForLinkFilter]
        [HttpPost]
        public ActionResult FornecedoresFrm(FornecedoresVM model, int? CategoriaSelecionada, string CategoriaSelecionadaNome, int? SolicitacaoID, int Empresa, string Acao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            bool solicitaDocumentos = (bool)Geral.PegaAuthTicket("SolicitaDocumentos");
            bool solicitaFichaCadastral = (bool)Geral.PegaAuthTicket("SolicitaFichaCadastral");
            int? grupoId = (int?)Geral.PegaAuthTicket("Grupo");

            ViewBag.Categoria = Mapper.Map<List<CategoriaVM>>(_fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList(), x => x.Items["Url"] = Url);
            ViewBag.CategoriaSelecionada = CategoriaSelecionada;
            ViewBag.CategoriaSelecionadaNome = CategoriaSelecionadaNome;
            if (CategoriaSelecionada != null)
                model.Categoria = (int)CategoriaSelecionada;

            if (grupoId != null)
                ViewBag.Empresa = new SelectList(_contratanteService.ListarTodosPorUsuario(usuarioId), "ID", "RAZAO_SOCIAL", Empresa);
            ViewBag.SolicitaDocumentos = solicitaDocumentos;
            ViewBag.solicitaFichaCadastral = solicitaFichaCadastral;
            ViewBag.Compras = new SelectList(_organizacaoComprasBP.ListarTodosPorIdContratante(contratanteId), "ID", "ORG_COMPRAS_DSC");
            ViewBag.Robo = false;

            model.ValidarCriacaoFornecedor();

            this.ValidarFormularioCriacaoSolicitacao(model, Acao, contratanteId);

            if (ModelState.IsValid)
            {
                SOLICITACAO solicitacao;
                var papelAtual = _papelBP.BuscarPorContratanteETipoPapel(contratanteId, (int)EnumTiposPapel.Solicitante).ID;

                //INCLUSÃO DO FORNECEDOR
                switch (Acao)
                {
                    case "Incluir":
                        try
                        {
                            return IncluirInclusaoFornecedor(model, contratanteId, usuarioId, out solicitacao, papelAtual);
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                            ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                            throw;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);
                            ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                        }
                        break;
                    case "Continuar":
                        return AlterarInclusaoFornecedorIncluindoRobo(model, SolicitacaoID, Empresa, contratanteId, usuarioId, out solicitacao, papelAtual);
                    case "Cancelar":
                        return CancelarInclusaoFornecedor(SolicitacaoID, contratanteId, usuarioId, out solicitacao, papelAtual);
                }
            }

            return View(model);
        }

        [Authorize]
        public FileResult FornecedorArquivo(string chaveurl)
        {
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            int arquivoID = 0;

            if (param.FirstOrDefault(p => p.Name == "ArquivoID") != null)
                Int32.TryParse(param.FirstOrDefault(p => p.Name == "ArquivoID").Value, out arquivoID);

            if (arquivoID > 0)
            {
                var wfdArquivo = _fornecedorArquivoService.BuscarPorId(arquivoID);

                if (wfdArquivo != null)
                {
                    var caminho = wfdArquivo.CAMINHO + wfdArquivo.ID + "##" + wfdArquivo.NOME_ARQUIVO;
                    if (!Directory.Exists(caminho))
                        Log.Error(string.Format("Caminho Inexistente para o arquivo {0}", arquivoID));
                    return File(caminho, wfdArquivo.TIPO_ARQUIVO, wfdArquivo.NOME_ARQUIVO);
                }
            }
            return File(string.Empty, string.Empty);
        }

        [Authorize]
        public ActionResult FornecedoresExibirFrm(string chaveurl)
        {
            int fornecedorId = 0;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "id").Value, out fornecedorId);
            }

            Fornecedor fornecedor = _fornecedorService.BuscarPorIdComRelacionamentos(fornecedorId);

            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM
                (fornecedor.Contratante.RAZAO_SOCIAL,
                fornecedor.RAZAO_SOCIAL,
                fornecedor.NOME_FANTASIA,
                //fornecedor.CNAE,
                fornecedor.TIPO_PJPF_ID == 3
                    ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00")
                    : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00"),
                fornecedor.INSCR_ESTADUAL,
                fornecedor.INSCR_MUNICIPAL);
            #region Dados Gerais

            this.FornecedorRobo(ficha, fornecedor);

            if (fornecedor.ROBO != null)
                ficha.FornecedorRobo.SimplesNacionalSituacao = fornecedor.ROBO.SIMPLES_NACIONAL_SITUACAO ?? "";

            #endregion Dados Gerais

            #region Dados de Endereço
            Mapeamento.PopularEndereco(ficha, fornecedor);
            #endregion Dados de Endereço

            #region Dados Bancarios

            WFD_CONTRATANTE_PJPF contratante = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault();
            if (contratante == null)
            {
                Log.Error("Não existe contratante");
                throw new WebForLinkException("Não existe contratante");
            }
            if (contratante.BancoDoFornecedor != null && contratante.BancoDoFornecedor.Count > 0)
            {
                ficha.DadosBancarios = Mapper.Map<List<DadosBancariosVM>>(contratante.BancoDoFornecedor.ToList());
            }

            #endregion Dados Bancarios

            #region Dados de Contatos

            if (contratante.WFD_PJPF_CONTATOS != null && contratante.WFD_PJPF_CONTATOS.Count > 0)
            {
                ficha.DadosContatos = Mapper.Map<List<DadosContatoVM>>(contratante.WFD_PJPF_CONTATOS.ToList());
            }

            #endregion Dados de Contatos

            #region Dados de Documentos

            ficha.SolicitacaoFornecedor = new SolicitacaoFornecedorVM
            {
                Solicitacao = false,
                Documentos = Mapper.Map<List<SolicitacaoDocumentosVM>>(fornecedor.DocumentosDoFornecedor.ToList())
            };

            #endregion Dados de Documentos

            return View(ficha);
        }


        private List<Fornecedor> ListarFornecedorPorFornecedoresId(List<int> fornecedoresId)
        {
            return Db.WFD_PJPF
                .Include("WFD_CONTRATANTE_PJPF.WFD_PJPF_CONTATOS")
                .Where(x => fornecedoresId.Contains(x.ID))
                .ToList();
        }

        [Authorize]
        public ActionResult SolicitarDocumentos(PesquisaFornecedoresVM model)
        {
            string idsCripto = Cripto.Criptografar(string.Format("ids={0}", model.ids), Key);
            return RedirectToAction("FornecedoresSolicitacaoDocumento", "Fornecedores", new
            {
                chaveurl = idsCripto
            });
        }


        #region Fornecedores Pesquisa e Cadastro
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfirmarFornecedoresSolicitacaoDocumento(FornecedoresSolicitacaoDocumentosVM model, string hdnFornecedores, string[] grupoDoc, string[] doc, string[] hdnObrigatorio, string[] hdnTipoAtualizacao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            /* Buscando os fornecedores */
            var fornecedoresId = hdnFornecedores.Split('|').Select(Int32.Parse).ToList();
            List<Fornecedor> listFornecedores = ListarFornecedorPorFornecedoresId(fornecedoresId);

            var dtPrazo = DateTime.Now.AddDays(_contratanteConfiguracaoService.BuscarPorID(contratanteId).PRAZO_ENTREGA_FICHA);
            var fluxoId = _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.ModificacaoDocumentos, contratanteId).ID;
            var papelAtual = _papelBP.BuscarPorContratanteETipoPapel(contratanteId, (int)EnumTiposPapel.Solicitante).ID;

            listFornecedores.ForEach(item =>
            {
                this.IncluirSolicitacaoDocumentoEnviarEmailContratantes(model, doc, hdnObrigatorio, hdnTipoAtualizacao, contratanteId, usuarioId, dtPrazo, fluxoId, papelAtual, item);
            });

            return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Documentos solicitados com sucesso!") });
        }

        [Authorize]
        public ActionResult FornecedoresSolicitacaoDocumento(string chaveurl)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            string ids = string.Empty;

            if (param.FirstOrDefault(p => p.Name == "ids") != null)
                ids = param.FirstOrDefault(p => p.Name == "ids").Value;

            List<string> lstIds = ids.Split('|').ToList();
            lstIds.Remove("");
            int[] arrayFornecedoresId = Array.ConvertAll(lstIds.ToArray(), int.Parse);


            var modeloEmail = Db.WFD_CONTRATANTE_CONFIG_EMAIL
                .FirstOrDefault(x => x.EMAIL_TP_ID == 3 &&
                    x.CONTRATANTE_ID == contratanteId);

            var listaPjpf = Db.WFD_PJPF
                .Where(x => x.WFD_CONTRATANTE_PJPF
                    .Any(a => arrayFornecedoresId.Contains(a.ID))
                )
                .ToList();

            var diasExpiracaoDocumento = _contratanteConfiguracaoService.BuscarPorID(contratanteId).PRAZO_ENTREGA_FICHA;

            Contratante contratante = _contratanteService.BuscarPorId(contratanteId);

            FornecedoresSolicitacaoDocumentosVM fornecedoresSolicitacaoDocumentosVM = new FornecedoresSolicitacaoDocumentosVM(
                1,
                contratante.CNPJ,
                modeloEmail.ASSUNTO,
                modeloEmail.CORPO,
                listaPjpf,
                diasExpiracaoDocumento,
                contratante.RAZAO_SOCIAL
                );

            ViewBag.TipoDocumentos = new SelectList(Db.WFD_TIPO_DOCUMENTOS.Where(x =>
                x.CONTRATANTE_ID == contratanteId)
                .OrderBy(e =>
                    e.DESCRICAO), "ID", "DESCRICAO");

            int tipoDocumento = 0;
            int descricaoDocumento = 0;

            ViewBag.DescricaoDocumentos = new SelectList(this.MontarDescricaoDocumento(tipoDocumento, "Frm"), "Value", "Text", descricaoDocumento);
            ViewBag.Periodicidade = new SelectList(Db.WFD_TIPO_PERIODICIDADE.OrderBy(x => x.ID).ToList(), "ID", "PERIODICIDADE_NM", fornecedoresSolicitacaoDocumentosVM.Periodicidade);

            return View(fornecedoresSolicitacaoDocumentosVM);
        }
        #endregion Fornecedores Pesquisa e Cadastro

        #region Fornecedores Modificação

        [HttpGet]
        public ActionResult FornecedorFichaVL(string chaveurl)
        {
            try
            {
                string CNPJ_CPF = "";
                string CodContratanteVL = "";
                string chaveVL = "";
                int contranteId = 0;

                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

                    CodContratanteVL = param.First(p => p.Name == "CodContratanteVL").Value;
                    CNPJ_CPF = param.First(p => p.Name == "CNPJ_CPF").Value;
                    chaveVL = param.First(p => p.Name == "ChaveVL").Value;
                }
                if (chaveVL != _configService.BuscarConfigGeral().CHAVE_WEBSERVICE || String.IsNullOrEmpty(CNPJ_CPF))
                {
                    FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
                    ViewBag.erro = "Acesso Negado!";
                    return View(ficha);
                }

                var cnpj = CNPJ_CPF.Length > 11 ? CNPJ_CPF : "";
                var cpf = CNPJ_CPF.Length <= 11 ? CNPJ_CPF : "";

                var pjpf = Db.WFD_PJPF.FirstOrDefault(x => x.CNPJ == cnpj);
                var contratante = Db.Contratante.FirstOrDefault(x => x.COD_WEBFORMAT == CodContratanteVL);

                Fornecedor fornecedor = _fornecedorService.BuscarPorIdComRelacionamentos(pjpf.ID);
                WFD_CONTRATANTE_PJPF contratanteFornecedor = null;

                FichaCadastralWebForLinkVM fichaCadastralVM = Mapper.Map<Fornecedor, FichaCadastralWebForLinkVM>(fornecedor);
                fichaCadastralVM.HabilitaEdicao = false;
                fichaCadastralVM.PJPFID = pjpf.ID;

                //Mapear UNSPSC
                fichaCadastralVM.FornecedoresUnspsc = Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(fornecedor.FornecedorServicoMaterialList.Where(x => x.DT_EXCLUSAO == null).ToList());
                ViewBag.DataAtuUnspsc = fichaCadastralVM.DataAtualizacaoUnspsc;
                fichaCadastralVM.FornecedorRobo = new FornecedorRoboVM();
                this.FornecedorRobo(fichaCadastralVM, fornecedor);

                if (contratante != null)
                {
                    contranteId = contratante.ID;
                    contratanteFornecedor = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == contranteId);

                    if (contratanteFornecedor != null)
                    {

                        fichaCadastralVM.ContratanteID = contratanteFornecedor.CONTRATANTE_ID;
                        fichaCadastralVM.ContratanteFornecedorID = contratanteFornecedor.ID;
                        fichaCadastralVM.DadosBancarios = Mapper.Map<List<BancoDoFornecedor>, List<DadosBancariosVM>>(contratanteFornecedor.BancoDoFornecedor.ToList());
                        fichaCadastralVM.DadosEnderecos = Mapper.Map<List<FORNECEDOR_ENDERECO>, List<DadosEnderecosVM>>(contratanteFornecedor.WFD_PJPF_ENDERECO.ToList());
                        fichaCadastralVM.DadosContatos = Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(contratanteFornecedor.WFD_PJPF_CONTATOS.ToList());

                        fichaCadastralVM.Questionarios = new RetornoQuestionario<QuestionarioVM>
                        {
                            QuestionarioDinamicoList = Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                                            _cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                                            {
                                                ContratanteId = contranteId,
                                                PapelId = _papelBP.BuscarPorContratanteETipoPapel(contranteId, (int)EnumTiposPapel.Solicitante).ID,
                                                CategoriaId = contratanteFornecedor.CATEGORIA_ID ?? 0,
                                                Alteracao = true,
                                                SolicitacaoId = 0,
                                                FornecedorId = pjpf.ID,
                                                ContratantePJPFId = fichaCadastralVM.ContratanteFornecedorID
                                            })
                                   ),
                            HabilitaBotoesEdicao = true
                        };

                        //SE FORNECEDOR BLOQUEADO
                        ViewBag.StatusFornecedor = contratanteFornecedor.PJPF_STATUS_ID; //Se o fornecedor estiver desbloqueado
                        if (contratanteFornecedor.PJPF_STATUS_ID == 2)
                        {
                            var solicitacao = Db.WFD_SOLICITACAO.Include("SOLICITACAO_BLOQUEIO.TipoDeFuncaoDuranteBloqueio").FirstOrDefault(x => x.ID == contratanteFornecedor.PJPF_STATUS_ID_SOL);

                            foreach (var item in solicitacao.SOLICITACAO_BLOQUEIO)
                            {
                                string lanc = item.BLQ_LANCAMENTO_TODAS_EMP != null ? (bool)item.BLQ_LANCAMENTO_TODAS_EMP ? "1" : "2" : null;
                                fichaCadastralVM.DadosBloqueio = new DadosBloqueioVM
                                {
                                    ID = item.ID,
                                    Compra = (bool)item.BLQ_COMPRAS_TODAS_ORG_COMPRAS,
                                    ContratanteID = solicitacao.CONTRATANTE_ID,
                                    FornecedorID = solicitacao.PJPF_ID,
                                    Lancamento = lanc,
                                    Motivo = item.BLQ_QUALIDADE_FUNCAO_BQL_ID,
                                    MotivoQualidade = item.TipoDeFuncaoDuranteBloqueio != null ? item.TipoDeFuncaoDuranteBloqueio.FUNCAO_BLOQ_DSC : string.Empty,
                                    MotivoSolicitacao = item.BLQ_MOTIVO_DSC,
                                    SolicitacaoID = solicitacao.ID
                                };
                            }
                        }
                    }
                }

                fichaCadastralVM.Categoria = new CategoriaFichaVM
                {
                    Id = (int)fichaCadastralVM.CategoriaId
                    ,
                    Nome = fichaCadastralVM.CategoriaId.ToString()
                };

                return View(fichaCadastralVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
                ViewBag.erro = "Não foi possível acessar a Ficha Cadastral!";
                return View(ficha);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult FornecedorModificacaoFrm(string chaveurl)
        {
            ViewBag.ChaveUrl = chaveurl;
            int? usuarioId = (int?)Geral.PegaAuthTicket("UsuarioId");
            try
            {
                FichaCadastralWebForLinkVM fichaCadastralVM = RetornarModeloFornecedorModificacaoFrm(chaveurl, usuarioId);

                return View(fichaCadastralVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Alerta", "Home");
            }
        }

        private FichaCadastralWebForLinkVM RetornarModeloFornecedorModificacaoFrm(string chaveurl, int? usuarioId)
        {
            int fornecedorID = int.MinValue;
            int contratanteID = int.MinValue;
            int papelID = (int)EnumPapeisWorkflow.Solicitante;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

                Int32.TryParse(param.First(p => p.Name == "FornecedorID").Value, out fornecedorID);
                Int32.TryParse(param.First(p => p.Name == "ContratanteID").Value, out contratanteID);

                // Este parametro é utilizado quando o fornecedor atualiza algum documento e o retorno é sucesso para apresentar alerta.
                if (param.Any(p => p.Name == "RetSucessoDocs"))
                    ViewBag.RetSucessoDocs = param.First(p => p.Name == "RetSucessoDocs").Value;

                // Este parametro é utilizado quando o fornecedor atualiza algum comprovante e o retorno é sucesso para apresentar alerta.
                if (param.Any(p => p.Name == "RetSucessoBancos"))
                    ViewBag.RetSucessoBancos = param.First(p => p.Name == "RetSucessoBancos").Value;
            }

            Fornecedor fornecedor = _fornecedorService.Buscar(y => y.ID == fornecedorID);

            FichaCadastralWebForLinkVM fichaCadastralVM = new FichaCadastralWebForLinkVM(true);
            fichaCadastralVM = Mapper.Map<FichaCadastralWebForLinkVM>(fornecedor);

            fichaCadastralVM.ActionOrigem = "FornecedorModificacaoFrm";
            fichaCadastralVM.ControllerOrigem = "Fornecedores";
            fichaCadastralVM.ContratanteID = fornecedor.CONTRATANTE_ID;
            fichaCadastralVM.ContratanteFornecedorID = fornecedor.ID;
            fichaCadastralVM.HabilitaEdicao = true;
            fichaCadastralVM.PJPFID = fornecedorID;

            var contratanteFornecedor = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault();

            var dadosBancarios = contratanteFornecedor.BancoDoFornecedor.ToList();
            fichaCadastralVM.DadosBancarios = Mapper.Map<List<BancoDoFornecedor>, List<DadosBancariosVM>>(dadosBancarios);

            var dadosEnderecos = contratanteFornecedor.WFD_PJPF_ENDERECO.ToList();
            fichaCadastralVM.DadosEnderecos = Mapper.Map<List<FORNECEDOR_ENDERECO>, List<DadosEnderecosVM>>(dadosEnderecos);

            var dadosContatos = contratanteFornecedor.WFD_PJPF_CONTATOS.ToList();
            fichaCadastralVM.DadosContatos = Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(dadosContatos);

            fichaCadastralVM.SolicitacaoFornecedor = new SolicitacaoFornecedorVM()
            {
                Documentos = Mapper.Map<List<DocumentosDoFornecedor>, List<SolicitacaoDocumentosVM>>(contratanteFornecedor.WFD_PJPF_DOCUMENTOS.ToList())
            };

            fichaCadastralVM.FornecedorRobo = new FornecedorRoboVM();
            this.FornecedorRobo(fichaCadastralVM, fornecedor);

            //Mapear UNSPSC
            fichaCadastralVM.FornecedoresUnspsc = Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(fornecedor.FornecedorServicoMaterialList.Where(x => x.DT_EXCLUSAO == null).ToList());
            ViewBag.DataAtuUnspsc = fichaCadastralVM.DataAtualizacaoUnspsc;

            fichaCadastralVM.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                            Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                                _cadastroUnicoBP.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                                {
                                    ContratanteId = contratanteID,
                                    PapelId = papelID,
                                    CategoriaId = contratanteFornecedor.CATEGORIA_ID ?? 0,
                                    Alteracao = true,
                                    SolicitacaoId = 0,
                                    FornecedorId = fornecedorID,
                                    ContratantePJPFId = fichaCadastralVM.ContratanteFornecedorID
                                })
                       ),
                HabilitaBotoesEdicao = true
            };
            List<int> contratantesForn = Db.WFD_CONTRATANTE_PJPF.Where(c => c.PJPF_ID == fornecedorID).Select(cc => cc.CONTRATANTE_ID).ToList();
            List<Contratante> contratantes = Db.Contratante.Where(c => c.WFD_USUARIO1.Any(u => u.ID == usuarioId) && !contratantesForn.Contains(c.ID)).ToList();
            fichaCadastralVM.Categoria = new CategoriaFichaVM
            {
                Id = (int)fichaCadastralVM.CategoriaId
                ,
                Nome = fichaCadastralVM.CategoriaId.ToString()
            };
            ViewBag.Ampliar = contratantes.Any(); //se houver ligação com os 4 contratantes do grupo ao qual o usuário pertence
            ViewBag.StatusFornecedor = contratanteFornecedor.PJPF_STATUS_ID; //Se o fornecedor estiver desbloqueado

            //SE FORNECEDOR BLOQUEADO
            if (contratanteFornecedor.PJPF_STATUS_ID == 2)
            {
                var solicitacao = Db.WFD_SOLICITACAO.Include("SOLICITACAO_BLOQUEIO.TipoDeFuncaoDuranteBloqueio").FirstOrDefault(x => x.ID == contratanteFornecedor.PJPF_STATUS_ID_SOL);

                foreach (var item in solicitacao.SOLICITACAO_BLOQUEIO)
                {
                    string lanc = item.BLQ_LANCAMENTO_TODAS_EMP != null ? (bool)item.BLQ_LANCAMENTO_TODAS_EMP ? "1" : "2" : null;
                    fichaCadastralVM.DadosBloqueio = new DadosBloqueioVM
                    {
                        ID = item.ID,
                        Compra = (bool)item.BLQ_COMPRAS_TODAS_ORG_COMPRAS,
                        ContratanteID = solicitacao.CONTRATANTE_ID,
                        FornecedorID = solicitacao.PJPF_ID,
                        Lancamento = lanc,
                        Motivo = item.BLQ_QUALIDADE_FUNCAO_BQL_ID,
                        MotivoQualidade = item.TipoDeFuncaoDuranteBloqueio != null ? item.TipoDeFuncaoDuranteBloqueio.FUNCAO_BLOQ_DSC : string.Empty,
                        MotivoSolicitacao = item.BLQ_MOTIVO_DSC,
                        SolicitacaoID = solicitacao.ID
                    };
                }
            }
            return fichaCadastralVM;
        }
        #endregion Modificação Fornecedores

        [HttpPost]
        [Authorize]
        public ContentResult UploadArquivoFornecedor(string cnpj_cpf, string arqTmp)
        {
            var caminho = _configService.BuscarConfigGeral().CAMINHO_ARQUIVOS;
            return base.UploadArquivo(cnpj_cpf, arqTmp, caminho);
        }

        public JsonResult ValidarInternaCNPJ(string cnpj, int contratante, int tipoFornecedor, int categoria)
        {
            int codigo = 1;
            string mensagem = "";
            RetornarMensagemValidacaoDocumento(cnpj, tipoFornecedor, categoria, ref codigo, ref mensagem);

            return Json(new { code = codigo, message = mensagem }, JsonRequestBehavior.AllowGet);
        }

        private void RetornarMensagemValidacaoDocumento(string cnpj, int tipoFornecedor, int categoria, ref int codigo, ref string mensagem)
        {
            if (tipoFornecedor == 1 || tipoFornecedor == 3)
            {
                if (string.IsNullOrEmpty(cnpj))
                {
                    codigo = 0;
                    if (tipoFornecedor == 1)
                        mensagem = "CNPJ Obrigatório";
                    else
                        mensagem = "CPF Obrigatório";
                }
                else
                {
                    if (tipoFornecedor == 1)
                    {
                        if (!Validacao.ValidaCNPJ(cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
                        {
                            codigo = 0;
                            mensagem = "CNPJ Inválido";
                        }
                    }
                    else
                    {
                        if (!Validacao.ValidaCPF(cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
                        {
                            codigo = 0;
                            mensagem = "CPF Inválido";
                        }
                    }
                }

                if (codigo == 1)
                {
                    var cnpjteste = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");
                    if (_fornecedorService.Buscar(x => x.CNPJ == cnpjteste || x.CPF == cnpjteste) != null)
                    {
                        codigo = 0;
                        mensagem = "Já existe um fornecedor cadastrado com esse CNPJ/CPF!";
                    }
                    else if (_solicitacaoCadastroFornecedorService.Buscar(x => x.CNPJ == cnpjteste || x.CPF == cnpjteste) != null)
                    {
                        codigo = 0;
                        mensagem = "Já existe uma solicitação de criação para este CNPJ/CPF!";
                    }
                }
                if (codigo == 1)
                {
                    int totalDocCatecoria = Db.WFD_PJPF_CATEGORIA.Include(a => a.ListaDeDocumentosDeFornecedor).FirstOrDefault(c => c.ID == categoria).ListaDeDocumentosDeFornecedor.Count;

                    if (totalDocCatecoria == 0)
                    {
                        codigo = 0;
                        mensagem = "Não é possível enviar esta solicitação. A Categoria/Grupo de Contas selecionado não possue Lista de Documento para solicitação!";
                    }
                }
            }
        }

        private ActionResult IncluirInclusaoFornecedor(FornecedoresVM model, int contratanteId, int usuarioId, out SOLICITACAO solicitacao, int papelAtual)
        {
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
                Assunto = "WebForLink",
                RazaoSocial = model.RazaoSocial,
                CPF = cnpj,
                DataNascimento = model.DataNascimento
            };

            solicitacao = _solicitacaoService.CadastrarSolicitacaoNovoFornecedor(modeloCadastro);

            ViewBag.SolicitacaoId = solicitacao.ID;

            if (model.TipoFornecedor != (int)EnumTiposFornecedor.EmpresaEstrangeira) // SE NACIONAL
            {
                _tramite.AtualizarTramite(contratanteId, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aguardando, usuarioId);
                ViewBag.Robo = true;

                model.RoboReceitaCNPJ = new RoboReceitaCNPJ();
                model.RoboSintegra = new RoboSintegra();
                model.RoboSimples = new RoboSimples();
                model.RoboReceitaCPF = new RoboReceitaCPF();

                model.RoboReceitaCNPJ.cssCor = "default";
                model.RoboSintegra.cssCor = "default";
                model.RoboSimples.cssCor = "default";

                return View(model);
            }

            return RedirectToAction("FornecedoresDiretoFrm", "FornecedoresDireto", new { chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}", solicitacao.ID), Key) });
        }

        private ActionResult AlterarInclusaoFornecedorIncluindoRobo(FornecedoresVM model, int? SolicitacaoID, int Empresa, int contratanteId, int usuarioId, out SOLICITACAO solicitacao, int papelAtual)
        {
            solicitacao = _solicitacaoService.BuscarPorIdComSolicitacaoCadastroFornecedor((int)SolicitacaoID);
            solicitacao.IncluirRoboCriacaoFornecedor();

            //var robo = solicitacao.ROBO.FirstOrDefault();

            //if (robo != null)
            //{
            //    if (robo.RF_CONSULTA_DTHR != null && robo.SINT_CONSULTA_DTHR != null && robo.SN_CONSULTA_DTHR != null)
            //    {
            //        solicitacao.ROBO_EXECUTADO = true;
            _solicitacaoService.Alterar(solicitacao);
            //    }
            //}

            if (model.TipoCadastro == (int)EnumTiposPreenchimento.Fornecedor)
            {
                _tramite.AtualizarTramite(contratanteId, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aprovado, usuarioId);
                return RedirectToAction("SolicitacaoEnviar", "Documento", new
                {
                    chaveUrl = Cripto.Criptografar(string.Format("SolicitacaoID={0}", solicitacao.ID), Key),
                    nomeContato = model.NomeContato,
                    emailContato = model.Email,
                    contratanteId = Empresa,
                    documentoPfPj = model.CNPJ
                });
            }

            return RedirectToAction("FornecedoresDiretoFrm", "FornecedoresDireto",
                new
                {
                    chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}", solicitacao.ID), Key)
                });
        }

        private ActionResult CancelarInclusaoFornecedor(int? SolicitacaoID, int contratanteId, int usuarioId, out SOLICITACAO solicitacao, int papelAtual)
        {
            solicitacao = _solicitacaoService.BuscarPorIdIncluindoFluxo((int)SolicitacaoID);

            _tramite.AtualizarTramite(contratanteId, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, 6, usuarioId);

            return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = "Solicitação Cancelada!" });
        }

        //---
        /*
        public ActionResult ConsultaFornecedoresResult(string chaveurl, string pagina, bool meusFornecedoresCheck)
        {
            string codigoCliente = (string)Geral.PegaAuthTicket("CodigoCliente");

            string tip = string.Empty;
            string tipo = string.Empty;
            string grupo = string.Empty;
            string subGrupo = string.Empty;
            string nomeBasico = string.Empty;
            string anterior = string.Empty;
            string nomeModificador = string.Empty;

            if (chaveurl != null)
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                tip = param.FirstOrDefault(p => p.Name == "Tip").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "Tip").Value;
                tipo = param.FirstOrDefault(p => p.Name == "Tipo").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "Tipo").Value;
                grupo = param.FirstOrDefault(p => p.Name == "Grupo").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "Grupo").Value;
                subGrupo = param.FirstOrDefault(p => p.Name == "SubGrupo").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "SubGrupo").Value;
                nomeBasico = param.FirstOrDefault(p => p.Name == "NomeBasico").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "NomeBasico").Value;
                anterior = param.FirstOrDefault(p => p.Name == "Anterior").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "Anterior").Value;
                nomeModificador = param.FirstOrDefault(p => p.Name == "NomeModificador").Value == string.Empty ? string.Empty : param.FirstOrDefault(p => p.Name == "NomeModificador").Value;
            }

            try
            {
                List<Vendor_List.Models.PROC_FORNECEDORES_MAT_Result> listaFornecedores =
                    dbContext.PROC_FORNECEDORES_MAT("PRT", tip, grupo, subGrupo, nomeBasico, nomeModificador, "0", codigoCliente, meusFornecedoresCheck == true ? "1" : "0").ToList();

                List<ResultadoMateriaisVM> LstMat = new List<ResultadoMateriaisVM>();
                ResultadoMateriaisVM modelo = new ResultadoMateriaisVM();

                int pag = 0;
                if (string.IsNullOrEmpty(pagina))
                    pag = 1;
                else
                    int.TryParse(pagina, out pag);

                var pagination = new Pagination(listaFornecedores.Count(), pag);

                listaFornecedores.Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .ToList()
                    .ForEach(x =>
                    {
                        modelo.Grid.Add(new GridConsultaFornecedorBuscaVM()
                        {
                            CodigoGrupo = grupo,
                            //CodigoMaterial = x.cod_material,
                            CodigoSubgrupo = subGrupo,
                            CodigoTipo = tipo,
                            NomeBasico = nomeBasico,
                            Fornecedor = x.RAZAO_SOCIAL,
                            FornecedorDocumento = x.CNPJ_CPF,
                            FornecedorNomeContato = x.NOME_CONTATO,
                            //FornecedorEmail = x.EMAIL,
                            //Detalhes = x.DSC_MATERIAL_TRAD,
                            FornecedorTelefone = "(" + x.DDD_TEL + ")" + x.TELEFONE,
                            FornecedorNumeroCliente = 2,
                            FornecedorCodigoSap = x.COD_FORNECEDOR_CLIENTE,
                            MeusFornecedores = x.MeusFornecedores,
                            FornecedorClienteBase = x.Forn_cliente_base.ToString(),
                            NomeFantasia = x.NOME_FANTASIA.ToString(),
                            ReferenciaFornecedor = x.cod_referencia,
                            Ficha = x.SituacaoRobo,
                            Convite = x.ConviteFornecedor,
                            Estado = x.ESTADO,
                            FornecedorStatus = x.StatusFornecedor,
                            FornecedorLogo = x.LogoFornecedor
                        });
                    });

                modelo.Pagination = pagination;
                modelo.ParametrosCriptografados = chaveurl;
                ViewBag.CheckMeusFornecedores = meusFornecedoresCheck;
                return View(modelo);
            }
            catch (Exception ex)
            {
                customException.GravaLog(ex);
            }

            return null;
        }
        */

    }
}