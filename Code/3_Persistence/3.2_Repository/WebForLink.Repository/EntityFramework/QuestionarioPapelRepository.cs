using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IQuestionarioPapelRepository : IRepository<QUESTIONARIO_PAPEL>
    {
    }

    public class QuestionarioPapelRepository : EntityFrameworkRepository<QUESTIONARIO_PAPEL, WebForLinkContexto>,
        IQuestionarioPapelRepository
    {
    }
}