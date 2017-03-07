using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IQuestionarioDinamicoRepository : IRepository<QuestionarioDinamicoDTO>
    {
    }

    public class QuestionarioDinamicoRepository : EntityFrameworkRepository<QuestionarioDinamicoDTO, WebForLinkContexto>,
        IQuestionarioDinamicoRepository
    {
    }
}