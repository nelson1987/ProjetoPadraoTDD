using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoDocumentoWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoDocumentoWebForLinkAppService
    {
        private readonly ISolicitacaoDocumentoWebForLinkService _solicitacaoDocumentoService;

        public SolicitacaoDocumentoWebForLinkAppService(
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

        public SolicitacaoDeDocumentos Update(SolicitacaoDeDocumentos pergunta)
        {
            try
            {
                _solicitacaoDocumentoService.Update(pergunta);
                return pergunta;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma Pergunta", ex);
            }
        }

        public List<SolicitacaoDeDocumentos> ListarPorIdSolicitacao(int id)
        {
            try
            {
                return _solicitacaoDocumentoService.ListarPorIdSolicitacao(id);
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
                return _solicitacaoDocumentoService.Find(y => y.SOLICITACAO_ID == solicitacaoId &&
                                                              y.DESCRICAO_DOCUMENTO_ID == descricaoDocumentoId)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Solicitação de documento por id", ex);
            }
        }

        public void Dispose()
        {
        }

        public SolicitacaoDeDocumentos Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoDeDocumentos Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoDeDocumentos GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoDeDocumentos> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoDeDocumentos> Find(Expression<Func<SolicitacaoDeDocumentos, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SolicitacaoDeDocumentos entity)
        {
            throw new NotImplementedException();
        }

        ValidationResult IWriteOnlyAppService<SolicitacaoDeDocumentos>.Update(SolicitacaoDeDocumentos entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SolicitacaoDeDocumentos entity)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoDeDocumentos Get(int id)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoDeDocumentos Get(Expression<Func<SolicitacaoDeDocumentos, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoDeDocumentos> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoDeDocumentos> Find(Expression<Func<SolicitacaoDeDocumentos, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}