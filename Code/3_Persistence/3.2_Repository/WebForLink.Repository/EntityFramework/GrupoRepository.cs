using System;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class GrupoWebForLinkRepository : EntityFrameworkRepository<GRUPO, WebForLinkContexto>,
        IGrupoWebForLinkRepository
    {
        public int QuantidadeEmpresa(int contratanteId)
        {
            try
            {
                return DbSet.Count(x => x.WFD_CONTRATANTE.Any(y => y.ID == contratanteId));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}