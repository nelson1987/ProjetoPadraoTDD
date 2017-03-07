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
    public class QuestionarioAbaWebForLinkAppService : AppService<WebForLinkContexto>,
        IQuestionarioAbaWebForLinkAppService
    {
        private readonly IAbaQuestionarioService _abaQuestionarioService;

        public QuestionarioAbaWebForLinkAppService(IAbaQuestionarioService abaQuestionarioService)
        {
            try
            {
                _abaQuestionarioService = abaQuestionarioService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public QUESTIONARIO_ABA Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_ABA Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_ABA GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_ABA> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_ABA> Find(Expression<Func<QUESTIONARIO_ABA, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(QUESTIONARIO_ABA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(QUESTIONARIO_ABA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(QUESTIONARIO_ABA entity)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_ABA Get(int id)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_ABA Get(Expression<Func<QUESTIONARIO_ABA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_ABA> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_ABA> Find(Expression<Func<QUESTIONARIO_ABA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QUESTIONARIO_ABA BuscarPorID(int id)
        {
            try
            {
                return _abaQuestionarioService.BuscarPorId(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }

    public interface IAbaQuestionarioService
    {
        QUESTIONARIO_ABA BuscarPorId(int id);
    }
}