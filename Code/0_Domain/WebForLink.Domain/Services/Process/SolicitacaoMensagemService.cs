using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoMensagemWebForLinkService : IService<SOLICITACAO_MENSAGEM>
    {
        void InserirMensagem(SOLICITACAO_MENSAGEM Mensagem, List<SolicitacaoDeDocumentos> docs);
    }

    public class SolicitacaoMensagemWebForLinkService : Service<SOLICITACAO_MENSAGEM>,
        ISolicitacaoMensagemWebForLinkService
    {
        private readonly ISolicitacaoMensagemWebForLinkRepository _solicitacaoMensagemRepository;

        public SolicitacaoMensagemWebForLinkService(
            ISolicitacaoMensagemWebForLinkRepository solicitacaoMensagemRepository)
            : base(solicitacaoMensagemRepository)
        {
            try
            {
                _solicitacaoMensagemRepository = solicitacaoMensagemRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="Mensagem"></param>
        /// <param name="docs"></param>
        public void InserirMensagem(SOLICITACAO_MENSAGEM Mensagem, List<SolicitacaoDeDocumentos> docs)
        {
            try
            {
                Mensagem.WFD_SOL_DOCUMENTOS = docs;
                _solicitacaoMensagemRepository.Add(Mensagem);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao inserir a solicitação de mensagem.", e);
            }
        }

        public void Dispose()
        {
        }
    }
}