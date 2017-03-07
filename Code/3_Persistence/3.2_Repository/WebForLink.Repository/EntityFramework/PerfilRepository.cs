using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class PerfilWebForLinkRepository : EntityFrameworkRepository<Perfil, WebForLinkContexto>,
        IPerfilWebForLinkRepository
    {
        public List<Perfil> ListarPorContratanteId(int contratanteId)
        {
            return DbSet.Where(x => x.CONTRATANTE_ID == contratanteId).ToList();
        }
    }
}