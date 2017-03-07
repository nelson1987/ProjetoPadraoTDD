using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorVersaoDocumentosWebForLinkService : IService<VersionamentoDeDocumentoDoFornecedor>
    {
        VersionamentoDeDocumentoDoFornecedor Inserir(VersionamentoDeDocumentoDoFornecedor entidade);
    }

    public class FornecedorVersaoDocumentosWebForLinkService : Service<VersionamentoDeDocumentoDoFornecedor>,
        IFornecedorVersaoDocumentosWebForLinkService
    {
        private readonly IFornecedorDocumentosVersaoWebForLinkRepository _fornecedorVersionamentoDocumentoRepository;

        public FornecedorVersaoDocumentosWebForLinkService(
            IFornecedorDocumentosVersaoWebForLinkRepository fornecedorVersionamentoDocumento)
            : base(fornecedorVersionamentoDocumento)
        {
            try
            {
                _fornecedorVersionamentoDocumentoRepository = fornecedorVersionamentoDocumento;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public VersionamentoDeDocumentoDoFornecedor Inserir(VersionamentoDeDocumentoDoFornecedor entidade)
        {
            try
            {
                _fornecedorVersionamentoDocumentoRepository.Add(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o PJPF Base por ID", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}