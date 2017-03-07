using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class PapelWebForLinkRepository : EntityFrameworkRepository<Papel, WebForLinkContexto>,
        IPapelWebForLinkRepository
    {
        public List<Papel> ListarPorContratanteId(int contratanteId)
        {
            return DbSet.Where(x => x.CONTRATANTE_ID == contratanteId).ToList();
        }

        public Papel BuscarPorContratanteIdETipoPapelId(int contratanteId, int tipoPapelId)
        {
            return DbSet.FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId && x.PAPEL_TP_ID == tipoPapelId);
        }
    }
}