using System;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class EstadoWebForLinkRepository : EntityFrameworkRepository<TiposDeEstado, WebForLinkContexto>,
        IEstadoWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns></returns>
        public TiposDeEstado BuscarPorID(string sigla)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.UF_SGL == sigla);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}