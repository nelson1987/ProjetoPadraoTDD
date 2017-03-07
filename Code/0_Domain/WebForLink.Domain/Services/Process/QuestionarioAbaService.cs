using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class QuestionarioAbaWebForLinkService : Service<QUESTIONARIO_ABA>, IQuestionarioAbaWebForLinkService
    {
        private readonly IAbaQuestionarioWebForLinkRepository _abaQuestionarioRepository;

        public QuestionarioAbaWebForLinkService(IAbaQuestionarioWebForLinkRepository abaQuestionarioRepository)
            : base(abaQuestionarioRepository)
        {
            try
            {
                _abaQuestionarioRepository = abaQuestionarioRepository;
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
        public QUESTIONARIO_ABA BuscarPorID(int id)
        {
            try
            {
                return _abaQuestionarioRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }

    public interface IAbaQuestionarioWebForLinkRepository : IRepository<QUESTIONARIO_ABA>
    {
        QUESTIONARIO_ABA BuscarPorId(int id);
    }
}