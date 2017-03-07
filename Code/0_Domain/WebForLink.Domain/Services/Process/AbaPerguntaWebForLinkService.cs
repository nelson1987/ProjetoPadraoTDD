using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Service.Process
{
    public class AbaPerguntaWebForLinkService : Service<QUESTIONARIO_PERGUNTA>, IAbaPerguntaWebForLinkService
    {
        public readonly IAbaPerguntaWebForLinkRepository _readOnlyRepository;

        public AbaPerguntaWebForLinkService(IAbaPerguntaWebForLinkRepository readOnlyRepository)
            : base(readOnlyRepository)
        {
            try
            {
                _readOnlyRepository = readOnlyRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }
}