using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoMensagemWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoMensagemWebForLinkAppService
    {
        private readonly ISolicitacaoMensagemWebForLinkService _solicitacaoMensagemService;

        public SolicitacaoMensagemWebForLinkAppService(ISolicitacaoMensagemWebForLinkService solicitacaoMensagemService)
        {
            try
            {
                _solicitacaoMensagemService = solicitacaoMensagemService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="docs"></param>
        public void InserirMensagem(SOLICITACAO_MENSAGEM mensagem, List<SolicitacaoDeDocumentos> docs)
        {
            try
            {
                mensagem.WFD_SOL_DOCUMENTOS = docs;
                _solicitacaoMensagemService.Add(mensagem);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao inserir a solicitação de mensagem.", e);
            }
        }

        public void Dispose()
        {
        }

        public SOLICITACAO_MENSAGEM Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MENSAGEM Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MENSAGEM GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MENSAGEM> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MENSAGEM> Find(Expression<Func<SOLICITACAO_MENSAGEM, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO_MENSAGEM entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SOLICITACAO_MENSAGEM entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO_MENSAGEM entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MENSAGEM Get(int id)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MENSAGEM Get(Expression<Func<SOLICITACAO_MENSAGEM, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MENSAGEM> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MENSAGEM> Find(Expression<Func<SOLICITACAO_MENSAGEM, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}