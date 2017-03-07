using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class AbaPerguntaWebForLinkAppService : AppService<WebForLinkContexto>, IAbaPerguntaWebForLinkAppService
    {
        private readonly IAbaPerguntaWebForLinkAppService _fichaCadastralService;

        public AbaPerguntaWebForLinkAppService(IAbaPerguntaWebForLinkAppService fichaCadastral)
        {
            _fichaCadastralService = fichaCadastral;
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(QUESTIONARIO_PERGUNTA entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Commit();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> Find(Expression<Func<QUESTIONARIO_PERGUNTA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_PERGUNTA> Find(Expression<Func<QUESTIONARIO_PERGUNTA, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(Expression<Func<QUESTIONARIO_PERGUNTA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(int id)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_PERGUNTA GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(QUESTIONARIO_PERGUNTA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(QUESTIONARIO_PERGUNTA entity)
        {
            throw new NotImplementedException();
        }
    }
}