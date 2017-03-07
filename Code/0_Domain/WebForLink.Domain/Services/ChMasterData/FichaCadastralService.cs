using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class FichaCadastralService : Service<FichaCadastral>, IFichaCadastralService
    {
        private readonly IFichaCadastralReadOnlyRepository _readOnlyRepository;
        private readonly IFichaCadastralRepository _repository;

        public FichaCadastralService(IFichaCadastralRepository repository,
            IFichaCadastralReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _repository = repository;
        }

        public FichaCadastral Incluir(FichaCadastral ficha)
        {
            _repository.Add(ficha);
            return ficha;
        }

        public void Incluir(FichaCadastral ficha, int idSolicitacao)
        {
            _readOnlyRepository.Incluir(ficha, idSolicitacao);
        }

        public void IncluirArquivo(int idSolicitacao, int idDocumentoSolicitado, string nomeOriginal, int size,
            string url)
        {
            _repository.IncluirArquivo(idSolicitacao, idDocumentoSolicitado, nomeOriginal, size, url);
        }

        public void IncluirFichaCadastral(FichaCadastral ficha)
        {
            _readOnlyRepository.IncluirFichaCadastral(ficha);
        }
    }
}