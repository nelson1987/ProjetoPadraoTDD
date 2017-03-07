using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class DadosGeraisController : ControllerPadrao
    {
        private readonly IVisaoWebForLinkAppService _visaoService;
        private readonly IFornecedorWebForLinkAppService _fornecedorService;
        private readonly ITramiteWebForLinkAppService _tramiteService;

        public DadosGeraisController(IVisaoWebForLinkAppService visao, IFornecedorWebForLinkAppService fornecedor, ITramiteWebForLinkAppService tramite)
        {
            _visaoService = visao;
            _fornecedorService = fornecedor;
            _tramiteService = tramite;
        }

        [Authorize]
        public ActionResult FornecedoresModificacaoDadosGeraisFrm(string chaveurl)
        {
            int fornecedorId = 0;
            int fornecedorContratanteID = 0;
            ViewBag.ChaveUrl = chaveurl;

            try
            {
                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "id").Value, out fornecedorId);
                    Int32.TryParse(param.First(p => p.Name == "contratanteid").Value, out fornecedorContratanteID);
                }

                if (fornecedorId != 0)
                {
                    Fornecedor fornecedor = _fornecedorService.BuscarPorIdModificacaoFornecedor(fornecedorId);
                    Contratante contratante = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault(c => c.CONTRATANTE_ID == fornecedorContratanteID).WFD_CONTRATANTE;

                    FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
                    if (fornecedor != null)
                    {
                        CriarEntidadePartialDadosCadastro(fornecedorId, fornecedor, contratante, ficha);
                    }

                    ViewBag.OutrosDadosVisao = new SelectList(_visaoService.ListarTodos(), "ID", "VISAO_NM");
                    ViewBag.OutrosDadosGrupo = new SelectList(new List<TIPO_GRUPO>(), "ID", "GRUPO_NM");
                    ViewBag.OutrosDadosDescricao = new SelectList(new List<TIPO_DESCRICAO>(), "ID", "DESCRICAO_NM");

                    return View(ficha);
                }
                else
                {
                    return RedirectToAction("Alerta", "Home");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FornecedoresModificacaoDadosGeraisFrm(FichaCadastralWebForLinkVM model)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            ViewBag.OutrosDadosVisao = new SelectList(Db.WFL_TP_VISAO.ToList(), "ID", "VISAO_NM", model.OutrosDadosVisao);
            ViewBag.OutrosDadosGrupo = new SelectList(Db.WFD_T_GRUPO.Where(g => g.VISAO_ID == model.OutrosDadosVisao).ToList(), "ID", "GRUPO_NM", model.OutrosDadosGrupo);
            ViewBag.OutrosDadosDescricao = new SelectList(Db.WFD_T_DESCRICAO.Where(d => d.GRUPO_ID == model.OutrosDadosGrupo).ToList(), "ID", "DESCRICAO_NM", model.OutrosDadosDescricao);

            // VERIFICA DADOS

            if (model.OutrosDadosVisao == 0)
            {
                ModelState.AddModelError("OutrosDadosVisaoValidation", "Selecione a Visão!");
            }
            if (model.OutrosDadosGrupo == 0)
            {
                ModelState.AddModelError("OutrosDadosGrupoValidation", "Selecione o Grupo!");
            }
            if (model.OutrosDadosDescricao == 0)
            {
                ModelState.AddModelError("OutrosDadosDescricaoValidation", "Selecione a Descrição!");
            }
            if (string.IsNullOrEmpty(model.OutrosDadosDescricaoMudança) || String.IsNullOrWhiteSpace(model.OutrosDadosDescricaoMudança))
            {
                ModelState.AddModelError("OutrosDadosDescricaoMudancaValidation", "Informe a Descrição da Alteração!");
            }

            if (ModelState.IsValid)
            {
                SOLICITACAO_MODIFICACAO_DADOSGERAIS solgerais = new SOLICITACAO_MODIFICACAO_DADOSGERAIS();

                SOLICITACAO solicitacao = new SOLICITACAO();
                try
                {
                    int FluxoId = 4;

                    PopularSolicitacaoEmAprovacao(model.ContratanteID, model.ID,
                        usuarioId, FluxoId, solicitacao);

                    Db.Entry(solicitacao).State = EntityState.Added;
                    Db.SaveChanges();

                    Db.WFD_SOL_MOD_DGERAIS_SEQ.Add(new SOLICITACAO_MODIFICACAO_DADOSGERAIS
                    {
                        SOLICITACAO_ID = solicitacao.ID,
                        VISAO_ID = model.OutrosDadosVisao,
                        GRUPO_ID = model.OutrosDadosGrupo,
                        DESCRICAO_ID = model.OutrosDadosDescricao,
                        DESCRICAOALTERACAO = model.OutrosDadosDescricaoMudança
                    });

                    Db.SaveChanges();

                    _tramiteService.AtualizarTramite(model.ContratanteID, solicitacao.ID, FluxoId, 1, 2, usuarioId);
                }
                catch
                {
                    if (solicitacao.ID != 0)
                    {
                        if (solgerais.ID != 0)
                            Db.WFD_SOL_MOD_DGERAIS_SEQ.Remove(solgerais);
                        Db.WFD_SOLICITACAO.Remove(solicitacao);
                        Db.SaveChanges();
                    }
                }

                return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação {0} de Alteração de Dados Gerais realizado com Sucesso!", solicitacao.ID) });
            }

            return View(model);
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