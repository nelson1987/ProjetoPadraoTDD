using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    [WebForLinkFilter]
    public class AprovacaoController : ControllerPadrao
    {
        #region Chamadas Para BP
        protected internal IPapelWebForLinkAppService _papelService;
        protected internal ISolicitacaoWebForLinkAppService _solicitacaoService;
        protected internal ICadastroUnicoWebForLinkAppService _cadastroUnicoService;
        protected internal IBancoWebForLinkAppService _bancoService;
        protected internal ITipoBloqueioRoboWebForLinkAppService _tipoBloqueioRoboService;
        protected internal IAprovacaoWebForLinkAppService _aprovacaoService;
        protected internal ITramiteWebForLinkAppService _tramite;
        protected internal IFluxoSequenciaWebForLinkAppService _fluxoSequenciaBp;
        protected internal IGrupoWebForLinkAppService _grupoBp;
        protected internal IEnderecoWebForLinkAppService _enderecoService;
        protected internal IFornecedorWebForLinkAppService _fornecedorService;
        protected internal IFornecedorRoboWebForLinkAppService _roboService;
        #endregion

        public int? paginaAtual { get; private set; }

        public AprovacaoController(
            IPapelWebForLinkAppService papel,
            ISolicitacaoWebForLinkAppService solicitacao,
            ICadastroUnicoWebForLinkAppService cadastroUnico,
            IBancoWebForLinkAppService banco,
            ITipoBloqueioRoboWebForLinkAppService tipoBloqueioRobo,
            IAprovacaoWebForLinkAppService aprovacao, 
            ITramiteWebForLinkAppService tramite,
            IFluxoSequenciaWebForLinkAppService fluxoSequencia,
            IGrupoWebForLinkAppService grupo,
            IEnderecoWebForLinkAppService enderecoService,
            IFornecedorWebForLinkAppService fornecedor,
            IFornecedorRoboWebForLinkAppService robo )
        {
            _enderecoService = enderecoService;
            try
            {
                _papelService = papel;
                _solicitacaoService = solicitacao;
                _cadastroUnicoService = cadastroUnico;
                _bancoService = banco;
                _tipoBloqueioRoboService = tipoBloqueioRobo;
                _aprovacaoService = aprovacao;
                _tramite = tramite;
                _fluxoSequenciaBp = fluxoSequencia;
                _grupoBp = grupo;
                _fornecedorService = fornecedor;
                _roboService = robo;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        // GET: Aprovacao
        [Authorize]
        public ActionResult AprovacaoLst(int? Pagina, string MensagemSucesso)
        {
            int pagina = Pagina ?? 1;
            ViewBag.Pagina = pagina;
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";

            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            int[] papeis = _papelService.EmpilharPorUsuarioId(usuarioId);

            string chave = Path.GetRandomFileName().Replace(".", "");

            var lstWfdSolicitacao = _solicitacaoService.BuscarPesquisa(
                    x => x.WFD_SOLICITACAO_TRAMITE.Any(y => papeis.Contains(y.PAPEL_ID) && y.SOLICITACAO_STATUS_ID == 1),
                    TamanhoPagina,
                    pagina,
                    x => x.ID);

            List<AprovacaoVM> lstAprovacaoVM = new List<AprovacaoVM>();

            try
            {
                if (lstWfdSolicitacao.RegistrosPagina.Any())
                {
                    foreach (SOLICITACAO item in lstWfdSolicitacao.RegistrosPagina)
                    {
                        foreach (var tramite in item.WFD_SOLICITACAO_TRAMITE.Where(x => papeis.Contains(x.PAPEL_ID) && x.SOLICITACAO_STATUS_ID == 1))
                        {
                            AprovacaoVM aprovacaoVM = new AprovacaoVM
                            {
                                Solicitacao = new SOLICITACAO(),
                                Solicitacao_Tramites = new List<SOLICITACAO_TRAMITE>(),
                                Fornecedor = new SolicitacaoCadastroFornecedor(),
                                FluxoId = item.FLUXO_ID,
                                Contratante_ID = item.CONTRATANTE_ID,
                                NomeContratante = item.Contratante.RAZAO_SOCIAL,
                                Solicitacao_Dt_Cria = item.SOLICITACAO_DT_CRIA,
                                Login = item.Usuario != null ? item.Usuario.NOME : null,
                                IdSolicitacao = item.ID,
                                NomeSolicitacao = item.Fluxo.FLUXO_NM,
                                Solicitacao_Tramite = tramite
                            };

                            if (item.CadastroFornecedor(item))
                                if (item.SolicitacaoCadastroFornecedor != null)
                                    aprovacaoVM.NomeFornecedor = item.SolicitacaoCadastroFornecedor.First().PJPF_TIPO != 3
                                        ? item.SolicitacaoCadastroFornecedor.First().RAZAO_SOCIAL
                                        : item.SolicitacaoCadastroFornecedor.First().NOME;
                                else
                                    aprovacaoVM.NomeFornecedor = item.FORNECEDORBASE.PJPF_TIPO != 3
                                        ? item.FORNECEDORBASE.RAZAO_SOCIAL
                                        : item.FORNECEDORBASE.NOME;
                            else
                                if (item.Fornecedor != null)
                                aprovacaoVM.NomeFornecedor = item.Fornecedor.TIPO_PJPF_ID == 3
                                    ? item.Fornecedor.NOME
                                    : item.Fornecedor.RAZAO_SOCIAL;
                            else
                                aprovacaoVM.NomeFornecedor = item.FORNECEDORBASE.PJPF_TIPO != 3
                                    ? item.FORNECEDORBASE.RAZAO_SOCIAL
                                    : item.FORNECEDORBASE.NOME;

                            aprovacaoVM.UrlAprovacao = Url.Action("AprovacaoFrm", "Aprovacao", new { chaveurl = Cripto.Criptografar(string.Format("idSolicitacao={0}&idSolicitacaoTipo={1}&idPapel={2}", aprovacaoVM.IdSolicitacao.ToString(), aprovacaoVM.FluxoId.ToString(), tramite.PAPEL_ID, chave), Key) }, Request.Url.Scheme);

                            lstAprovacaoVM.Add(aprovacaoVM);
                        }
                    }
                    ViewBag.TotalPaginas = lstWfdSolicitacao.TotalPaginas;
                    ViewBag.TotalRegistros = lstWfdSolicitacao.TotalRegistros;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Erro ao buscar as solicitações");
                return View();
            }

            return View(lstAprovacaoVM);
        }

        [Authorize]
        public ActionResult AprovacaoFrm(string chaveurl)
        {
            int idSolicitacao = 0;
            int idSolicitacaoTipo = 0;
            int idPapel = 0;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "idSolicitacao").Value, out idSolicitacao);
                Int32.TryParse(param.First(p => p.Name == "idSolicitacaoTipo").Value, out idSolicitacaoTipo);
                Int32.TryParse(param.First(p => p.Name == "idPapel").Value, out idPapel);
            }

            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM(idSolicitacao);
            SOLICITACAO solicitacao = _solicitacaoService.BuscarAprovacaoPorId(idSolicitacao);

            int? grupoTramite = solicitacao.WFD_SOLICITACAO_TRAMITE.FirstOrDefault(t => t.PAPEL_ID == idPapel && t.SOLICITACAO_STATUS_ID == 1).GRUPO_DESTINO;
            ViewBag.NecessitaExecucaoManual = _fluxoSequenciaBp.NecessitaExecucaoManual(solicitacao.CONTRATANTE_ID, solicitacao.FLUXO_ID, idPapel, solicitacao.ID);
            ViewBag.QtdGrupoEmpresa = _grupoBp.QuantidadeEmpresa(solicitacao.CONTRATANTE_ID);

            switch (solicitacao.Fluxo.FLUXO_TP_ID)
            {
                // CADASTRO FORNECEDOR
                case 10:
                case 30:
                case 20:
                case 40:
                case 50:
                    this.CadastroFornecedor(ficha, solicitacao);
                    this.FornecedorRobo(ficha, solicitacao);
                    break;

                // AMPLIAÇÃO DE FORNECEDOR
                case 60:
                    this.AmpliacaoFornecedor(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // MODIFICAÇÕES GERAIS
                case 70:
                    this.ModificacoesGerais(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // MODIFICAÇÕES DE DADOS FISCAIS
                case 80:
                    this.ModificacoesDadosFiscais(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // MODIFICAÇÕES DE DADOS BANCÁRIOS
                case 90:
                    this.ModificacoesDadosBancarios(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // MODIFICAÇÕES DE DADOS CONTATOS
                case 100:
                    this.ModificacoesDadosContatos(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // BLOQUEIO DO FORNECEDOR
                case 110:
                    this.BloqueioFornecedor(ficha, solicitacao);
                    ViewBag.BloqueioMotivoQualidade = _tipoBloqueioRoboService.ListarTodosPorCodigoFuncaoBloqueio();
                    if (solicitacao.Fornecedor != null)
                        this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // DESBLOQUEIO DO FORNECEDOR
                case 120:
                    this.DesbloqueioFornecedor(ficha, solicitacao);
                    if (solicitacao.Fornecedor != null)
                        this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // ATUALIZAÇÃO DE DOCUMENTOS
                case 130:
                    this.AtualizacaoDocumentos(ficha, solicitacao);
                    this.FornecedorRobo(ficha, solicitacao);
                    break;

                // MODIFICAÇÕES DE DADOS BANCÁRIOS
                case 140:
                    this.ModificacoesInformacoesComplementares(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                case 150:
                    this.ModificacaoDadosEnderecos(ficha, solicitacao);
                    this.FornecedorRobo(ficha, this.RetornaFornecedor(solicitacao));
                    break;

                // MODIFICACAO DE UNSPSC
                case 160:
                    this.AtualizacaoUnspsc(ficha, solicitacao);
                    this.FornecedorRobo(ficha, solicitacao);
                    break;
            }

            // Solicitação
            if (solicitacao.SolicitacaoCadastroFornecedor.Count > 0)
                ficha.CategoriaNome = solicitacao.SolicitacaoCadastroFornecedor.First().WFD_PJPF_CATEGORIA.DESCRICAO;
            else if (solicitacao.Fornecedor != null)
                ficha.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            else
                ficha.CategoriaNome = solicitacao.FORNECEDORBASE.WFD_PJPF_CATEGORIA.DESCRICAO;

            ficha.Aprovacao.ID = solicitacao.ID;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Aprovacao.NomeContratante = solicitacao.Contratante.RAZAO_SOCIAL;
            ficha.Aprovacao.Solicitacao_Dt_Cria = solicitacao.SOLICITACAO_DT_CRIA;
            ficha.Aprovacao.NomeSolicitacao = solicitacao.Fluxo.FLUXO_NM;
            ficha.Aprovacao.FluxoId = solicitacao.FLUXO_ID;
            ficha.Aprovacao.FluxoTPId = solicitacao.Fluxo.FLUXO_TP_ID;
            ficha.Aprovacao.Login = solicitacao.Usuario != null ? solicitacao.Usuario.NOME : null;
            ficha.Aprovacao.Solicitacao_Tramite = solicitacao.WFD_SOLICITACAO_TRAMITE.FirstOrDefault(x => x.PAPEL_ID == idPapel);

            ficha.Solicitacao.Tramite = new SolicitacaoTramiteVM { Papel = new PapelVM { ID = idPapel } };

            int tpFluxoId = solicitacao.Fluxo.FLUXO_TP_ID;
            ViewBag.Fluxo = tpFluxoId;

            this.PreencheStatusRobo(ficha, solicitacao, tpFluxoId);

            //Mapear UNSPSC
            ficha.FornecedoresUnspsc =
                Mapper.Map<List<SOLICITACAO_UNSPSC>, List<FornecedorUnspscVM>>(solicitacao.WFD_SOL_UNSPSC.ToList());

            ficha.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                    Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                    _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                    {
                        //PapelId = papelAtual,
                        //UF = "RJ",
                        ContratanteId = solicitacao.CONTRATANTE_ID,
                        PapelId = idPapel,
                        CategoriaId = ficha.CategoriaId,
                        Alteracao = true,
                        SolicitacaoId = solicitacao.ID
                    })
                    )
            };

            ViewBag.Bancos = _bancoService.ListarTodosPorNome();
            ViewBag.TipoEndereco = new SelectList(_enderecoService.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");
            ViewBag.UF = new SelectList(_enderecoService.ListarTodosPorNome(), "UF_SGL", "UF_NM");

            return View(ficha);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AprovacaoFrm(FichaCadastralWebForLinkVM model, int ContratanteID, int SolicitacaoID, int FluxoID, int PapelID, string btnAcao, int[] DocumentoID, string[] Documento, HttpPostedFileBase[] file, string[] DataValidade, bool[] ExigeValidade, int[] Banco, string[] Agencia, string[] Digito, string[] ContaCorrente, string[] ContaCorrenteDigito, int[] ContatoID, string[] NomeContato, string[] Email, string[] Telefone, string[] Celular, string NomeEmpresa, string Estilo, bool? SolicitaFichaCadastral, bool? SolicitaDocumentos, string motivoReprovao, string ccCliente, string ccGrupoEmpresa)
        {
            try
            {
                SOLICITACAO solicitacao = _solicitacaoService.BuscarPorIdIncluindoFluxo(SolicitacaoID);

                int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                ViewBag.Bancos = _bancoService.ListarTodosPorNome();
                ViewBag.BloqueioMotivoQualidade = _tipoBloqueioRoboService.ListarTodosPorCodigoFuncaoBloqueio();
                ViewBag.TipoEndereco = new SelectList(_enderecoService.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");
                ViewBag.UF = new SelectList(_enderecoService.ListarTodosPorNome(), "UF_SGL", "UF_NM");

                switch (btnAcao)
                {
                    case "modificar":
                        var motivoReprovacao = string.IsNullOrEmpty(motivoReprovao) ? string.Empty : motivoReprovao;
                        _solicitacaoService.AlterarAprovacao(SolicitacaoID, ContratanteID, FluxoID, motivoReprovacao, usuarioId);
                        return RedirectToAction("AprovacaoLst", "Aprovacao", new
                        {
                            MensagemSucesso = string.Format("Modificação da solicitação Nº {0} realizada com sucesso!", SolicitacaoID)
                        });

                    case "aprovar":
                        _tramite.AtualizarTramite(ContratanteID, SolicitacaoID, FluxoID, PapelID, 2, usuarioId);
                        return RedirectToAction("AprovacaoLst", "Aprovacao", new
                        {
                            MensagemSucesso = string.Format("Aprovação da solicitação Nº {0} realizada com sucesso!", SolicitacaoID)
                        });

                    case "executado":
                        int? grupoId = (int?)Geral.PegaAuthTicket("Grupo");
                        _aprovacaoService.FinalizarSolicitacao(grupoId, solicitacao.Fluxo.FLUXO_TP_ID, SolicitacaoID);
                        _tramite.AtualizarTramite(ContratanteID, SolicitacaoID, FluxoID, PapelID, 2, usuarioId);
                        return RedirectToAction("AprovacaoLst", "Aprovacao", new
                        {
                            MensagemSucesso = string.Format("Aprovação da solicitação {0} realizada com sucesso!", SolicitacaoID)
                        });

                    case "reprovar":
                        _tramite.AtualizarTramite(ContratanteID, SolicitacaoID, FluxoID, PapelID, 3, usuarioId);
                        return RedirectToAction("AprovacaoLst", "Aprovacao", new
                        {
                            MensagemSucesso = string.Format("Reprovação da solicitação Nº {0} realizada com sucesso!", SolicitacaoID)
                        });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

    }
}