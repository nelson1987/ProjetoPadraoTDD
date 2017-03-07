using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class DocumentoAnexadoRepository : EntityFrameworkRepository<DocumentoAnexado, ChMasterDataContext>,
        IDocumentoAnexadoRepository
    {
    }
}