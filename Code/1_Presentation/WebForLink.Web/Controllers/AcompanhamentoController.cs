using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    [WebForLinkFilter]
    public class AcompanhamentoController : ControllerPadrao, IAcompanhamentoController
    {
        public int? PaginaAtual { get; private set; }
        protected internal IConfiguracaoEmailContratanteWebForLinkAppService _contratanteConfiguracaoEmailBP;
        protected internal IUsuarioWebForLinkAppService _usuarioBP;
        protected internal IPapelWebForLinkAppService _papelService;
        protected internal ISolicitacaoTramiteWebForLinkAppService _solicitacaoTramite;
        protected internal ISolicitacaoWebForLinkAppService _solicitacaoService;
        protected internal ISolicitacaoProrrogacaoPrazoWebForLinkAppService _solicitacaoProrrogacaoPrazo;
        protected internal ITramiteWebForLinkAppService _tramite;
        protected internal IFornecedorWebForLinkAppService _fornecedorService;

        public AcompanhamentoController(IConfiguracaoEmailContratanteWebForLinkAppService configuracaoEmail,
            IUsuarioWebForLinkAppService usuario,
            ISolicitacaoWebForLinkAppService solicitacao,
            ISolicitacaoTramiteWebForLinkAppService solicitacaoTramite,
            IPapelWebForLinkAppService papel,
            ISolicitacaoProrrogacaoPrazoWebForLinkAppService prorrogacaoPrazo, 
            ITramiteWebForLinkAppService tramite, 
            int? paginaAtual,
            IFornecedorWebForLinkAppService fornecedor)
        {
            _contratanteConfiguracaoEmailBP = configuracaoEmail;
            _usuarioBP = usuario;
            _solicitacaoService = solicitacao;
            _papelService = papel;
            _solicitacaoTramite = solicitacaoTramite;
            _solicitacaoProrrogacaoPrazo = prorrogacaoPrazo;
            _tramite = tramite;
            PaginaAtual = paginaAtual;
            _fornecedorService = fornecedor;
        }
        
        [Authorize]
        [ValidateInput(false)]
        public void PreparaModal(FichaCadastralWebForLinkVM model)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            model.TipoFuncionalidade = EnumTiposFuncionalidade.ReenviarFicha;

            var configEmail = _contratanteConfiguracaoEmailBP.BuscarPorContratanteETipo(contratanteId, 1);

            Preenchermodelo(contratanteId, model, configEmail, DateTime.Today.ToShortDateString());

            model.TipoFuncionalidade = EnumTiposFuncionalidade.ReenviarFicha;
            model.NomeFuncionalidade = "REENVIAR FICHA CADASTRAL";

            model.Mensagem = model.MensagemImportacao.Mensagem;
            model.Assunto = model.MensagemImportacao.Assunto;
        }

        [Authorize]
        [ValidateInput(false)]
        public ActionResult ReenviarFicha(string TipoFuncionalidade, int idSolicitacao, string CNPJ, string EmailContato, FichaCadastralWebForLinkVM ficha)
        {
            try
            {
                CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

                int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

                SOLICITACAO solicitacao = _solicitacaoService.BuscarPorIdControleSolicitacoes(idSolicitacao);
                ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
                ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;


                var papelFornecedor = _papelService.BuscarPorContratanteETipoPapel(ficha.ContratanteID, (int)EnumTiposPapel.Fornecedor).ID;
                var tramites = _solicitacaoTramite.BuscarTramitePorSolicitacaoIdPapelId(idSolicitacao, papelFornecedor);

                if (tramites == null)
                {
                    var papelSolicitante = _papelService.BuscarPorContratanteETipoPapel(ficha.ContratanteID, (int)EnumTiposPapel.Solicitante).ID;
                    _tramite.AtualizarTramite(ficha.ContratanteID, idSolicitacao, ficha.Solicitacao.Fluxo.ID, papelSolicitante, (int)EnumStatusTramite.Aprovado, usuarioId);
                }

                //se não for primeiro acesso enviar para tela de acesso
                string url = Url.Action("Index", "Home", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}&Login={1}&TravaLogin=1", idSolicitacao, CNPJ), Key)
                }, Request.Url.Scheme);

                //se for primeiro acesso enviar para criação de usuário
                #region BuscarPorEmail
                //validar se o e-mail já existe na tabela de Usuarios
                if (!_usuarioBP.ValidarPorCnpj(CNPJ))
                    url = Url.Action("CadastrarUsuarioFornecedor", "Home", new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("Login={0}&SolicitacaoID={1}&Email={2}",
                        CNPJ,
                        idSolicitacao,
                        EmailContato), Key)
                    }, Request.Url.Scheme);

                #endregion

                string mensagemLink = string.Concat(ficha.Mensagem, "<p><a href='", url, "'>Link</a>:", url, "</p>");
                bool emailEnviadoComSucesso = _metodosGerais.EnviarEmail(EmailContato, ficha.Assunto, mensagemLink);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return RedirectToAction("FornecedoresControleSolicitacoesLst", "ControleSolicitacao");
        }

        [HttpGet]
        [Authorize]
        public JsonResult AprovarProrrogacao(int idProrrogacao)
        {
            try
            {
                int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                _solicitacaoProrrogacaoPrazo.AprovarSolicitacaoProrrogacao(idProrrogacao, usuarioId);
                return Json(new { erro = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return Json(new { erro = 1 }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize]
        public JsonResult ReprovarProrrogacao(int idProrrogacao, string motivo)
        {
            try
            {
                int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                _solicitacaoProrrogacaoPrazo.ReprovarSolicitacaoProrrogacao(idProrrogacao, motivo, usuarioId);
                return Json(new { erro = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return Json(new { erro = 1 }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _FichaCadastral_DadosBancario(List<DadosBancariosVM> modelo)
        {
            return PartialView("../Acompanhamento/Partials/_FichaCadastral_DadosBancarios", modelo);
        }

        #region Compartilhados
        public void ListarGridAcompanhamento(List<AprovacaoVM> lstAprovacaoVM, SOLICITACAO item)
        {
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

            aprovacaoVM.Fornecedor = !item.SolicitacaoCadastroFornecedor.Any()
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
                aprovacaoVM.UrlAprovacao = Url.Action("FornecedoresControleSolicitacoesFrm", "Acompanhamento",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("idS={0}&idST={1}&idP={2}",
                        aprovacaoVM.IdSolicitacao,
                        aprovacaoVM.FluxoId,
                        tramite.PAPEL_ID), Key)

                    }, Request.Url.Scheme);

            lstAprovacaoVM.Add(aprovacaoVM);
        }

        public void ManipularFiltroEspecifico(EnumTiposFuncionalidade funcionalidade, ImportacaoFornecedoresFiltrosDTO filtro)
        {
            switch (funcionalidade)
            {
                case EnumTiposFuncionalidade.ValidarEmOrgaosPublicos:
                    if (!filtro.Validados.HasValue) filtro.Validados = false; break;
                case EnumTiposFuncionalidade.Categorizar:
                    if (!filtro.Categorizados.HasValue) filtro.Categorizados = false; break;
                case EnumTiposFuncionalidade.Convidar:
                    if (!filtro.Convidados.HasValue) filtro.Convidados = false; break;
                case EnumTiposFuncionalidade.ProrrogarPrazo:
                    if (!filtro.Prorrogados.HasValue) filtro.Prorrogados = false; break;
                case EnumTiposFuncionalidade.GerarCarga:
                    if (!filtro.Gerados.HasValue) filtro.Gerados = false; break;
                case EnumTiposFuncionalidade.CompletarDados:
                    if (!filtro.Completos.HasValue) filtro.Completos = false; break;
                case EnumTiposFuncionalidade.AprovarPrazo:
                    if (!filtro.Aprovados.HasValue) filtro.Aprovados = 1; break;
                case EnumTiposFuncionalidade.Bloquear:
                    if (!filtro.Bloqueados.HasValue) filtro.Bloqueados = 0; break;
                case EnumTiposFuncionalidade.ReenviarFicha:
                    if (!filtro.Reenviados.HasValue) filtro.Reenviados = false; break;
            }
        }

        public void Preenchermodelo(int contratanteId, FichaCadastralWebForLinkVM model, CONTRATANTE_CONFIGURACAO_EMAIL configEmail, string stData)
        {
            model.MensagemImportacao = new MensagemImportacaoVM(configEmail.ASSUNTO, configEmail.CORPO);
        }
        #endregion
    }
}