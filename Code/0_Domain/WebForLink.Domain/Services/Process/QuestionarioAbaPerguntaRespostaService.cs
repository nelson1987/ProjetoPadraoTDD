using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class QuestionarioAbaPerguntaRespostaService : Service<QUESTIONARIO_RESPOSTA>
    {
        private readonly IQuestionarioRespostaWebForLinkRepository _respostaQuestionarioRepository;

        public QuestionarioAbaPerguntaRespostaService(
            IQuestionarioRespostaWebForLinkRepository respostaQuestionarioRepository)
            : base(respostaQuestionarioRepository)
        {
            try
            {
                _respostaQuestionarioRepository = respostaQuestionarioRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QUESTIONARIO_RESPOSTA BuscarPorID(int id)
        {
            try
            {
                return _respostaQuestionarioRepository.Get(id);
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
                return _respostaQuestionarioRepository.Find(x => x.PERG_ID == idPergunta).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}