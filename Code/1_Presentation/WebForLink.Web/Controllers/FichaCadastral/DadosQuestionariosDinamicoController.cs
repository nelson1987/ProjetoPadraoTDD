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
using WebForLink.Domain.Infrastructure.Service;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DadosQuestionarioDinamicoController : ControllerPadrao
    {
        #region Chamadas Para Service
        private readonly IPapelWebForLinkAppService _papelService;
        private readonly IFluxoWebForLinkAppService _fluxoService;
        private readonly ICadastroUnicoWebForLinkAppService _cadastroUnicoService;
        private readonly IInformacaoComplementarWebForLinkAppService _informacaoComplementarService;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly ITramiteWebForLinkAppService _tramiteService;

        public DadosQuestionarioDinamicoController(
            IPapelWebForLinkAppService papel,
            IFluxoWebForLinkAppService fluxo,
            ICadastroUnicoWebForLinkAppService cadastroUnico,
            IInformacaoComplementarWebForLinkAppService informacaoComplementar,
            ISolicitacaoWebForLinkAppService solicitacao, 
            ITramiteWebForLinkAppService tramite)
        {
            _papelService = papel;
            _fluxoService = fluxo;
            _cadastroUnicoService = cadastroUnico;
            _informacaoComplementarService = informacaoComplementar;
            _solicitacaoService = solicitacao;
            _tramiteService = tramite;
        }
        #endregion

        #region Modificação Questionário Dinâmico
        [HttpPost]
        public ActionResult EditarQuestionarioDinamico(int contratanteFornecedorID, int contratanteID, int fornecedorID, int? tipoFluxoID, int categoriaID, int TpPapel)
        {
            var papelAtual = _papelService.BuscarPorContratanteETipoPapel(contratanteID, TpPapel).ID;

            return PartialView("_FichaCadastral_QuestionarioDinamico_Editavel", new RetornoQuestionario<QuestionarioVM>()
            {
                QuestionarioDinamicoList =
                            Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                                _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                                {
                                    ContratanteId = contratanteID,
                                    PapelId = papelAtual,
                                    CategoriaId = categoriaID,
                                    Alteracao = true,
                                    SolicitacaoId = 0,
                                    FornecedorId = fornecedorID,
                                    ContratantePJPFId = contratanteFornecedorID
                                })
                            ),
            });
        }
        [HttpPost]
        public ActionResult SalvarQuestionarioDinamico(FichaCadastralWebForLinkVM model)
        {
            var solicitacaoId = model.SolicitacaoID;

            var solicitacao = CriarSolicitacao(model, (int)EnumTiposFluxo.ModificacaoQuestionarioDinamico);

            foreach (var item in model.Questionarios.QuestionarioDinamicoList)
            {
                var questionario = item;
                List<SalvaInformacaComplementarVM> respostaList = (
                        from aba in questionario.AbaList
                        from pergunta in aba.PerguntaList
                        select new SalvaInformacaComplementarVM
                        {
                            PerguntaId = pergunta.Id,
                            Resposta = model.SolicitacaoID != null ? pergunta.Resposta : pergunta.RespostaFornecedor,
                            SolicitacaoId = solicitacao.ID,
                            RespostaId = model.SolicitacaoID != null ? pergunta.RespostaId : pergunta.RespostaFornecedorId
                        }
                    ).ToList();
                _informacaoComplementarService.InsertAll(Mapper.Map<List<WFD_INFORM_COMPL>>(respostaList));
            }
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            _tramiteService.AtualizarTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, (int)EnumPapeisWorkflow.Solicitante, (int)EnumStatusTramite.Aprovado, usuarioId);

            var papelAtual = _papelService.BuscarPorContratanteETipoPapel(model.ContratanteID, (int)EnumTiposPapel.Solicitante).ID;

            return PartialView("_FichaCadastral_QuestionarioDinamico", new RetornoQuestionario<QuestionarioVM>()
            {
                QuestionarioDinamicoList =
                    Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                        _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                        {
                            ContratanteId = model.ContratanteID,
                            PapelId = papelAtual,
                            CategoriaId = model.CategoriaId,
                            Alteracao = false,
                            SolicitacaoId = solicitacao.ID,
                            ContratantePJPFId = model.ContratanteFornecedorID
                        })
                    ),
            });
        }
        [HttpPost]
        public ActionResult CancelarQuestionarioDinamico(int contratanteFornecedorID, int contratanteID, int fornecedorID, int? tipoFluxoID, int categoriaID, int TpPapel)
        {
            var papelAtual = _papelService.BuscarPorContratanteETipoPapel(contratanteID, TpPapel).ID;

            return PartialView("_FichaCadastral_QuestionarioDinamico", new RetornoQuestionario<QuestionarioVM>()
            {
                QuestionarioDinamicoList =
                    Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                        _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO()
                        {
                            ContratanteId = contratanteID,
                            PapelId = papelAtual,
                            CategoriaId = categoriaID,
                            Alteracao = true,
                            SolicitacaoId = 0,
                            FornecedorId = fornecedorID,
                            ContratantePJPFId = contratanteFornecedorID
                        })
                    ),
            });
        }
        #endregion

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

    }
}