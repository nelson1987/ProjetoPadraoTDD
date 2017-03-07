using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class FornecedorCategoriaRepository : EntityFrameworkRepository<FORNECEDOR_CATEGORIA, WebForLinkContexto>,
        IFornecedorCategoriaWebForLinkRepository
    {
        public List<FORNECEDOR_CATEGORIA> ListarPorContratanteId(int id)
        {
            return DbSet.Where(x => x.CONTRATANTE_ID == id).ToList();
        }

        public FORNECEDOR_CATEGORIA BuscarComPjPfListaDocumento(int id)
        {
            return DbSet.Include(x => x.ListaDeDocumentosDeFornecedor).FirstOrDefault(c => c.ID == id);
        }
    }
}