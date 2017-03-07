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
    public class TipoGrupoWebForLinkRepository : EntityFrameworkRepository<TIPO_GRUPO, WebForLinkContexto>,
        ITipoGrupoWebForLinkRepository
    {
        public List<TIPO_GRUPO> ListarGruposPorVisao(int visaoId)
        {
            try
            {
                return DbSet.Where(g => g.VISAO_ID == visaoId).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}