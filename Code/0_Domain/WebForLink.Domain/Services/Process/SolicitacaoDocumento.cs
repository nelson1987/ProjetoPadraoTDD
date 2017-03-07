using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoDocumentoWebForLinkService : IService<SolicitacaoDeDocumentos>
    {
        List<SolicitacaoDeDocumentos> ListarPorIdSolicitacao(int id);
        SolicitacaoDeDocumentos BuscarPorIdSolicitacaoIdDescricaoDocumento(int solicitacaoId, int descricaoDocumentoId);
    }

    public class SolicitacaoDocumentoWebForLinkService : Service<SolicitacaoDeDocumentos>,
        ISolicitacaoDocumentoWebForLinkService
    {
        private readonly ISolicitacaoDocumentoWebForLinkRepository _solicitacaoDocumentoRepository;

        public SolicitacaoDocumentoWebForLinkService(
            ISolicitacaoDocumentoWebForLinkRepository solicitacaoDocumentoRepository)
            : base(solicitacaoDocumentoRepository)
        {
            try
            {
                _solicitacaoDocumentoRepository = solicitacaoDocumentoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<SolicitacaoDeDocumentos> ListarPorIdSolicitacao(int id)
        {
            try
            {
                return _solicitacaoDocumentoRepository.ListarPorIdSolicitacao(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Solicitação de documento por id", ex);
            }
        }

        public SolicitacaoDeDocumentos BuscarPorIdSolicitacaoIdDescricaoDocumento(int solicitacaoId,
            int descricaoDocumentoId)
        {
            try
            {
                return _solicitacaoDocumentoRepository.Get(y => y.SOLICITACAO_ID == solicitacaoId &&
                                                                y.DESCRICAO_DOCUMENTO_ID == descricaoDocumentoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Solicitação de documento por id", ex);
            }
        }

        //public SolicitacaoDeDocumentos Update(SolicitacaoDeDocumentos pergunta)
        //{
        //    try
        //    {
        //        _solicitacaoDocumentoRepository.Update(pergunta);
        //        return pergunta;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao inserir uma Pergunta", ex);
        //    }
        //}

        public void Dispose()
        {
        }
    }
}