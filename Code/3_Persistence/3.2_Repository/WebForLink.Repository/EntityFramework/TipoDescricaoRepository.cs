using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class TipoDescricaoWebForLinkRepository : EntityFrameworkRepository<TIPO_DESCRICAO, WebForLinkContexto>,
        ITipoDescricaoWebForLinkRepository
    {
        public List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId)
        {
            try
            {
                return DbSet.Where(g => g.GRUPO_ID == grupoId).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma descrição por grupoId", ex);
            }
        }
    }
}