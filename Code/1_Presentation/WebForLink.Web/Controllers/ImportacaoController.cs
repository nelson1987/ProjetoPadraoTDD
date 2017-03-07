using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    public class ImportacaoController : ControllerPadrao
    {
        public readonly IFornecedorBaseImportacaoWebForLinkAppService _fornecedorBaseImportacaoService;
        public readonly IFornecedorBaseWebForLinkAppService _fornecedorBaseService;
        public readonly IConfiguracaoEmailContratanteWebForLinkAppService _contratanteConfiguracaoEmailService;
        public readonly IImportacaoWebForLinkAppService _importacaoService;
        public readonly ISolicitacaoProrrogacaoPrazoWebForLinkAppService _solicitacaoProrrogacaoService;
        public readonly IFornecedorCategoriaWebForLinkAppService _fornecedorCategoriaService;
        public readonly IPapelWebForLinkAppService _papelService;
        public readonly IFluxoWebForLinkAppService _fluxoService;
        public readonly IUsuarioWebForLinkAppService _usuarioService;
        public readonly IContratanteWebForLinkAppService _contratanteService;
        public readonly ITramiteWebForLinkAppService _tramite;
        
        #region Constantes
        private const int LINHA_CABECALHO = 1;
        private const int LINHA_INICIO = 2;
        private const int PAGINA_DEFAULT = 1;
        private const int ITENS_POR_PAGINA = 10;

        private const string TEXT_CENTER = "text-center";
        private const string TEXT_LEFT = "text-left";
        private const string TEXT_RIGHT = "text-right";
        private const string TEXT_DANGER_TEXT_CENTER = "text-danger text-center";
        private const string TEXT_DANGER_TEXT_LEFT = "text-danger text-left";
        private const string TEXT_DANGER_TEXT_RIGHT = "text-danger text-right";
        private const string DANGER_TEXT_CENTER = "danger text-center";
        #endregion

        public ImportacaoController(
            IFornecedorBaseImportacaoWebForLinkAppService fornecedorBaseImportacao,
            IFornecedorBaseWebForLinkAppService fornecedorBase,
            IConfiguracaoEmailContratanteWebForLinkAppService configuracaoEmail,
            IImportacaoWebForLinkAppService importacao,
            ISolicitacaoProrrogacaoPrazoWebForLinkAppService solicitacaoProrrogacao,
            IFornecedorCategoriaWebForLinkAppService fornecedorCategoriaService,
            IPapelWebForLinkAppService papel,
            IFluxoWebForLinkAppService fluxo,
            IUsuarioWebForLinkAppService usuario,
            IContratanteWebForLinkAppService contratante,
            ITramiteWebForLinkAppService tramite            
            )
        {
            try
            {
                _fornecedorBaseImportacaoService = fornecedorBaseImportacao;
                _fornecedorBaseService = fornecedorBase;
                _contratanteConfiguracaoEmailService = configuracaoEmail;
                _importacaoService = importacao;
                _solicitacaoProrrogacaoService = solicitacaoProrrogacao;
                _fornecedorCategoriaService = fornecedorCategoriaService;
                _papelService = papel;
                _fluxoService = fluxo;
                _usuarioService = usuario;
                _contratanteService = contratante;
                _tramite = tramite;                
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        [Authorize]
        public ActionResult Listar(FornecedorBaseListaVM model)
        {
            int pagina = model.Pagina ?? 1;
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            ImportacaoFornecedoresFiltrosDTO filtro = null;

            this.MapearFiltro(model, ref filtro);

            var retorno = _fornecedorBaseService.PesquisarFornecedores(filtro, pagina, ITENS_POR_PAGINA);

            model.FornecedoresBase = Mapper.Map<List<FornecedorBaseVM>>(retorno.RegistrosPagina);
            model.Filtro = Mapper.Map<ImportacaoFornecedoresFiltrosDTO, FornecedorBaseFiltroVM>(filtro);
            model.Topo = Mapper.Map<FornecedorBaseTopoVM>(_fornecedorBaseService.PesquisarFornecedoresBaseTopo(filtro.ContratanteId));
            model.Timeline = Mapper.Map<TimelineVM>(_fornecedorBaseService.RetornarIndicesTimeLine(filtro.ContratanteId));
            model.Arquivos = Mapper.Map<List<SelectListItem>>(_fornecedorBaseImportacaoService.ListarTodas(contratanteId));
            model.Arquivos.Insert(0, new SelectListItem { Text = "Todas", Value = null });
            model.FornecedoresBase.ForEach(x =>
            {
                if (Request.Url == null) return;

                x.UrlEditar = Url.Action("Editar", "Importacao",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("ID={0}", x.ID), Key)
                    }, Request.Url.Scheme);
                x.UrlExcluir = Url.Action("Excluir", "Importacao",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("ID={0}", x.ID), Key)
                    }, Request.Url.Scheme);
            });

            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = retorno.TotalPaginas;
            ViewBag.TotalRegistros = retorno.TotalRegistros;

            this.PersistirDadosEmMemoria();

            return View(model);
        }

        #region Carregar Arquivo
        [Authorize]
        public ActionResult CarregarArquivo()
        {
            this.PersistirDadosEmMemoria();

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CarregarArquivo(DadosImportacaoVM model)
        {
            if (ModelState.IsValid)
            {
                this.ValidarExtensaoArquivo(model.Arquivo);

                if (ModelState.IsValid)
                {
                    MemoryStream target = new MemoryStream();

                    model.Arquivo.InputStream.CopyTo(target);

                    using (ExcelPackage package = new ExcelPackage(target))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                        this.ValidarCabecalhoArquivoImportado(worksheet);

                        if (ModelState.IsValid)
                        {
                            var linhasArquivoImportado = this.LerArquivo(model, worksheet);
                            var relatorioLeituraVM = new RelatorioLeituraVM(linhasArquivoImportado);

                            TempData["RelatorioLeitura"] = relatorioLeituraVM;
                            TempData["Categoria"] = (model.Categoria.HasValue) ? model.Categoria.ToString() : null;
                            TempData["NomeArquivo"] = model.Arquivo.FileName;

                            return RedirectToAction("ExibirRelatorioLeitura", "Importacao");
                        }

                        package.Dispose();
                    }

                    target.Close();
                }
            }

            this.PersistirDadosEmMemoria();

            return View(model);
        }
        #endregion

        [Authorize]
        public ActionResult ExibirRelatorioLeitura()
        {
            var relatorioLeituraVM = TempData["RelatorioLeitura"] as RelatorioLeituraVM;

            TempData["LinhasSemErro"] = relatorioLeituraVM.LinhasSemErro;

            return View(relatorioLeituraVM);
        }

        [HttpPost]
        public ActionResult EfetivarImportacao()
        {
            var linhas = TempData["LinhasSemErro"] as List<LinhaPlanilhaModel>;
            var nomeArquivo = TempData["NomeArquivo"] as string;
            TempData.Keep("LinhasSemErro");

            int categoriaId = 0;
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            Int32.TryParse(TempData["Categoria"] as String, out categoriaId);

            var arquivoImportado = new FORNECEDORBASE_IMPORTACAO()
            {
                CONTRATANTE_ID = contratanteId,
                DT_UPLOAD = DateTime.Now,
                NOME_ARQUIVO = nomeArquivo,
                USUARIO_ID = usuarioId
            };

            var fornecedoresImportados = new List<FORNECEDORBASE>();

            foreach (var item in linhas)
            {
                var fornecedorBase = new FORNECEDORBASE();

                var cnpjOuCPF = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.CNPJouCPF)).First().ValorManipulado;
                var razaoSocialOuNome = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.RazaoSocialOuNome)).First().ValorManipulado;
                var dataNascimento = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.DataNascimento)).First().ValorManipulado;
                var nomeContato = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.NomeContato)).First().ValorManipulado;
                var email = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.Email)).First().ValorManipulado;
                var telefone = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.Telefone)).First().ValorManipulado;
                var celular = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.Celular)).First().ValorManipulado;
                var novoFornecedor = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.NovoFornecedor)).First().ValorManipulado;
                var codigoErp = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.CodigoERP)).First().ValorManipulado;
                var inscricaoEstadual = item.Celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.InscricaoEstadual)).First().ValorManipulado;


                bool isCNPJValido = Validacao.ValidaCNPJ(cnpjOuCPF);
                bool isCPFValido = Validacao.ValidaCPF(cnpjOuCPF);
                bool isDataNascimentoValida = Validacao.ValidarData(dataNascimento);

                if (isCNPJValido)
                {
                    fornecedorBase.CNPJ = cnpjOuCPF;
                    fornecedorBase.RAZAO_SOCIAL = razaoSocialOuNome;
                }
                else if (isCPFValido)
                {
                    fornecedorBase.CPF = cnpjOuCPF;
                    fornecedorBase.NOME = razaoSocialOuNome;
                }

                if (isDataNascimentoValida)
                    fornecedorBase.DT_NASCIMENTO = DateTime.Parse(dataNascimento);

                fornecedorBase.NOME_CONTATO = nomeContato;
                fornecedorBase.EMAIL = email;
                fornecedorBase.TELEFONE = telefone;
                fornecedorBase.CELULAR = celular;

                if (categoriaId > 0)
                    fornecedorBase.CATEGORIA_ID = categoriaId;

                fornecedorBase.CONTRATANTE_ID = contratanteId;
                fornecedorBase.COD_ERP = codigoErp;


                fornecedoresImportados.Add(fornecedorBase);

            }
            var fornecedoresJaExistentes = _fornecedorBaseService.IncluirFornecedoresBase(arquivoImportado, (int)Geral.PegaAuthTicket("ContratanteId"), fornecedoresImportados);

            var fornecedoresImportadosComSucesso = new List<LinhaPlanilhaModel>();

            int indexador = 0;
            fornecedoresImportados.ForEach(x =>
            {
                if (!fornecedoresJaExistentes.Contains(x))
                    fornecedoresImportadosComSucesso.Add(linhas.ElementAt(indexador));
                indexador++;
            });

            TempData["FornecedoresImportadosComSucesso"] = fornecedoresImportadosComSucesso;

            return RedirectToAction("ExibirRelatorioImportacao", "Importacao");
        }

        [Authorize]
        public ActionResult ExibirRelatorioImportacao()
        {
            var linhas = TempData["FornecedoresImportadosComSucesso"] as List<LinhaPlanilhaModel>;

            var relatorioImportacaoVM = new RelatorioImportacaoVM();

            relatorioImportacaoVM.Colunas = (EnumHelper.GetDescriptions<EnumColunasPlanilha>());
            relatorioImportacaoVM.Colunas.Insert(0, " ");

            relatorioImportacaoVM.Linhas = linhas;

            return View(relatorioImportacaoVM);
        }

        #region Editar
        [HttpGet]
        [Authorize]
        public ActionResult Editar(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int id;

            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "ID").Value, out id);

            var fornecedorBase = Mapper.Map<FornecedorBaseVM>(_fornecedorBaseService.BuscarPorId(id));
            ViewBag.CategoriaSelecionada = fornecedorBase.CategoriaId;
            ViewBag.CategoriaSelecionadaNome = fornecedorBase.CategoriaNome;

            if (fornecedorBase == null)
                return HttpNotFound();

            this.AplicarMascaras(ref fornecedorBase);

            this.PersistirDadosEmMemoria();

            return View(fornecedorBase);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Editar(FornecedorBaseVM model, int? CategoriaSelecionada, string CategoriaSelecionadaNome)
        {
            FORNECEDORBASE fornecedorBase = Mapper.Map<FornecedorBaseVM, FORNECEDORBASE>(model);
            ViewBag.CategoriaSelecionada = CategoriaSelecionada;
            ViewBag.CategoriaSelecionadaNome = CategoriaSelecionadaNome;
            fornecedorBase.CATEGORIA_ID = CategoriaSelecionada;

            this.RemoverMascaras(ref fornecedorBase);
            try
            {
                _fornecedorBaseService.Atualizar(fornecedorBase);
                TempData["MensagemSucesso"] = "Fornecedor alterado com sucesso!";
                return RedirectToAction("Listar", "Importacao");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                TempData["MensagemSucesso"] = "Erro ao tentar alterar fornecedor.";
            }
            return View(model);
        }
        #endregion

        #region Excluir

        [HttpGet]
        [Authorize]
        public ActionResult Excluir(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int id;

            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "ID").Value, out id);

            var fornecedorBase = Mapper.Map<FornecedorBaseVM>(_fornecedorBaseService.BuscarPorId(id));

            if (fornecedorBase == null)
                return HttpNotFound();

            this.AplicarMascaras(ref fornecedorBase);

            return View(fornecedorBase);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Excluir(FornecedorBaseVM model)
        {
            _fornecedorBaseService.Excluir(model.ID);

            return RedirectToAction("Listar", "Importacao");
        }
        #endregion

        #region Executar Funcionalidade
        [Authorize]
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ExecutarFuncionalidade(string TipoFuncionalidade)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            FornecedorBaseListaVM model = new FornecedorBaseListaVM();
            if (TipoFuncionalidade == "ValidarEmOrgaosPublicos")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.ValidarEmOrgaosPublicos;
            if (TipoFuncionalidade == "Categorizar")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.Categorizar;
            if (TipoFuncionalidade == "Convidar")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.Convidar;
            if (TipoFuncionalidade == "Bloquear")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.Bloquear;
            if (TipoFuncionalidade == "ProrrogarPrazo")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.ProrrogarPrazo;
            if (TipoFuncionalidade == "GerarCarga")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.GerarCarga;
            if (TipoFuncionalidade == "CompletarDados")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.CompletarDados;
            if (TipoFuncionalidade == "AprovarPrazo")
                model.TipoFuncionalidade = EnumTiposFuncionalidade.AprovarPrazo;

            int pagina = model.Pagina ?? 1;
            ImportacaoFornecedoresFiltrosDTO filtro = null;

            this.MapearFiltro(model, ref filtro);
            this.ManipularFiltroEspecifico(model.TipoFuncionalidade, ref filtro);

            //var funcao = Request.Form["Funcao"];

            var configEmail = _contratanteConfiguracaoEmailService.BuscarPorContratanteETipo(contratanteId, 1);
            this.Preenchermodelo(contratanteId, model, configEmail, DateTime.Today.ToShortDateString());
            model.Mensagem = model.MensagemImportacao.Mensagem;
            model.Assunto = model.MensagemImportacao.Assunto;

            var retorno = _fornecedorBaseService.PesquisarFornecedores(filtro, pagina, ITENS_POR_PAGINA);

            model.Filtro = Mapper.Map<FornecedorBaseFiltroVM>(filtro);
            model.FornecedoresBaseFuncionalidade = Mapper.Map<List<FornecedorBaseFuncionalidadeVM>>(retorno.RegistrosPagina);

            if (filtro.Aprovados != null)
            {
                foreach (var item in retorno.RegistrosPagina)
                {
                    var subitemVM = model.FornecedoresBaseFuncionalidade.Single(x => x.ID == item.ID);
                    var subitemMD = item.WFD_SOLICITACAO.LastOrDefault().WFD_SOLICITACAO_PRORROGACAO.FirstOrDefault(x => x.APROVADO == null);
                    subitemVM.ProrrogarPara = subitemMD.DT_PRORROGACAO_PRAZO.ToShortDateString();
                    subitemVM.Motivo = subitemMD.MOTIVO_PRORROGACAO;
                }
            }

            this.AplicarValores(ref model);

            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = retorno.TotalPaginas;
            ViewBag.TotalRegistros = retorno.TotalRegistros;

            this.PersistirDadosEmMemoria();

            return View(model);
        }

        [Authorize]
        [ValidateInput(false)]
        public ActionResult ExecutarFuncionalidade(FornecedorBaseListaVM model, int? CategoriaSelecionada, string CategoriaSelecionadaNome)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            try
            {
                if (!ModelState.IsValid) { }
                int pagina = model.Pagina ?? 1;
                ImportacaoFornecedoresFiltrosDTO filtro = null;

                if (CategoriaSelecionada != null)
                    model.CategoriaId = (int)CategoriaSelecionada;

                this.MapearFiltro(model, ref filtro);
                this.ManipularFiltroEspecifico(model.TipoFuncionalidade, ref filtro);

                var funcao = Request.Form["Funcao"];

                var configEmail = _contratanteConfiguracaoEmailService.BuscarPorContratanteETipo(contratanteId, 1);

                this.Preenchermodelo(contratanteId, model, configEmail, model.StDataProrrogacao);

                if (!String.IsNullOrEmpty(funcao))
                {
                    var selecionados = model.Selecionados.Split(',').Select(Int32.Parse).ToList();
                    var funcaoEnum = (EnumTiposFuncionalidade)Enum.Parse(typeof(EnumTiposFuncionalidade), funcao);
                    //int intfuncao = Convert.ToInt32(funcao);
                    //switch ((EnumTiposFuncionalidade)funcao.ToEnum<EnumTiposFuncionalidade>())
                    switch (funcaoEnum)
                    {
                        case EnumTiposFuncionalidade.ValidarEmOrgaosPublicos:
                            this.AtivarExecucaoRobo(selecionados.ToArray());
                            break;
                        case EnumTiposFuncionalidade.Categorizar:
                            this.Categorizar(selecionados.ToArray(), model.CategoriaId);
                            break;
                        case EnumTiposFuncionalidade.Convidar:
                            this.Convidar(selecionados, model.Mensagem, model.Assunto, usuarioId);
                            break;
                        case EnumTiposFuncionalidade.ProrrogarPrazo:
                            this.ProrrogarPrazo(selecionados, model.Motivo, DateTime.Parse(model.StDataProrrogacao), usuarioId);
                            break;
                        case EnumTiposFuncionalidade.AprovarPrazo:
                            this.AvaliarPrazo(selecionados, model.Motivo, usuarioId, EnumTiposFuncionalidade.AprovarPrazo);
                            break;
                        case EnumTiposFuncionalidade.ReprovarPrazo:
                            this.AvaliarPrazo(selecionados, model.Motivo, usuarioId, EnumTiposFuncionalidade.ReprovarPrazo);
                            break;
                        case EnumTiposFuncionalidade.Bloquear:
                            this.Bloquear(selecionados, model.BloqueioId, usuarioId, contratanteId);
                            break;
                    }
                }

                model.Selecionados = string.Empty;

                var retorno = _fornecedorBaseService.PesquisarFornecedores(filtro, pagina, ITENS_POR_PAGINA);

                model.Mensagem = model.MensagemImportacao.Mensagem;
                model.Assunto = model.MensagemImportacao.Assunto;
                model.AprovaPrazo = true;

                model.Filtro = Mapper.Map<FornecedorBaseFiltroVM>(filtro);

                model.FornecedoresBaseFuncionalidade = Mapper.Map<List<FornecedorBaseFuncionalidadeVM>>(retorno.RegistrosPagina);
                if (filtro.Aprovados != null)
                {
                    foreach (var item in retorno.RegistrosPagina)
                    {
                        var subitemVM = model.FornecedoresBaseFuncionalidade.Single(x => x.ID == item.ID);
                        var subitemMD = item.WFD_SOLICITACAO.LastOrDefault().WFD_SOLICITACAO_PRORROGACAO.FirstOrDefault(x => x.APROVADO == null);
                        subitemVM.ProrrogarPara = subitemMD.DT_SOL_PRORROGACAO.ToShortDateString();
                        subitemVM.Motivo = subitemMD.MOTIVO_PRORROGACAO;
                    }
                }


                this.AplicarValores(ref model);

                ViewBag.Pagina = pagina;
                ViewBag.TotalPaginas = retorno.TotalPaginas;
                ViewBag.TotalRegistros = retorno.TotalRegistros;

                this.PersistirDadosEmMemoria();
                model.Selecionados = "";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return View(model);
        }

        #endregion

        [HttpGet]
        public JsonResult ProrrogarPrazo(int solicitacaoId, string motivoProrrogacao, string dataProrrogacao)
        {
            try
            {
                if (_solicitacaoProrrogacaoService.BuscarPorId(solicitacaoId) == null)
                {
                    DateTime data = DateTime.Parse(dataProrrogacao);
                    _importacaoService.ProrrogarPrazo(solicitacaoId, (int)Geral.PegaAuthTicket("UsuarioId"), data, motivoProrrogacao);
                    return Json(new
                    {
                        status = true,
                        mensagem = "Solicitação de prorrogação realizada com sucesso!",
                        dataProrrogacao = dataProrrogacao,
                        statusGravacao = "Aguardando Aprovação...",
                        dataSolicitacao = DateTime.Now.ToShortDateString()
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = false, mensagem = "Já existe uma solicitação de prorrogação, favor aguardar resultado." }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return Json(new { status = false, mensagem = "Erro, na tentativa de prorrogação." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}