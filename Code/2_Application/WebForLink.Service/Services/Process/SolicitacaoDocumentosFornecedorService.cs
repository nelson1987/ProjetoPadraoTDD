using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoDocumentosFornecedorWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoDocumentosFornecedorWebForLinkAppService
    {
        private readonly ISolicitacaoDocumentoWebForLinkService _solicitacaoDocumentoService;

        public SolicitacaoDocumentosFornecedorWebForLinkAppService(
            ISolicitacaoDocumentoWebForLinkService solicitacaoDocumentoService)
        {
            try
            {
                _solicitacaoDocumentoService = solicitacaoDocumentoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SolicitacaoDeDocumentos BuscarPorId(int id)
        {
            try
            {
                return _solicitacaoDocumentoService.Get(id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(String.Format("Ocorreu um erro ao buscar o documento: {0}.", e));
            }
        }

        public List<SolicitacaoDeDocumentos> ListarPorSolicitacaoId(int solicitacaoId)
        {
            try
            {
                return _solicitacaoDocumentoService.Find(d => d.SOLICITACAO_ID == solicitacaoId).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(String.Format("Ocorreu um erro ao buscar os documentos: {0}.", e));
            }
        }

        public void InserirSolicitacoes(List<SolicitacaoDeDocumentos> solicitacoes)
        {
            try
            {
                foreach (var item in solicitacoes)
                {
                    _solicitacaoDocumentoService.Add(item);
                }
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao inserir a solicitação de modificação de documentos.", e);
            }
        }

        public void AtualizarSolicitacao(SolicitacaoDeDocumentos solicitacaoDeDocumentos)
        {
            try
            {
                _solicitacaoDocumentoService.Update(solicitacaoDeDocumentos);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    String.Format("Ocorreu um erro ao atualizar a solicitação de documentos: {0}.", e));
            }
        }

        public int AdicionaDocumentosSolicitacao(int SolicitacaoId, int DescricaoDocumentoId)
        {
            try
            {
                var documento = new SolicitacaoDeDocumentos();
                documento.DESCRICAO_DOCUMENTO_ID = DescricaoDocumentoId;
                documento.SOLICITACAO_ID = SolicitacaoId;

                _solicitacaoDocumentoService.Add(documento);

                return documento.ID;
            }
            catch (Exception e)
            {
                new ServiceWebForLinkException("Erro ao tentar incluir pré-cadastro.", e);
                return -1;
            }
        }

        public bool DocumentoDuplicado(int SolicitacaoId, int DescricaoDocumentoId)
        {
            try
            {
                return
                    _solicitacaoDocumentoService.Find(
                        x => x.SOLICITACAO_ID == SolicitacaoId && x.DESCRICAO_DOCUMENTO_ID == DescricaoDocumentoId)
                        .Any();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao tentar verificar duplicidade de documentos na Solicitação", e);
            }
        }

        public int RemoverDocumentosSolicitacao(int SolicitacaoId, int DescricaoDocumentoId)
        {
            try
            {
                var documento =
                    _solicitacaoDocumentoService.Get(
                        x => x.SOLICITACAO_ID == SolicitacaoId && x.DESCRICAO_DOCUMENTO_ID == DescricaoDocumentoId);

                if (documento.LISTA_DOCUMENTO_ID == null)
                {
                    _solicitacaoDocumentoService.Delete(documento);
                    return 1;
                }
                return -1;
            }
            catch (Exception e)
            {
                new ServiceWebForLinkException("Erro ao tentar Remover Documentos de Solicitacao.", e);
                return -1;
            }
        }

        public void Dispose()
        {
        }
    }
}