using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Service;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DadosContatosController : ControllerPadrao, IModificacaoFichaCadastral
    {
        #region Chamadas Para BP
        private readonly IFornecedorContatoWebForLinkAppService _fornecedorContatosService;
        private readonly ISolicitacaoModificacaoContatoWebForLinkAppService _solicitacaoModificacaoContatoService;
        private readonly ITramiteWebForLinkAppService _tramiteService;
        private readonly IBancoWebForLinkAppService _bancoService;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly IFluxoWebForLinkAppService _fluxoService;

        public DadosContatosController(
            IFornecedorContatoWebForLinkAppService pjPfContatos,
            ISolicitacaoModificacaoContatoWebForLinkAppService solicitacaoModificacaoContato,
            ITramiteWebForLinkAppService tramite,
        IBancoWebForLinkAppService banco,
        ISolicitacaoWebForLinkAppService solicitacao,
        IFluxoWebForLinkAppService fluxo)
        {
            _fornecedorContatosService = pjPfContatos;
            _solicitacaoModificacaoContatoService = solicitacaoModificacaoContato;
            _tramiteService = tramite;
            _bancoService = banco;
            _solicitacaoService = solicitacao;
            _fluxoService = fluxo;
        }
        #endregion

        [HttpPost]
        public ActionResult Incluir()
        {
            var dadosContatos = new DadosContatoVM();
            PersistirDadosEmMemoria();
            return PartialView("~/Views/Shared/EditorTemplates/DadosContatoVM.cshtml", dadosContatos);
        }

        [HttpPost]
        public ActionResult Editar(int contratanteFornecedorID)
        {
            return PartialView("~/Views/Fornecedores/_FichaCadastral_Contatos_Editavel", Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(_fornecedorContatosService.ListarPorContratantePJPFId(contratanteFornecedorID)));
        }

        [HttpPost]
        public ActionResult EditarDadosContatosSolicitacao(int SolicitacaoId)
        {
            return PartialView("_FichaCadastral_Contatos_Editavel", Mapper.Map<List<SolicitacaoModificacaoDadosContato>, List<DadosContatoVM>>(_solicitacaoModificacaoContatoService.ListarPorSolicitacaoId(SolicitacaoId)));
        }

        [HttpPost]
        public ActionResult Salvar(FichaCadastralWebForLinkVM model)
        {
            _solicitacaoModificacaoContatoService.IncluirSolicitacao(model.ContratanteID, model.ID, (int)Geral.PegaAuthTicket("UsuarioId"),
                    _fluxoService.BuscarPorTipoEContratante((int)EnumTiposFluxo.ModificacaoDadosContato, model.ContratanteID).ID);


            var solicitacao = CriarSolicitacao(model, (int)EnumTiposFluxo.ModificacaoDadosContato);
            var solicitacoesModContato = Mapper.Map<List<DadosContatoVM>, List<SolicitacaoModificacaoDadosContato>>(model.DadosContatos.ToList());

            solicitacoesModContato.Select(x =>
            {
                x.SOLICITACAO_ID = solicitacao.ID;
                x.CONTRATANTE_ID = model.ContratanteID;
                x.PJPF_ID = model.ID;
                return x;
            }).ToList();

            _solicitacaoModificacaoContatoService.InserirSolicitacoes(solicitacoesModContato);

            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            _tramiteService.AtualizarTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, (int)EnumPapeisWorkflow.Solicitante, (int)EnumStatusTramite.Aprovado, usuarioId);

            var dadosContatosFinalizados = _fornecedorContatosService.ListarPorContratantePJPFId(model.ContratanteFornecedorID);
            return PartialView("_FichaCadastral_Contatos", Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(dadosContatosFinalizados));
        }

        [HttpPost]
        public ActionResult Cancelar(int contratanteFornecedorID)
        {
            var dadosContatos = _fornecedorContatosService.ListarPorContratantePJPFId(contratanteFornecedorID);

            return PartialView("_FichaCadastral_Contatos", Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(dadosContatos));
        }

        private SOLICITACAO CriarSolicitacao(FichaCadastralWebForLinkVM model, int tipoFluxoId)
        {
            SOLICITACAO solicitacao = new SOLICITACAO();

            PopularSolicitacaoEmAprovacao(model.ContratanteID,
                model.ID,
                (int)Geral.PegaAuthTicket("UsuarioId"),
                _fluxoService.BuscarPorTipoEContratante(tipoFluxoId, model.ContratanteID).ID,
                solicitacao);

            return _solicitacaoService.InserirSolicitacao(solicitacao);
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

        private void PersistirDadosEmMemoria()
        {
            //ViewBag.Bancos
            if (TempData["Bancos"] == null)
                TempData["Bancos"] = new SelectList(_bancoService.ListarTodosPorNome(), "ID", "BANCO_NM");

            ViewBag.Bancos = TempData["Bancos"] as SelectList;
            TempData.Keep("Bancos");
        }
    }
}