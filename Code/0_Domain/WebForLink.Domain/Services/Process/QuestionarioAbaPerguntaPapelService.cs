using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class QuestionarioAbaPerguntaPapelWebForLinkService : Service<QUESTIONARIO_PAPEL>
    {
        private readonly IQuestionarioAbaPerguntaPapelWebForLinkRepository _processo;

        public QuestionarioAbaPerguntaPapelWebForLinkService(IQuestionarioAbaPerguntaPapelWebForLinkRepository processo)
            : base(processo)
        {
            try
            {
                _processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }
}