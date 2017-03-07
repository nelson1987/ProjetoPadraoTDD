using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Exceptions;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DesbloqueioController : ControllerPadrao
    {
        private readonly ITramiteWebForLinkAppService _tramite;
        private readonly IFluxoWebForLinkAppService _fluxoBP;
        private readonly ITipoBloqueioRoboWebForLinkAppService _funcaoBloqueioBP;
        private readonly SolicitacaoDesbloqueioWebForLinkAppService _solicitacaoDesbloqueio;
        private readonly IPapelWebForLinkAppService _papelBP;
        public DesbloqueioController(ITramiteWebForLinkAppService tramite, 
            IFluxoWebForLinkAppService fluxoBP, 
            ITipoBloqueioRoboWebForLinkAppService funcaoBloqueioBP,
            SolicitacaoDesbloqueioWebForLinkAppService solicitacaoDesbloqueio,
            IPapelWebForLinkAppService papelBP)
            : base()
        {
            _tramite = tramite;
            _fluxoBP = fluxoBP;
            _funcaoBloqueioBP = funcaoBloqueioBP;
            _solicitacaoDesbloqueio = solicitacaoDesbloqueio;
            _papelBP = papelBP;
        }

        [Authorize]
        public ActionResult FornecedoresDesBloqueioFrm(string chaveurl)
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
                        .FirstOrDefault(c => c.PJPF_ID == fornecedorID &&
                            c.CONTRATANTE_ID == contratanteID);

                    if (contratantePjpf.WFD_PJPF != null)
                    {
                        CriarEntidadePartialDadosCadastro(fornecedorID, contratantePjpf.WFD_PJPF, contratantePjpf.WFD_CONTRATANTE, ficha);
                        ficha.ContratanteFornecedorID = contratantePjpf.ID;
                        ficha.TipoFornecedor = (int)contratantePjpf.WFD_PJPF.TIPO_PJPF_ID;

                        var fluxoId = _fluxoBP.BuscarPorTipoEContratante((int)EnumTiposFluxo.DesbloqueioFornecedor, contratanteID).ID;
                        ficha.Solicitacao.Fluxo.ID = fluxoId;
                    }

                    var bloq = Db.WFD_SOL_BLOQ.Include("TipoDeFuncaoDuranteBloqueio").FirstOrDefault(x => x.SOLICITACAO_ID == contratantePjpf.PJPF_STATUS_ID_SOL);

                    ViewBag.BloqueioMotivoQualidade = _funcaoBloqueioBP.ListarTodosPorCodigoFuncaoBloqueio();
                    ViewBag.BloqueioEscolhidoId = bloq.BLQ_QUALIDADE_FUNCAO_BQL_ID;
                    ViewBag.BloqueioEscolhido = bloq.TipoDeFuncaoDuranteBloqueio.FUNCAO_BLOQ_DSC;
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
        public ActionResult FornecedoresDesBloqueioFrm(FichaCadastralWebForLinkVM model, string rdLancamento, string rdCompras, string txtAreaMotivoDesbloqueio, int ContratanteID, int ContratanteFornecedorID, int? ID, int? bloqueioMotivoQualidade)
        {
            if (rdLancamento == null)
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Informe ao menos um desbloqueio de lançamento!");

            if (ContratanteID == 0)
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Ocorreu um error ao tentar salvar o desbloqueio. Por favor, tente mais tarde.");

            if (ContratanteFornecedorID == 0)
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Ocorreu um error ao tentar salvar o desbloqueio. Por favor, tente mais tarde.");

            if (ID == 0 || ID == null)
                ModelState.AddModelError("FornecedoresBloqueioValidation", "Ocorreu um error ao tentar salvar o desbloqueio. Por favor, tente mais tarde.");

            if (ModelState.IsValid)
            {
                int UsuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                var solicitacaoId = 0;

                try
                {
                    SOLICITACAO_DESBLOQUEIO desbloqueio = _solicitacaoDesbloqueio.criarSolicitacaoDesbloqueio(ContratanteID, UsuarioId, model.ID, rdLancamento, rdCompras, bloqueioMotivoQualidade, txtAreaMotivoDesbloqueio);
                    solicitacaoId = desbloqueio.WFD_SOLICITACAO.ID;
                    var papelAtual = _papelBP.BuscarPorContratanteETipoPapel(ContratanteID, (int)EnumTiposPapel.Solicitante).ID;
                    _tramite.AtualizarTramite(model.ContratanteID, solicitacaoId, desbloqueio.WFD_SOLICITACAO.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aprovado, UsuarioId);
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Log.Error(
                            string.Format(
                                "A entidade do Tipo \"{0}\" no estado \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Log.Error(string.Format("- Propriedade : \"{0}\", Erro: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    ViewBag.MensagemErro = "Erro ao tentar incluir um novo Papel!";
                    throw new WebForLinkException("Erro ao tentar incluir um novo Papel!");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                    return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = "Ocorreu um error para realizar o desbloqueio solicitado. Por favor, tente mais tarde." });
                }

                return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação {0} de Desbloqueio realizado com Sucesso!", solicitacaoId) });
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
    }
}