using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DadosBancariosController : ControllerPadrao, IModificacaoFichaCadastral
    {
        private readonly IBancoWebForLinkAppService _bancoService;
        private readonly IFornecedorBancoWebForLinkAppService _fornecedorBancoService;
        private readonly IFornecedorArquivoWebForLinkAppService _fornecedorArquivoService;
        private readonly IFluxoWebForLinkAppService _fluxoService;
        private readonly ISolicitacaoModificacaoBancoWebForLinkAppService _solicitacaoBancoService;
        private readonly ITramiteWebForLinkAppService iTramiteService;

        public DadosBancariosController(IBancoWebForLinkAppService banco, 
            IFornecedorBancoWebForLinkAppService pjPfBanco, 
            IFornecedorArquivoWebForLinkAppService fornecedorArquivo, 
            IFluxoWebForLinkAppService fluxo, 
            ISolicitacaoModificacaoBancoWebForLinkAppService solBanco, 
            ITramiteWebForLinkAppService tramite)
        {
            _bancoService = banco;
            _fornecedorBancoService = pjPfBanco;
            _fornecedorArquivoService = fornecedorArquivo;
            _fluxoService = fluxo;
            _solicitacaoBancoService = solBanco;
            iTramiteService = tramite;
        }

        [HttpPost]
        public ActionResult Incluir()
        {
            var dadosBancarios = new DadosBancariosVM();
            PersistirDadosBancoEmMemoria();
            return PartialView("~/Views/Shared/EditorTemplates/DadosBancariosVM.cshtml", dadosBancarios);
        }

        [HttpPost]
        public ActionResult Editar(int contratanteFornecedorID)
        {
            //var dadosBancarios = pjPfBancoBP.BuscarPorContratantePJPFId(contratanteFornecedorID);
            PersistirDadosBancoEmMemoria();
            return PartialView("~/Views/Fornecedores/_FichaCadastral_DadosBancario_Editavel", Mapper.Map<List<BancoDoFornecedor>, List<DadosBancariosVM>>(_fornecedorBancoService.BuscarPorContratantePJPFId(contratanteFornecedorID)));
        }

        [HttpPost]
        public ActionResult Salvar(FichaCadastralWebForLinkVM model)
        {
            try
            {
                SOLICITACAO solicitacao = new SOLICITACAO();
                solicitacao.FLUXO_ID = _fluxoService.BuscarPorTipoEContratante((int)EnumTiposFluxo.ModificacaoDadosBancarios, model.ContratanteID).ID;
                solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
                solicitacao.USUARIO_ID = (int)Geral.PegaAuthTicket("UsuarioId");
                solicitacao.PJPF_ID = model.ID;

                if (model.ContratanteID != 0)
                    solicitacao.CONTRATANTE_ID = model.ContratanteID;

                foreach (var item in model.DadosBancarios)
                {
                    item.SolicitacaoID = solicitacao.ID;
                    item.ContratanteID = solicitacao.CONTRATANTE_ID;
                    item.BancoPJPFID = item.BancoPJPFID;

                    if (!string.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        var arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(solicitacao.CONTRATANTE_ID, item.ArquivoSubido, item.TipoArquivoSubido);
                        item.ArquivoID = arquivoId;
                    }

                }

                var solicitacoesModBanco = Mapper.Map<List<DadosBancariosVM>, List<SolicitacaoModificacaoDadosBancario>>(model.DadosBancarios.ToList());
                    _solicitacaoBancoService.InserirSolicitacoes(solicitacoesModBanco, solicitacao);

                int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                iTramiteService.AtualizarTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, (int)EnumPapeisWorkflow.Solicitante, (int)EnumStatusTramite.Aprovado, usuarioId);

                var dadosBancariosFinalizados = _fornecedorBancoService.BuscarPorContratantePJPFId(model.ContratanteFornecedorID);

                PersistirDadosBancoEmMemoria();

                string chaveUrl = "";

                if (model.ControllerOrigem == "Documento")
                    chaveUrl = Cripto.Criptografar(String.Format("SolicitacaoID=0&FornecedorID={0}&ContratanteID={1}&RetSucessoBancos=1", model.PJPFID, model.ContratanteID), Key);
                else if (model.ControllerOrigem == "Fornecedores")
                    chaveUrl = Cripto.Criptografar(String.Format("FornecedorID={0}&ContratanteID={1}&RetSucessoBancos=1", model.PJPFID, model.ContratanteID), Key);

                return RedirectToAction(model.ActionOrigem, model.ControllerOrigem, new { chaveurl = chaveUrl });

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                string chaveUrl = "";

                if (model.ControllerOrigem == "Documento")
                    chaveUrl = Cripto.Criptografar(String.Format("SolicitacaoID=0&FornecedorID={0}&ContratanteID={1}&RetSucessoBancos=-1", model.PJPFID, model.ContratanteID), Key);
                else if (model.ControllerOrigem == "Fornecedores")
                    chaveUrl = Cripto.Criptografar(String.Format("FornecedorID={0}&ContratanteID={1}&RetSucessoBancos=-1", model.PJPFID, model.ContratanteID), Key);

                return RedirectToAction(model.ActionOrigem, model.ControllerOrigem, new { chaveurl = chaveUrl });
            }

        }

        [HttpPost]
        public ActionResult Cancelar(int contratanteFornecedorID)
        {
            return PartialView("_FichaCadastral_DadosBancario", Mapper.Map<List<BancoDoFornecedor>, List<DadosBancariosVM>>(_fornecedorBancoService.BuscarPorContratantePJPFId(contratanteFornecedorID)));
        }
        
        private void PersistirDadosBancoEmMemoria()
        {
            //ViewBag.Bancos
            if (TempData["Bancos"] == null)
                TempData["Bancos"] = new SelectList(_bancoService.ListarTodosPorNome(), "ID", "BANCO_NM");

            ViewBag.Bancos = TempData["Bancos"] as SelectList;
            TempData.Keep("Bancos");
        }
    }
}