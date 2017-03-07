using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IFornecedorUnspscRepository : IRepository<FORNECEDOR_UNSPSC>
    {
        List<FORNECEDOR_UNSPSC> buscarPorPjpFId(int idPjpf);
    }

    public class FornecedorServicoMaterialRepository : EntityFrameworkRepository<FORNECEDOR_UNSPSC, WebForLinkContexto>,
        IFornecedorUnspscRepository
    {
        public List<FORNECEDOR_UNSPSC> buscarPorPjpFId(int idPjpf)
        {
            return DbSet.Where(x => x.PJPF_ID == idPjpf).ToList();
        }
    }
}