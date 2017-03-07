using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IDocumentosPjPFRepository : IRepository<DocumentosDoFornecedor>
    {
    }

    public class DocumentosPjPFRepository : EntityFrameworkRepository<DocumentosDoFornecedor, WebForLinkContexto>,
        IDocumentosPjPFRepository
    {
    }
}