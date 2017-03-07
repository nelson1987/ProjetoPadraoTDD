using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class BloqueioController : ControllerPadrao
    {
        private readonly ITramiteWebForLinkAppService _tramite;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoBP;
        private readonly IFluxoWebForLinkAppService _fluxoService;
        private readonly ITipoBloqueioRoboWebForLinkAppService _bloqueioService;

        public BloqueioController(ITramiteWebForLinkAppService tramite, 
            IFluxoWebForLinkAppService fluxoService,
            ISolicitacaoWebForLinkAppService solicitacaoBP, 
            ITipoBloqueioRoboWebForLinkAppService bloqueioService)
            : base()
        {
            _tramite = tramite;
            _fluxoService = fluxoService;
            this._solicitacaoBP = solicitacaoBP;
            _bloqueioService = bloqueioService;
        }

        [Authorize]
        public ActionResult FornecedoresBloqueioFrm(string chaveurl)
        {
            int fornecedorID = 0;
            int contratanteID = 0;
            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
            try
            {
                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "FornecedorID").Value, out fornecedorID);
                    Int32.TryParse(param.First(p => p.Name == "ContratanteID").Value, out contratanteID);
                }

                if (fornecedorID != 0)
                {
                    var contratantePjpf = Db.WFD_CONTRATANTE_PJPF
                        .Include("Contratante")
                        .Include("Fornecedor")
                        .Where(c => c.PJPF_ID == fornecedorID).ToList();

                    var contratante = contratantePjpf.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteID).WFD_CONTRATANTE;
                    var fornecedor = contratantePjpf.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteID).WFD_PJPF;

                    ficha.DadosContatos = new List<DadosContatoVM>();
                    ficha.Solicitacao = new SolicitacaoVM
                    {
                        Fluxo = new FluxoVM()
                    };

                    if (fornecedor != null)
                    {
                        CriarEntidadePartialDadosCadastro(fornecedorID, fornecedor, contratante, ficha);
                        ficha.ContratanteFornecedorID = contratantePjpf.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteID).ID;
                        ficha.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;

                        var fluxoId = _fluxoService.BuscarPorTipoEContratante((int)EnumTiposFluxo.BloqueioFornecedor, contratanteID).ID;
                        ficha.Solicitacao.Fluxo.ID = fluxoId;
                    }

                    ViewBag.BloqueioMotivoQualidade = _bloqueioService.ListarTodosPorCodigoFuncaoBloqueio();
                    ViewBag.QtdGrupoEmpresa = (int)Db.WFD_GRUPO.Count(x => x.WFD_CONTRATANTE.Any(y => y.ID == contratanteID));
                    ViewBag.ChaveUrl = chaveurl;
                    return View(ficha);
                }
                return RedirectToAction("Alerta", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return View(ficha);
        }

        [Authorize]
        [HttpPost]
        public ActionResult FornecedoresBloqueioFrm(FichaCadastralWebForLinkVM model, string rdLancamento, string rdCompras, string txtAreaMotivoBloqueio, int ContratanteID, int ContratanteFornecedorID, int? ID, int? bloqueioMotivoQualidade)
        {
            if (rdLancamento == null)
            {
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Informe ao menos um bloqueio de lançamento!");
            }

            if (ContratanteID == 0)
            {
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Ocorreu um error ao tentar salvar o bloqueio. Por favor, tente mais tarde.");
            }

            if (ContratanteFornecedorID == 0)
            {
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Ocorreu um error ao tentar salvar o bloqueio. Por favor, tente mais tarde.");
            }

            if (ID == 0 || ID == null)
            {
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Ocorreu um error ao tentar salvar o bloqueio. Por favor, tente mais tarde.");
            }

            if (ModelState.IsValid)
            {
                int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
                int UsuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                SOLICITACAO solicitacao = new SOLICITACAO();

                try
                {
                    int FluxoId = 8;

                    if (contratanteId != 0)
                        solicitacao.CONTRATANTE_ID = contratanteId;
                    solicitacao.USUARIO_ID = UsuarioId;
                    solicitacao.PJPF_ID = model.ID;

                    SOLICITACAO_BLOQUEIO bloqueio = new SOLICITACAO_BLOQUEIO
                    {
                        SOLICITACAO_ID = solicitacao.ID,
                        BLQ_LANCAMENTO_TODAS_EMP = rdLancamento == "1",
                        BLQ_LANCAMENTO_EMP = rdLancamento == "2",
                        BLQ_COMPRAS_TODAS_ORG_COMPRAS = !string.IsNullOrEmpty(rdCompras),
                        BLQ_QUALIDADE_FUNCAO_BQL_ID = bloqueioMotivoQualidade,
                        BLQ_MOTIVO_DSC = txtAreaMotivoBloqueio,
                    };
                    _solicitacaoBP.CriarSolicitacaoBloqueio(solicitacao, bloqueio);
                    _tramite.AtualizarTramite(model.ContratanteID, solicitacao.ID, FluxoId, 1, 2, UsuarioId);
                }
                catch (Exception ex)
                {
                    if (solicitacao.ID != 0)
                    {
                        Db.WFD_SOLICITACAO.Remove(solicitacao);
                        Db.SaveChanges();
                        Log.Error(ex);
                        return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = "Ocorreu um error para realizar o bloqueio solicitado. Por favor, tente mais tarde." });
                    }
                }
                return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação {0} de Bloqueio realizado com Sucesso!", solicitacao.ID) });
            }
            return View();
        }

        private static void CriarEntidadePartialDadosCadastro(int fornecedorID, Fornecedor fornecedor, Contratante contratante, FichaCadastralWebForLinkVM ficha)
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
        private void PopularSolicitacaoEmAprovacao(int contratanteId, int fornecedorId, int? usuarioId, int fluxoId, SOLICITACAO solicitacao)
        {
            if (contratanteId != 0)
                solicitacao.CONTRATANTE_ID = contratanteId;

            solicitacao.FLUXO_ID = fluxoId; // Bloqueio
            solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
            solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
            solicitacao.USUARIO_ID = usuarioId;
            solicitacao.PJPF_ID = fornecedorId;
        }

    }
}