using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.ChMasterData
{
    public class ContatoService : Service<Contato>, IContatoService
    {
        private readonly IContatoReadOnlyRepository _readOnlyRepository;
        private readonly IContatoRepository _repository;

        public ContatoService(IContatoRepository repository,
            IContatoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }

        public ContatoService(IContatoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Contato UpdateReadOnly(Contato entity)
        {
            return _readOnlyRepository.Update(entity);
        }

        public Contato InsertReadOnly(Contato entity)
        {
            return _readOnlyRepository.Insert(entity);
        }
    }
}