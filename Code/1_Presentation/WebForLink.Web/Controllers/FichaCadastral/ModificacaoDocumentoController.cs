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
using WebForLink.Domain.Infrastructure.Service;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class ModificacaoDocumentoController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IPapelWebForLinkAppService _papelService;
        private readonly IFluxoWebForLinkAppService _fluxoService;
        private readonly IFornecedorArquivoWebForLinkAppService _fornecedorArquivoService;
        private readonly IFornecedorDocumentoWebForLinkAppService _fornecedorDocumentoService;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly ISolicitacaoDocumentoWebForLinkAppService _solicitacaoDocumentoService;
        private readonly ITramiteWebForLinkAppService _tramiteService;
        #endregion

        public ModificacaoDocumentoController(
            IPapelWebForLinkAppService papel, 
            IFluxoWebForLinkAppService fluxo, 
            IFornecedorArquivoWebForLinkAppService fornecedorArquivo,
            IFornecedorDocumentoWebForLinkAppService pjPfDocumentos,
            ISolicitacaoWebForLinkAppService solicitacao,
            ISolicitacaoDocumentoWebForLinkAppService solicitacaoDocumento, 
            ITramiteWebForLinkAppService tramite)
        {
            _papelService = papel;
            _fluxoService = fluxo;
            _fornecedorArquivoService = fornecedorArquivo;
            _fornecedorDocumentoService = pjPfDocumentos;
            _solicitacaoService = solicitacao;
            _solicitacaoDocumentoService = solicitacaoDocumento;
            _tramiteService = tramite;
        }
        #region Modificação Documentos
        [HttpPost]
        public ActionResult EditarDocumentos(int fornecedorID, int contratanteID)
        {
            var pjpf = _fornecedorDocumentoService.BuscarPorPJPFId(fornecedorID);

            var documentos = pjpf.WFD_CONTRATANTE_PJPF.FirstOrDefault(c => c.CONTRATANTE_ID == contratanteID).WFD_PJPF_DOCUMENTOS.ToList();
            var documentosVM = Mapper.Map<List<DocumentosDoFornecedor>, List<SolicitacaoDocumentosVM>>(documentos);
            documentosVM.ForEach(x => x.DataValidade = null);
            var OutrosContratantes = pjpf.WFD_CONTRATANTE_PJPF.Where(x => x.CONTRATANTE_ID != contratanteID).ToList();

            foreach (var item in documentosVM)
            {
                if (OutrosContratantes.Any(x => x.WFD_PJPF_DOCUMENTOS.Any(y => y.DescricaoDeDocumentos.DESCRICAO_DOCUMENTOS_CH_ID == item.DescricaoDocumentoId_CH)))
                {
                    item.UsadoEmOutroContratante = true;
                }

            }

            return PartialView("_FichaCadastral_Anexos_Editavel", documentosVM);
        }

        [HttpPost]
        public ActionResult SalvarDocumentos(FichaCadastralWebForLinkVM model)
        {
            try
            {
                if (model.AtualizacaoDocumento)
                    AtualizarSolicitacaoDocumentos(model, (int)EnumTiposFluxo.ModificacaoDocumentos, model.ID);
                else
                    CriarSolicitacaoDocumento(model);

                string chaveUrl = "";

                if (model.ControllerOrigem == "Documento")
                    chaveUrl = Cripto.Criptografar(String.Format("SolicitacaoID=0&FornecedorID={0}&ContratanteID={1}&RetSucessoDocs=1", model.PJPFID, model.ContratanteID), Key);
                else if (model.ControllerOrigem == "Fornecedores")
                    chaveUrl = Cripto.Criptografar(String.Format("FornecedorID={0}&ContratanteID={1}&RetSucessoDocs=1", model.PJPFID, model.ContratanteID), Key);

                return RedirectToAction(model.ActionOrigem, model.ControllerOrigem, new { chaveurl = chaveUrl });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                string chaveUrl = "";

                if (model.ControllerOrigem == "Documento")
                    chaveUrl = Cripto.Criptografar(String.Format("SolicitacaoID=0&FornecedorID={0}&ContratanteID={1}&RetSucessoDocs=-1", model.PJPFID, model.ContratanteID), Key);
                else if (model.ControllerOrigem == "Fornecedores")
                    chaveUrl = Cripto.Criptografar(String.Format("FornecedorID={0}&ContratanteID={1}&RetSucessoDocs=-1", model.PJPFID, model.ContratanteID), Key);

                return RedirectToAction(model.ActionOrigem, model.ControllerOrigem, new { chaveurl = chaveUrl });
            }

        }

        [HttpPost]
        public ActionResult CancelarDocumentos(int fornecedorID, int contratanteID)
        {
            var pjpf = _fornecedorDocumentoService.BuscarPorPJPFId(fornecedorID);

            var documentos = pjpf.WFD_CONTRATANTE_PJPF.FirstOrDefault(c => c.CONTRATANTE_ID == contratanteID).WFD_PJPF_DOCUMENTOS.ToList();
            var solicitacaoFornecedorVM = new SolicitacaoFornecedorVM();

            solicitacaoFornecedorVM.Documentos = Mapper.Map<List<DocumentosDoFornecedor>, List<SolicitacaoDocumentosVM>>(documentos);

            return PartialView("_FichaCadastral_Anexos", solicitacaoFornecedorVM);
        }
        #endregion

        private void CriarSolicitacaoDocumento(FichaCadastralWebForLinkVM model)
        {
            var solicitacao = CriarSolicitacaoDocumentos(model, (int)EnumTiposFluxo.ModificacaoDocumentos);

            foreach (var item in model.SolicitacaoFornecedor.Documentos.Where(x => x.AtualizarDocOutrosContratantes == true).ToArray())
            {
                if (item.DescricaoDocumentoId_CH > 0)
                {
                    var pjpfs = _fornecedorDocumentoService.BuscarDocumentoOutrosContratantes(model.ContratanteID, model.PJPFID, item.DescricaoDocumentoId_CH);

                    foreach (var subitem in pjpfs)
                    {
                        FichaCadastralWebForLinkVM modelOutroContratante = new FichaCadastralWebForLinkVM();
                        modelOutroContratante.ContratanteID = subitem.CONTRATANTE_ID;
                        modelOutroContratante.PJPFID = subitem.PJPF_ID;
                        modelOutroContratante.SolicitacaoFornecedor = new SolicitacaoFornecedorVM();
                        modelOutroContratante.SolicitacaoFornecedor.Documentos = new List<SolicitacaoDocumentosVM>();

                        SolicitacaoDocumentosVM doc = new SolicitacaoDocumentosVM();
                        doc.NomeArquivo = item.NomeArquivo;
                        doc.Arquivo = item.Arquivo;
                        doc.PorValidade = item.PorValidade;
                        doc.DescricaoDocumentoId = item.DescricaoDocumentoId;
                        doc.ListaDocumentosID = item.ListaDocumentosID;
                        doc.SolicitacaoID = item.SolicitacaoID;

                        modelOutroContratante.SolicitacaoFornecedor.Documentos.Add(doc);

                        CriarSolicitacaoDocumentos(modelOutroContratante, (int)EnumTiposFluxo.ModificacaoDocumentos);
                    }
                }
            }
        }

        private void AtualizarSolicitacaoDocumentos(FichaCadastralWebForLinkVM model, int tipoFluxoId, int solicitacaoId)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            List<SolicitacaoDeDocumentos> documentosList = new List<SolicitacaoDeDocumentos>();
            model.SolicitacaoFornecedor.Documentos
                .ForEach(x =>
                {
                    SolicitacaoDeDocumentos solicitacaoDeDocumentosSolicitada = _solicitacaoDocumentoService.BuscarPorIdSolicitacaoIdDescricaoDocumento((int)x.SolicitacaoID, x.DescricaoDocumentoId);
                    if (!string.IsNullOrEmpty(x.ArquivoSubido) && solicitacaoDeDocumentosSolicitada != null)
                    {
                        int idSolicitacaoDocumento = solicitacaoDeDocumentosSolicitada.ID;
                        var arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(model.ContratanteID, x.ArquivoSubido, x.TipoArquivoSubido);

                        SolicitacaoDeDocumentos solicitacaoDeDocumentos = new SolicitacaoDeDocumentos()
                        {
                            ID = idSolicitacaoDocumento,
                            ARQUIVO_ID = arquivoId,
                            DATA_VENCIMENTO = x.PorValidade != null ? ((bool)x.PorValidade ? x.DataValidade : null) : null,
                            SOLICITACAO_ID = x.SolicitacaoID,
                            DESCRICAO_DOCUMENTO_ID = solicitacaoDeDocumentosSolicitada.DESCRICAO_DOCUMENTO_ID,
                            LISTA_DOCUMENTO_ID = solicitacaoDeDocumentosSolicitada.LISTA_DOCUMENTO_ID
                        };
                        documentosList.Add(solicitacaoDeDocumentos);
                    }
                });
            documentosList.ForEach(x =>
            {
                _solicitacaoDocumentoService.Update(x);
            });

            var papelAtual = _papelService.BuscarPorContratanteETipoPapel(model.ContratanteID, (int)EnumTiposPapel.Fornecedor).ID;

            var solDocumentos = _solicitacaoService.BuscarPorId(solicitacaoId);

            _tramiteService.AtualizarTramite(model.ContratanteID, solicitacaoId, solDocumentos.FLUXO_ID, papelAtual, 2, solDocumentos.USUARIO_ID);
        }

        private SOLICITACAO CriarSolicitacaoDocumentos(FichaCadastralWebForLinkVM model, int tipoFluxoId)
        {
            var solicitacao = new SOLICITACAO
            {
                CONTRATANTE_ID = model.ContratanteID,
                FLUXO_ID = _fluxoService.BuscarPorTipoEContratante(tipoFluxoId, model.ContratanteID).ID,
                USUARIO_ID = (int)Geral.PegaAuthTicket("UsuarioId"),
                SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.Aguardando,
                PJPF_ID = model.PJPFID,
            };

            List<SolicitacaoDeDocumentos> documentosList = new List<SolicitacaoDeDocumentos>();
            model.SolicitacaoFornecedor.Documentos
                .ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ArquivoSubido))
                    {
                        var arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(model.ContratanteID, x.ArquivoSubido, x.TipoArquivoSubido);

                        SolicitacaoDeDocumentos solicitacaoDeDocumentos = new SolicitacaoDeDocumentos()
                        {
                            DATA_UPLOAD = DateTime.Now,
                            NOME_ARQUIVO = _fornecedorArquivoService.PegaNomeArquivoSubido(x.ArquivoSubido),
                            EXTENSAO_ARQUIVO = x.TipoArquivoSubido,
                            ARQUIVO_ID = arquivoId,
                            DATA_VENCIMENTO = x.PorValidade != null ? ((bool)x.PorValidade ? x.DataValidade : null) : null,
                            DESCRICAO_DOCUMENTO_ID = x.DescricaoDocumentoId,
                            LISTA_DOCUMENTO_ID = x.ListaDocumentosID
                        };
                        documentosList.Add(solicitacaoDeDocumentos);
                    }
                });

            SOLICITACAO soldocumentos = _solicitacaoService.InserirSolicitacaoDocumentos(solicitacao, documentosList);

            var papelAtual = _papelService.BuscarPorContratanteETipoPapel(model.ContratanteID, (int)EnumTiposPapel.Solicitante).ID;

            _tramiteService.AtualizarTramite(model.ContratanteID, soldocumentos.ID, soldocumentos.FLUXO_ID, papelAtual, 2, soldocumentos.USUARIO_ID);
            return soldocumentos;

        }

    }
}