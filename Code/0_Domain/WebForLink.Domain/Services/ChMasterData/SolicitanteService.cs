using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class SolicitanteService : Service<Solicitante>, ISolicitanteService
    {
        private readonly ISolicitanteReadOnlyRepository _readOnlyRepository;
        private readonly ISolicitanteRepository _repository;

        public SolicitanteService(ISolicitanteRepository repository,
            ISolicitanteReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }
}