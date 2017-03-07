using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class ResponsavelService : Service<Responsavel>, IResponsavelService
    {
        private readonly IResponsavelReadOnlyRepository _readOnlyRepository;
        private readonly IResponsavelRepository _repository;

        public ResponsavelService(IResponsavelRepository repository,
            IResponsavelReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }
}