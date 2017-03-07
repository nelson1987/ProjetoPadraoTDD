using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class EnderecoService : Service<Endereco>, IEnderecoService
    {
        private readonly IEnderecoReadOnlyRepository _readOnlyRepository;
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository,
            IEnderecoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }

        public Endereco UpdateReadOnly(Endereco entity)
        {
            return _readOnlyRepository.Update(entity);
        }

        public Endereco InsertReadOnly(Endereco entity)
        {
            return _readOnlyRepository.Insert(entity);
        }

        public void ExcluirEndereco(int id)
        {
            _repository.Delete(_repository.Get(id));
        }
    }
}