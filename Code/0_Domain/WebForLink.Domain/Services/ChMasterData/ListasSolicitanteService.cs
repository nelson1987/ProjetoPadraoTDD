using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class ListasSolicitanteService : Service<ListasSolicitante>, IListasSolicitanteService
    {
        private readonly IListasSolicitanteReadOnlyRepository _readOnlyRepository;
        private readonly IListasSolicitanteRepository _repository;

        public ListasSolicitanteService(IListasSolicitanteRepository repository,
            IListasSolicitanteReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }
}