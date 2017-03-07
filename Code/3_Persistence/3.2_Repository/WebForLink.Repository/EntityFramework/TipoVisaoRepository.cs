using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class TipoVisaoWebForLinkRepository : EntityFrameworkRepository<TIPO_VISAO, WebForLinkContexto>,
        ITipoVisaoWebForLinkRepository
    {
        public List<TIPO_VISAO> listarPorContratanteId(int idContratante)
        {
            return DbSet.Where(x => x.VISAO_NM == "").ToList();
        }
    }
}