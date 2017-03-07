using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IQuestionarioAbaRepository : IRepository<QUESTIONARIO_ABA>
    {
    }

    public class QuestionarioAbaRepository : EntityFrameworkRepository<QUESTIONARIO_ABA, WebForLinkContexto>,
        IQuestionarioAbaRepository
    {
    }
}