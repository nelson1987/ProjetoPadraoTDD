﻿using System;
using System.Linq;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class ImportacaoWebForLinkAppService : AppService<WebForLinkContexto>, IImportacaoWebForLinkAppService
    {
        private readonly IContratanteConfiguracaoWebForLinkService _contratanteConfiguracaoBp;
        private readonly IFluxoWebForLinkService _fluxobp;
        private readonly IFornecedorBaseWebForLinkService _fornecedorBaseService;
        private readonly IFornecedorListaDocumentosWebForLinkService _listaDoc;
        private readonly IFornecedorBaseWebForLinkService _pjPfBaseBp;
        private readonly ISolicitacaoWebForLinkService _solicitacaoBp;
        private readonly ISolicitacaoProrrogacaoWebForLinkService _solicitacaoProrrogacaoPrazoService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public ImportacaoWebForLinkAppService(
            ISolicitacaoWebForLinkService solicitacao,
            IFornecedorBaseWebForLinkService fornecedorBase,
            ISolicitacaoProrrogacaoWebForLinkService solicitacaoProrrogacaoPrazo,
            IFluxoWebForLinkService fluxo,
            IFornecedorBaseWebForLinkService pjPfBase,
            IContratanteConfiguracaoWebForLinkService contratanteConfiguracao,
            IFornecedorListaDocumentosWebForLinkService listaDoc, ISolicitacaoWebForLinkService solicitacaoService)
        {
            _solicitacaoService = solicitacaoService;
            try
            {
                _listaDoc = listaDoc;
                _fornecedorBaseService = fornecedorBase;
                _solicitacaoProrrogacaoPrazoService = solicitacaoProrrogacaoPrazo;
                _fluxobp = fluxo;
                _pjPfBaseBp = pjPfBase;
                _contratanteConfiguracaoBp = contratanteConfiguracao;
                _solicitacaoBp = solicitacao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        ///     Importar fornecedores enviando e-mail de convite.
        /// </summary>
        /// <param name="idFornecedorBase">Id de fornecedores da tabela de importação</param>
        /// <param name="idUsuario">Categoria dos fornecedores a serem importados</param>
        /// <param name="assuntoMensagem">Assunto do E-mail</param>
        /// <param name="mensagemTexto">Corpo do E-mail</param>
        /// <returns>Importar DTO</returns>
        public ImportacaoDTO ImportarComConvite(int idFornecedorBase, int idUsuario, string assuntoMensagem,
            string mensagemTexto)
        {
            try
            {
                var pjpfBase = _pjPfBaseBp.Get(idFornecedorBase);
                //Usuario wfdUsuario = usuarioBP.Get(idUsuario);
                var tpFluxo = pjpfBase.PJPF_TIPO == 1 ? 10 : 30;

                var fluxoid = _fluxobp.BuscarPorTipoEContratante(tpFluxo, pjpfBase.CONTRATANTE_ID).ID;

                #region Solicitacao

                var solicitacao = new SOLICITACAO
                {
                    CONTRATANTE_ID = pjpfBase.CONTRATANTE_ID,
                    FLUXO_ID = fluxoid,
                    SOLICITACAO_STATUS_ID = 5,
                    SOLICITACAO_DT_CRIA = DateTime.Now,
                    PJPF_ID = null,
                    PJPF_BASE_ID = pjpfBase.ID,
                    MOTIVO = null,
                    TP_PJPF = null,
                    DT_PRAZO =
                        DateTime.Now.AddDays(_contratanteConfiguracaoBp.Get(pjpfBase.CONTRATANTE_ID).PRAZO_ENTREGA_FICHA),
                    USUARIO_ID = idUsuario
                };

                #endregion

                #region SolicitacaoCadastroPjPf

                var solicitacaoCadastroPjPf = new SolicitacaoCadastroFornecedor
                {
                    SOLICITACAO_ID = solicitacao.ID,
                    CATEGORIA_ID = pjpfBase.CATEGORIA_ID != null ? (int) pjpfBase.CATEGORIA_ID : 1,
                    PJPF_TIPO = pjpfBase.PJPF_TIPO,
                    CPF = pjpfBase.CPF,
                    CNPJ = pjpfBase.CNPJ,
                    RAZAO_SOCIAL = pjpfBase.RAZAO_SOCIAL,
                    NOME = pjpfBase.NOME,
                    NOME_FANTASIA = pjpfBase.NOME_FANTASIA,
                    CNAE = pjpfBase.CNAE,
                    INSCR_ESTADUAL = pjpfBase.INSCR_ESTADUAL,
                    INSCR_MUNICIPAL = pjpfBase.INSCR_MUNICIPAL,
                    TP_LOGRADOURO = pjpfBase.TP_LOGRADOURO.ToString(),
                    ENDERECO = pjpfBase.ENDERECO,
                    NUMERO = pjpfBase.NUMERO,
                    COMPLEMENTO = pjpfBase.COMPLEMENTO,
                    CEP = pjpfBase.CEP,
                    BAIRRO = pjpfBase.BAIRRO,
                    CIDADE = pjpfBase.CIDADE,
                    UF = pjpfBase.UF,
                    PAIS = pjpfBase.PAIS.ToString(),
                    OBSERVACAO = null,
                    EhExpansao = false,
                    EXPANSAO_PARA_CONTR_ID = null,
                    COD_PJPF_ERP = null,
                    ROBO_ID = pjpfBase.ROBO_ID,
                    CLIENTE = null,
                    GRUPO_EMPRESA = null,
                    DT_NASCIMENTO = pjpfBase.DT_NASCIMENTO,
                    WFD_SOLICITACAO = solicitacao
                };

                #endregion

                #region Contato

                var contato = new SolicitacaoModificacaoDadosContato
                {
                    CELULAR = pjpfBase.CELULAR,
                    CONTRATANTE_ID = pjpfBase.CONTRATANTE_ID,
                    EMAIL = pjpfBase.EMAIL,
                    NOME = pjpfBase.NOME_CONTATO,
                    PJPF_ID = null,
                    SOLICITACAO_ID = solicitacao.ID,
                    TELEFONE = pjpfBase.TELEFONE,
                    TP_CONTATO_ID = null,
                    WFD_SOLICITACAO = solicitacao
                };

                #endregion

                #region Documentos por Categoria

                var docs = _listaDoc.BuscarPorCategoriaId(solicitacaoCadastroPjPf.CATEGORIA_ID).Select(x =>
                    new SolicitacaoDeDocumentos
                    {
                        WFD_SOLICITACAO = solicitacao,
                        DESCRICAO_DOCUMENTO_ID = x.DESCRICAO_DOCUMENTO_ID,
                        LISTA_DOCUMENTO_ID = x.ID,
                        OBRIGATORIO = x.OBRIGATORIO,
                        EXIGE_VALIDADE = x.EXIGE_VALIDADE,
                        PERIODICIDADE_ID = x.PERIODICIDADE_ID
                    }).ToList();

                #endregion

                #region Convite

                var convite = new FORNECEDORBASE_CONVITE
                {
                    DT_ENVIO = DateTime.Now,
                    PJPF_BASE_ID = idFornecedorBase,
                    WFD_SOLICITACAO = solicitacao,
                    USUARIO_ID = idUsuario
                };

                #endregion

                var mensagem = new SOLICITACAO_MENSAGEM
                {
                    WFD_SOLICITACAO = solicitacao,
                    ASSUNTO = assuntoMensagem,
                    MENSAGEM = mensagemTexto,
                    DT_ENVIO = DateTime.Now
                };

                _solicitacaoBp.ConvidarFornecedorComSolicitacao(solicitacao, solicitacaoCadastroPjPf, contato, null,
                    docs, convite, mensagem);
                return new ImportacaoDTO
                {
                    Cnpj = pjpfBase.PJPF_TIPO == 1 ? pjpfBase.CNPJ : pjpfBase.CPF,
                    Email = pjpfBase.EMAIL,
                    SolicitacaoId = solicitacao.ID,
                    ContratanteId = pjpfBase.CONTRATANTE_ID,
                    TipoFornecedor = pjpfBase.PJPF_TIPO
                };
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao importar com convite", ex);
            }
        }

        public void ProrrogarPrazo(int solicitacaoId, int idUsuario, DateTime dataProrrogacao, string motivo)
        {
            try
            {
                var prorrogacao = new SOLICITACAO_PRORROGACAO
                {
                    SOLICITACAO_ID = solicitacaoId,
                    DT_PRORROGACAO_PRAZO = dataProrrogacao,
                    MOTIVO_PRORROGACAO = motivo,
                    DT_SOL_PRORROGACAO = DateTime.Now,
                    USUARIO_SOL_ID = idUsuario,
                    APROVADO = null,
                    MOTIVO_REPROVACAO = null,
                    DT_AVALIACAO = null,
                    USUARIO_AVALIACAO_ID = null
                };

                _solicitacaoProrrogacaoPrazoService.Add(prorrogacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao importar com convite", ex);
            }
        }

        public void AvaliarProrrogacao(int[] idsFornecedorBase, int idUsuario, string motivo,
            EnumTiposFuncionalidade avaliacao)
        {
            try
            {
                var pjpfBase = _fornecedorBaseService.Find(x => idsFornecedorBase.Contains(x.ID)).ToList();

                foreach (var item in pjpfBase)
                {
                    var solicitacao =
                        item.WFD_SOLICITACAO.Where(x => x.WFD_SOLICITACAO_PRORROGACAO.Any(y => y.APROVADO == null))
                            .LastOrDefault();
                    var prorrogacao =
                        solicitacao.WFD_SOLICITACAO_PRORROGACAO.Where(x => x.APROVADO == null).LastOrDefault();

                    if (avaliacao == EnumTiposFuncionalidade.AprovarPrazo)
                    {
                        solicitacao.DT_PRORROGACAO_PRAZO = prorrogacao.DT_PRORROGACAO_PRAZO;
                        _solicitacaoService.Update(solicitacao);

                        prorrogacao.APROVADO = true;
                        prorrogacao.DT_AVALIACAO = DateTime.Now;
                        prorrogacao.USUARIO_AVALIACAO_ID = idUsuario;
                        _solicitacaoProrrogacaoPrazoService.Update(prorrogacao);
                    }
                    else
                    {
                        prorrogacao.APROVADO = false;
                        prorrogacao.DT_AVALIACAO = DateTime.Now;
                        prorrogacao.USUARIO_AVALIACAO_ID = idUsuario;
                        prorrogacao.MOTIVO_REPROVACAO = motivo;
                        _solicitacaoProrrogacaoPrazoService.Update(prorrogacao);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao importar com convite", ex);
            }
        }

        public void Dispose()
        {
        }

        #region Acesso BM externa

        #endregion
    }
}