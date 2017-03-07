using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class QuestionarioAbaPerguntaPapelWebForLinkAppService : AppService<WebForLinkContexto>,
        IQuestionarioAbaPerguntaPapelWebForLinkAppService
    {
        public QuestionarioAbaPerguntaPapelWebForLinkAppService()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public QUESTIONARIO_PAPEL Get(Expression<Func<QUESTIONARIO_PAPEL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PAPEL> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PAPEL> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(QUESTIONARIO_PAPEL entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IEnumerable<QUESTIONARIO_PAPEL> Find(Expression<Func<QUESTIONARIO_PAPEL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PAPEL> Find(Expression<Func<QUESTIONARIO_PAPEL, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PAPEL Get(int id)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PAPEL Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PAPEL Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PAPEL GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(QUESTIONARIO_PAPEL entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(QUESTIONARIO_PAPEL entity)
        {
            throw new NotImplementedException();
        }
    }
}