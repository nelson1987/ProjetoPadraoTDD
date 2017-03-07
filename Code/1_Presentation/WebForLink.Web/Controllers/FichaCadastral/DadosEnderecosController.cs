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
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DadosEnderecosController : ControllerPadrao
    {
        private readonly IEnderecoWebForLinkAppService _enderecoService;
        private readonly IContratanteFornecedorWebForLinkAppService _contratantePjPfService;
        private readonly ISolicitacaoModificacaoEnderecoWebForLinkAppService _solicitacaoModificacaoEnderecoService;
        private readonly IFluxoWebForLinkAppService _fluxoService;
        private readonly IPapelWebForLinkAppService _papelService;
        private readonly ITramiteWebForLinkAppService _tramiteService;

        public DadosEnderecosController(
            IEnderecoWebForLinkAppService endereco,
            IContratanteFornecedorWebForLinkAppService contratantePjPf,
            ISolicitacaoModificacaoEnderecoWebForLinkAppService solicitacaoModificacaoEndereco,
            IFluxoWebForLinkAppService fluxo,
            IPapelWebForLinkAppService papel,
            ITramiteWebForLinkAppService tramite)
        {
            _enderecoService = endereco;
            _contratantePjPfService = contratantePjPf;
            _solicitacaoModificacaoEnderecoService = solicitacaoModificacaoEndereco;
            _fluxoService = fluxo;
            _papelService = papel;
            _tramiteService = tramite;
        }

        [HttpPost]
        public ActionResult Incluir()
        {
            var dadosEnderecos = new DadosEnderecosVM();
            PersistirDadosEnderecoEmMemoria();
            return PartialView("~/Views/Shared/EditorTemplates/DadosEnderecosVM.cshtml", dadosEnderecos);
        }

        [HttpPost]
        public ActionResult Editar(int contratanteFornecedorID)
        {
            var contratantePjpf = _contratantePjPfService.BuscarPjpfPorContratanteComEndereco(contratanteFornecedorID);
            var pjpf = contratantePjpf.WFD_PJPF;
            var dadosEnderecos = contratantePjpf.WFD_PJPF_ENDERECO.ToList();

            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
            ficha = Mapper.Map<Fornecedor, FichaCadastralWebForLinkVM>(pjpf);
            ficha.DadosEnderecos = Mapper.Map<List<FORNECEDOR_ENDERECO>, List<DadosEnderecosVM>>(dadosEnderecos);
            PersistirDadosEnderecoEmMemoria();
            return PartialView("~/Views/Fornecedores/_FichaCadastral_DadosEnderecos_Editavel.cshtml", ficha);
        }

        [HttpPost]
        public ActionResult Salvar(FichaCadastralWebForLinkVM model)
        {
            try
            {
                SOLICITACAO solicitacao = new SOLICITACAO();
                solicitacao.FLUXO_ID = _fluxoService.BuscarPorTipoEContratante((int)EnumTiposFluxo.ModificacaoEndereco, model.ContratanteID).ID;
                solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
                solicitacao.USUARIO_ID = (int)Geral.PegaAuthTicket("UsuarioId");
                solicitacao.PJPF_ID = model.ID;

                if (model.ContratanteID != 0)
                    solicitacao.CONTRATANTE_ID = model.ContratanteID;

                var solicitacoesModEndereco = Mapper.Map<List<DadosEnderecosVM>, List<SOLICITACAO_MODIFICACAO_ENDERECO>>(model.DadosEnderecos.ToList());

                if (solicitacoesModEndereco.Any())
                    _solicitacaoModificacaoEnderecoService.InserirSolicitacoes(solicitacoesModEndereco, solicitacao);

                var papelAtual = _papelService.BuscarPorContratanteETipoPapel(solicitacao.CONTRATANTE_ID, (int)EnumTiposPapel.Solicitante).ID;

                int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                _tramiteService.AtualizarTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aprovado, usuarioId);

                PersistirDadosEnderecoEmMemoria();

                string chaveUrl = "";

                if (model.ControllerOrigem == "Fornecedores")
                    chaveUrl = Cripto.Criptografar(String.Format("FornecedorID={0}&ContratanteID={1}&RetSucessoEnderecos=1", model.PJPFID, model.ContratanteID), Key);

                return RedirectToAction(model.ActionOrigem, model.ControllerOrigem, new { chaveurl = chaveUrl });

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                string chaveUrl = "";

                if (model.ControllerOrigem == "Fornecedores")
                    chaveUrl = Cripto.Criptografar(String.Format("FornecedorID={0}&ContratanteID={1}&RetSucessoEnderecos=-1", model.PJPFID, model.ContratanteID), Key);

                return RedirectToAction(model.ActionOrigem, model.ControllerOrigem, new { chaveurl = chaveUrl });
            }
        }

        [HttpPost]
        public ActionResult Cancelar(int contratanteFornecedorID)
        {
            var contratantePjpf = _contratantePjPfService.BuscarPjpfPorContratanteComEndereco(contratanteFornecedorID);
            var pjpf = contratantePjpf.WFD_PJPF;
            var dadosEnderecos = contratantePjpf.WFD_PJPF_ENDERECO.ToList();

            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
            ficha = Mapper.Map<Fornecedor, FichaCadastralWebForLinkVM>(pjpf);
            ficha.DadosEnderecos = Mapper.Map<List<FORNECEDOR_ENDERECO>, List<DadosEnderecosVM>>(dadosEnderecos);

            return PartialView("_FichaCadastral_DadosEnderecos", ficha);
        }

        private void PersistirDadosEnderecoEmMemoria()
        {
            if (TempData["UF"] == null)
                TempData["UF"] = new SelectList(_enderecoService.ListarTodosPorNome(), "UF_SGL", "UF_NM");

            ViewBag.UF = TempData["UF"] as SelectList;
            TempData.Keep("UF");

            if (TempData["TipoEndereco"] == null)
                TempData["TipoEndereco"] = new SelectList(_enderecoService.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");

            ViewBag.TipoEndereco = TempData["TipoEndereco"] as SelectList;
            TempData.Keep("TipoEndereco");
        }

    }
}