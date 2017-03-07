using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class QuestionarioAbaPerguntaRespostaWebForLinkAppService : AppService<WebForLinkContexto>,
        IQuestionarioAbaPerguntaRespostaWebForLinkAppService
    {
        private readonly IQuestionarioRespostaService _respostaQuestionarioService;

        public QuestionarioAbaPerguntaRespostaWebForLinkAppService(
            IQuestionarioRespostaService respostaQuestionarioService)
        {
            try
            {
                _respostaQuestionarioService = respostaQuestionarioService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public QUESTIONARIO_RESPOSTA Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_RESPOSTA Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_RESPOSTA GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_RESPOSTA> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_RESPOSTA> Find(Expression<Func<QUESTIONARIO_RESPOSTA, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(QUESTIONARIO_RESPOSTA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(QUESTIONARIO_RESPOSTA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(QUESTIONARIO_RESPOSTA entity)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_RESPOSTA Get(int id)
        {
            throw new NotImplementedException();
        }

        public QUESTIONARIO_RESPOSTA Get(Expression<Func<QUESTIONARIO_RESPOSTA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_RESPOSTA> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QUESTIONARIO_RESPOSTA> Find(Expression<Func<QUESTIONARIO_RESPOSTA, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QUESTIONARIO_RESPOSTA BuscarPorID(int id)
        {
            try
            {
                return _respostaQuestionarioService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <returns></returns>
        public List<QUESTIONARIO_RESPOSTA> BuscarPorPerguntaId(int idPergunta)
        {
            try
            {
                return _respostaQuestionarioService.Find(x => x.PERG_ID == idPergunta).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}