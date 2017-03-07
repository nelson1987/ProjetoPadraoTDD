using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class FornecedorDocumentosVersaoRepository :
        EntityFrameworkRepository<VersionamentoDeDocumentoDoFornecedor, WebForLinkContexto>,
        IFornecedorDocumentosVersaoWebForLinkRepository
    {
    }
}