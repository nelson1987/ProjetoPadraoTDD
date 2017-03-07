using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ITipoDocumentosChWebForLinkService : IService<TIPO_DOCUMENTOS_CH>
    {
    }

    public class ContratantePjpfWebForLinkService : Service<WFD_CONTRATANTE_PJPF>, IContratantePjpfWebForLinkService
    {
        private readonly IContratanteFornecedorWebForLinkRepository _repository;

        public ContratantePjpfWebForLinkService(IContratanteFornecedorWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public WFD_CONTRATANTE_PJPF BuscaFichaCadastralPagante(int contratanteId)
        {
            return _repository.Get(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == 1);
        }

        public void Dispose()
        {
        }
    }

    public class TipoDocumentosChWebForLinkService : Service<TIPO_DOCUMENTOS_CH>, ITipoDocumentosChWebForLinkService
    {
        private readonly ITipoDocumentosChWebForLinkRepository _repository;

        public TipoDocumentosChWebForLinkService(ITipoDocumentosChWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }
}