using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class BancoService : Service<Banco>, IBancoService
    {
        private readonly IBancoReadOnlyRepository _readOnlyRepository;
        private readonly IBancoRepository _repository;

        public BancoService(IBancoRepository repository,
            IBancoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }

        public Banco UpdateReadOnly(Banco entity)
        {
            return _readOnlyRepository.Update(entity);
        }

        public Banco InsertReadOnly(Banco entity)
        {
            return _readOnlyRepository.Insert(entity);
        }
    }

    public class ArquivoService : Service<Arquivo>, IArquivoService
    {
        private readonly IArquivoReadOnlyRepository _readOnlyRepository;
        private readonly IArquivoRepository _repository;

        public ArquivoService(IArquivoRepository repository,
            IArquivoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }

    public class CarrinhoService : Service<Carrinho>, ICarrinhoService
    {
        private readonly ICarrinhoReadOnlyRepository _readOnlyRepository;
        private readonly ICarrinhoRepository _repository;

        public CarrinhoService(ICarrinhoRepository repository,
            ICarrinhoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }

    public class DocumentoAnexadoService : Service<DocumentoAnexado>, IDocumentoAnexadoService
    {
        private readonly IDocumentoAnexadoReadOnlyRepository _readOnlyRepository;
        private readonly IDocumentoAnexadoRepository _repository;

        public DocumentoAnexadoService(IDocumentoAnexadoRepository repository,
            IDocumentoAnexadoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }

    public class DocumentosSolicitacaoService : Service<DocumentoSolicitacao>, IDocumentosSolicitacaoService
    {
        private readonly IDocumentosSolicitacaoReadOnlyRepository _readOnlyRepository;
        private readonly IDocumentosSolicitacaoRepository _repository;

        public DocumentosSolicitacaoService(IDocumentosSolicitacaoRepository repository,
            IDocumentosSolicitacaoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }

    public class ListasDocumentoService : Service<ListaDocumento>, IListasDocumentoService
    {
        private readonly IListaDocumentoReadOnlyRepository _readOnlyRepository;
        private readonly IListasDocumentoRepository _repository;

        public ListasDocumentoService(IListasDocumentoRepository repository,
            IListaDocumentoReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }
    }
}