using AutoMapper;
using System;
using System.Collections.Generic;
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
    public class ControleSolicitacaoController : ControllerPadrao
    {
        public readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly ITipoBloqueioRoboWebForLinkAppService _funcaoBloqueioService;
        private readonly ICadastroUnicoWebForLinkAppService _cadastroUnicoService;
        private readonly IConfiguracaoEmailContratanteWebForLinkAppService _contratanteConfiguracaoEmailService;
        public readonly IFornecedorWebForLinkAppService _fornecedorService;
        public readonly IVisaoWebForLinkAppService _visaoService;
        public readonly ITipoGrupoWebForLinkAppService _tipoGrupoService;
        public readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoService;
        public readonly IFluxoWebForLinkAppService _fluxoService;
        public readonly IGrupoWebForLinkAppService _grupoService;
        public readonly IDescricaoWebForLinkAppService _descricaoService;
        public readonly IPapelWebForLinkAppService _papelService;
        public readonly IBancoWebForLinkAppService _BancoService;

        public ControleSolicitacaoController(
            ISolicitacaoWebForLinkAppService solicitacao,
            ITipoBloqueioRoboWebForLinkAppService funcaoBloqueio,
            ICadastroUnicoWebForLinkAppService cadastroUnico,
            IConfiguracaoEmailContratanteWebForLinkAppService contratanteConfiguracaoEmail,
            IFornecedorWebForLinkAppService pJpF,
            IVisaoWebForLinkAppService visao,
            ITipoGrupoWebForLinkAppService tipoGrupo,
            IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracao,
            IFluxoWebForLinkAppService fluxo,
            IGrupoWebForLinkAppService grupo,
            IDescricaoWebForLinkAppService descricao,
            IPapelWebForLinkAppService papel,
            IBancoWebForLinkAppService banco)
        {
            try
            {
                _solicitacaoService = solicitacao;
                _funcaoBloqueioService = funcaoBloqueio;
                _cadastroUnicoService = cadastroUnico;
                _contratanteConfiguracaoEmailService = contratanteConfiguracaoEmail;
                _fornecedorService = pJpF;
                _visaoService = visao;
                _tipoGrupoService = tipoGrupo;
                _contratanteConfiguracaoService = contratanteConfiguracao;
                _fluxoService = fluxo;
                _grupoService = grupo;
                _descricaoService = descricao;
                _papelService = papel;
                _BancoService = banco;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        [Authorize]
        public ActionResult FornecedoresControleSolicitacoesLst(int? Pagina, string chkSolicitacao, string cnpj, string cpf, string razaoSocial, string codigoSolicitacao, string tipoSolicitacao, string MensagemSucesso, bool? pendentes)
        {
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            // Dropdown de Tipo de Solicitação
            ViewBag.TipoSolicitacao = new SelectList(_fluxoService.ListarPorContratanteId(contratanteId), "ID", "FLUXO_NM");


            int pagina = Pagina ?? 1;
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;

            List<AprovacaoVM> lstAprovacaoVM = new List<AprovacaoVM>();

            int tpSolicitacao;
            int codSolicitacao;
            int.TryParse(tipoSolicitacao, out tpSolicitacao);
            int.TryParse(codigoSolicitacao, out codSolicitacao);
            AcompanhamentoPesquisaVM modelo = new AcompanhamentoPesquisaVM()
            {
                GrupoId = grupoId,
                Pendentes = pendentes,
                TipoSolicitacao = tpSolicitacao,
                CodigoSolicitacao = codSolicitacao,
                Cnpj = Mascara.RemoverMascaraCpfCnpj(cnpj),
                Cpf = Mascara.RemoverMascaraCpfCnpj(cpf),
                RazaoSocial = razaoSocial

            };

            //BUSCA Solicitações E MONTA PAGINAÇÃO
            RetornoPesquisa<SOLICITACAO> listaPesquisa = _solicitacaoService.BuscarPesquisa(Predicativos.FiltrarAcompanhamentoGrid(modelo, contratanteId), TamanhoPagina, pagina, a => a.ID);
            ViewBag.TotalPaginas = listaPesquisa.TotalPaginas;
            ViewBag.TotalRegistros = listaPesquisa.TotalRegistros;

            foreach (SOLICITACAO item in listaPesquisa.RegistrosPagina)
            {
                ListarGridAcompanhamento(lstAprovacaoVM, item);
            }

            return View(lstAprovacaoVM);
        }

        [Authorize]
        public ActionResult FornecedoresControleSolicitacoesFrm(string chaveurl)
        {
            int idSolicitacao = 0;
            int idSolicitacaoTipo;
            int idPapel;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "idS").Value, out idSolicitacao);
                Int32.TryParse(param.First(p => p.Name == "idST").Value, out idSolicitacaoTipo);
                Int32.TryParse(param.First(p => p.Name == "idP").Value, out idPapel);
            }

            int tipoFluxoId = _solicitacaoService.BuscarTipoFluxoId(idSolicitacao);
            ViewBag.Fluxo = tipoFluxoId;

            SOLICITACAO solicitacao = _solicitacaoService.BuscarPorIdControleSolicitacoes(idSolicitacao);
            int tpFluxoId = solicitacao.Fluxo.FLUXO_TP_ID;

            ViewBag.QtdGrupoEmpresa = _grupoService.QuantidadeEmpresa(solicitacao.CONTRATANTE_ID);
            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM(
                idSolicitacao,
                new AprovacaoVM
                {
                    Solicitacao_Tramite = new SOLICITACAO_TRAMITE()
                });
            switch (tipoFluxoId)
            {
                case 10: // Cadastro Fornecedor Nacional
                case 20: // Cadastro Fornecedor Nacional Direto
                case 30: // Cadastro de Fonecedor Pessoa Física
                case 40: // Cadastro Fornecedor Pessoa Fisica Direto
                    this.PopularAcompanhamentoNovoFornecedor(ficha, solicitacao);
                    break;

                case 50: // Cadastro Fornecedor Estrangeiro
                    this.PopularAcompanhamentoNovoFornecedorEstrangeiro(ficha, solicitacao);
                    break;

                case 60: // Ampliação de Fornecedor
                    this.PopularAcompanhamentoAmpliacao(ficha, solicitacao);
                    break;

                case 70: // Modificações gerais
                    this.PopularAcompanhamentoModificacaoGerais(ficha, solicitacao);
                    ViewBag.OutrosDadosVisao = new SelectList(_visaoService.ListarTodos(), "ID", "VISAO_NM", ficha.OutrosDadosVisao);
                    ViewBag.OutrosDadosGrupo = new SelectList(_tipoGrupoService.ListarGruposPorVisao(ficha.OutrosDadosVisao), "ID", "GRUPO_NM", ficha.OutrosDadosGrupo);
                    ViewBag.OutrosDadosDescricao = new SelectList(_descricaoService.ListarPorGrupoId(ficha.OutrosDadosGrupo), "ID", "DESCRICAO_NM", ficha.OutrosDadosDescricao);
                    break;

                case 90: // Modificações de dados Bancários
                    this.PopularAcompanhamentoModificacaoDadosBancarios(ficha, solicitacao);
                    break;

                case 100: // Modificações de dados Contatos
                    this.PopularAcompanhamentoModificacaoDadosContatos(ficha, solicitacao);
                    break;

                case 110: // Bloqueio do Fornecedor
                    this.PopularAcompanhamentoBloqueio(ficha, solicitacao);
                    ViewBag.BloqueioMotivoQualidade = _funcaoBloqueioService.ListarTodosPorCodigoFuncaoBloqueio();
                    break;

                case 120: // Desbloqueio do Fornecedor
                    this.PopularAcompanhamentoDesbloqueio(ficha, solicitacao);
                    break;

                case 130: // Atualizar Documentos
                    this.PopularAcompanhamentoAtualizacaoDocumento(ficha, solicitacao);
                    break;

                case 140: //Questionário Dinâmico
                    this.PopularAcompanhamentoQuestionarioDinamico(ficha, solicitacao);
                    break;

                case 150: //Mudança de Endereco
                    this.PopularAcompanhamentoModificacaoEndereco(ficha, solicitacao);
                    break;

                case 160: //Unspsc
                    this.PopularAcompanhamentoModificacaoUnspsc(ficha, solicitacao);
                    break;
            }

            //Mapear UNSPSC
            ficha.FornecedoresUnspsc =
                Mapper.Map<List<SOLICITACAO_UNSPSC>, List<FornecedorUnspscVM>>(solicitacao.WFD_SOL_UNSPSC.ToList());

            // Solicitação
            Mapper.Map(solicitacao, ficha.Aprovacao);

            this.PopularAcompanhamentoPreencheStatusRobo(ficha, solicitacao, tpFluxoId);

            int papelSolicitante = _papelService.BuscarPorContratanteETipoPapel(solicitacao.Contratante.ID, (int)EnumTiposPapel.Solicitante).ID;

            ficha.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                    Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                         _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                         {
                             ContratanteId = solicitacao.Contratante.ID,
                             PapelId = papelSolicitante,
                             CategoriaId = ficha.CategoriaId,
                             Alteracao = false,
                             SolicitacaoId = solicitacao.ID
                         })
                    )
            };

            var prorrogacao = solicitacao.WFD_SOLICITACAO_PRORROGACAO.Where(o => o.APROVADO == null).LastOrDefault();
            if (prorrogacao != null)
            {
                //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                ficha.ProrrogacaoPrazo =
                    Mapper.Map<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>(prorrogacao);
            }
            ficha.ProrrogacaoPrazo.PrazoPreenchimento = _contratanteConfiguracaoService.BuscarPrazo(solicitacao);

            PreparaModal(ficha);

            return View(ficha);
        }
        [Authorize]
        [ValidateInput(false)]
        //public void PreparaModal(ref FichaCadastralWebForLinkVM model)
        public void PreparaModal(FichaCadastralWebForLinkVM model)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            model.TipoFuncionalidade = EnumTiposFuncionalidade.ReenviarFicha;

            var configEmail = _contratanteConfiguracaoEmailService.BuscarPorContratanteETipo(contratanteId, 1);

            Preenchermodelo(contratanteId, model, configEmail, DateTime.Today.ToShortDateString());

            model.TipoFuncionalidade = EnumTiposFuncionalidade.ReenviarFicha;
            model.NomeFuncionalidade = "REENVIAR FICHA CADASTRAL";

            model.Mensagem = model.MensagemImportacao.Mensagem;
            model.Assunto = model.MensagemImportacao.Assunto;
        }

        public void Preenchermodelo(int contratanteId, FichaCadastralWebForLinkVM model, CONTRATANTE_CONFIGURACAO_EMAIL configEmail, string stData)
        {
            model.MensagemImportacao = new MensagemImportacaoVM(configEmail.ASSUNTO, configEmail.CORPO);
        }

        public void ListarGridAcompanhamento(List<AprovacaoVM> lstAprovacaoVM, SOLICITACAO item)
        {
            var cnpjAprovacao = string.Empty;
            var etapaAprovacao = string.Empty;




            string etapa;

            SOLICITACAO_TRAMITE tramite = item.WFD_SOLICITACAO_TRAMITE
                .LastOrDefault(x => x.SOLICITACAO_ID == item.ID);
            if (tramite != null)
                etapa = item.SOLICITACAO_STATUS_ID != (int)EnumStatusTramite.Concluido
                    ? tramite.Papel.PAPEL_NM
                    : item.WFD_SOLICITACAO_STATUS.NOME;
            else
                etapa = item.WFD_SOLICITACAO_STATUS.NOME;

            var aprovacaoVM = new AprovacaoVM(item.ID, item.FLUXO_ID, item.Fluxo.FLUXO_NM);
            aprovacaoVM.Fornecedor =

                !item.SolicitacaoCadastroFornecedor.Any()
                ? null
                : item.SolicitacaoCadastroFornecedor.First();

            if (aprovacaoVM.Fornecedor != null)
                aprovacaoVM.GrupoContas = item.SolicitacaoCadastroFornecedor.FirstOrDefault().WFD_PJPF_CATEGORIA.DESCRICAO;
            else if (item.SolicitacaoCadastroFornecedor.FirstOrDefault() != null)
                aprovacaoVM.GrupoContas = item.Fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == item.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            else if (item.Fornecedor != null)
                aprovacaoVM.GrupoContas = item.Fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == item.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            else
                aprovacaoVM.GrupoContas = item.FORNECEDORBASE.WFD_PJPF_CATEGORIA.DESCRICAO;

            aprovacaoVM.Contratante = item.Contratante;
            aprovacaoVM.Solicitacao_Dt_Cria = item.SOLICITACAO_DT_CRIA;
            aprovacaoVM.Login = (item.Usuario != null) ? item.Usuario.NOME : null;
            aprovacaoVM.Etapa = etapa;

            if (item.Fornecedor != null)
                if (item.Fornecedor.TIPO_PJPF_ID == 1 || item.Fornecedor.TIPO_PJPF_ID == 2)
                    aprovacaoVM.NomeFornecedor = item.Fornecedor.RAZAO_SOCIAL;
                else
                    aprovacaoVM.NomeFornecedor = item.Fornecedor.NOME;
            else
                if (item.SolicitacaoCadastroFornecedor.FirstOrDefault() != null)
                if (item.SolicitacaoCadastroFornecedor.First().PJPF_TIPO != 3)
                    aprovacaoVM.NomeFornecedor = item.SolicitacaoCadastroFornecedor.FirstOrDefault().RAZAO_SOCIAL;
                else
                    aprovacaoVM.NomeFornecedor = item.SolicitacaoCadastroFornecedor.FirstOrDefault().NOME;
            else
                aprovacaoVM.NomeFornecedor = item.FORNECEDORBASE.RAZAO_SOCIAL != null
                    ? item.FORNECEDORBASE.RAZAO_SOCIAL
                    : item.FORNECEDORBASE.NOME;

            // 1  = Cadastro Fornecedor Nacional
            // 2  = Cadastro Fornecedor Estrangeiro
            // 10 = Cadastro de Fonecedor Pessoa Física 
            // 11 = Cadastro Fornecedor Nacional Direto
            // 12 = Cadastro Fornecedor Pessoa Fisica Direto

            var fluxos = new List<int> { 10, 20, 30, 40, 50 };

            if (fluxos.Contains(item.Fluxo.FLUXO_TP_ID))
            {
                if (item.SolicitacaoCadastroFornecedor.Any())
                {
                    string sCnpj = item.SolicitacaoCadastroFornecedor.First().CNPJ;
                    string sCpf = item.SolicitacaoCadastroFornecedor.First().CPF;
                    int tpForn = item.SolicitacaoCadastroFornecedor.First().PJPF_TIPO;
                    if (tpForn == 1 || tpForn == 2)
                        if (tpForn != 3)
                            aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCnpj).ToString(@"00\.000\.000\/0000\-00");
                        else
                            aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCpf).ToString(@"000\.000\.000\-00");
                    else
                        aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCpf).ToString(@"000\.000\.000\-00");
                }
                else
                {
                    if (item.Fornecedor != null)
                    {
                        string sCnpj = item.Fornecedor.CNPJ;
                        if (!string.IsNullOrEmpty(sCnpj))
                            aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCnpj).ToString(@"00\.000\.000\/0000\-00");
                    }
                }
            }
            else
            {

                string sCnpj = item.Fornecedor != null ? item.Fornecedor.CNPJ : item.FORNECEDORBASE.CNPJ;
                string sCpf = item.Fornecedor != null ? item.Fornecedor.CPF : item.FORNECEDORBASE.CPF;
                int? tpForn = item.Fornecedor != null ? item.Fornecedor.TIPO_PJPF_ID : item.FORNECEDORBASE.PJPF_TIPO;
                if (tpForn == 1 || tpForn == 2)
                    if (tpForn != 3)
                        aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCnpj).ToString(@"00\.000\.000\/0000\-00");
                    else
                        aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCpf).ToString(@"000\.000\.000\-00");
                else
                    aprovacaoVM.CNPJ_CPF = Convert.ToUInt64(sCpf).ToString(@"000\.000\.000\-00");
            }
            if (tramite != null)
                aprovacaoVM.UrlAprovacao = Url.Action("FornecedoresControleSolicitacoesFrm", "ControleSolicitacao",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("idS={0}&idST={1}&idP={2}",
                        aprovacaoVM.IdSolicitacao,
                        aprovacaoVM.FluxoId,
                        tramite.PAPEL_ID), Key)

                    }, Request.Url.Scheme);

            lstAprovacaoVM.Add(aprovacaoVM);
        }

    }
}