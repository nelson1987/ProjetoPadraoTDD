using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorVersaoDocumentosWebForLinkAppService
    {
        VersionamentoDeDocumentoDoFornecedor Inserir(VersionamentoDeDocumentoDoFornecedor entidade);
    }

    public class FornecedorVersaoDocumentosWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorVersaoDocumentosWebForLinkAppService
    {
        private readonly IFornecedorDocumentosVersaoWebForLinkService _fornecedorVersionamentoDocumentoService;

        public FornecedorVersaoDocumentosWebForLinkAppService(
            IFornecedorDocumentosVersaoWebForLinkService fornecedorVersionamentoDocumento)
        {
            try
            {
                _fornecedorVersionamentoDocumentoService = fornecedorVersionamentoDocumento;
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
                BeginTransaction();
                _fornecedorVersionamentoDocumentoService.Add(entidade);
                Commit();
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