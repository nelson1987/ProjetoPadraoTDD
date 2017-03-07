using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class SolicitadoService : Service<Solicitado>, ISolicitadoService
    {
        private readonly ISolicitadoReadOnlyRepository _readOnlyRepository;
        private readonly ISolicitadoRepository _repository;

        public SolicitadoService(ISolicitadoRepository repository,
            ISolicitadoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }
}