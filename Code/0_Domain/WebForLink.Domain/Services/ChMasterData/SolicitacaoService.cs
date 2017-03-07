using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class SolicitacaoService : Service<Solicitacao>, ISolicitacaoService
    {
        private readonly ISolicitacaoReadOnlyRepository _readOnlyRepository;
        private readonly ISolicitacaoRepository _repository;

        public SolicitacaoService(ISolicitacaoRepository repository,
            ISolicitacaoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _repository = repository;
        }

        public Solicitacao GetAllReferencesFichaCadastral(int id)
        {
            return _readOnlyRepository.GetAllReferencesFichaCadastral(id);
        }

        public Solicitacao BuscarArquivo(int id)
        {
            return _repository.BuscarArquivo(id);
        }

        public Solicitacao BuscarFichaCompleta(int id)
        {
            return _repository.BuscarFichaCompleta(id);
        }
    }
}