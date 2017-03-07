using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class DocumentosSolicitacaoDapperRepository : Common.Repository, IDocumentosSolicitacaoReadOnlyRepository
    {
        public IEnumerable<DocumentoSolicitacao> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentoSolicitacao> Find(Expression<Func<DocumentoSolicitacao, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DocumentoSolicitacao Get(int id)
        {
            throw new NotImplementedException();
        }

        public DocumentoSolicitacao GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }
    }
}