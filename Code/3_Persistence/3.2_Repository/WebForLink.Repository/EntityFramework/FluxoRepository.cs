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
    public class FluxoWebForLinkRepository : EntityFrameworkRepository<Fluxo, WebForLinkContexto>,
        IFluxoWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="tipoFluxoId"></param>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public Fluxo BuscarPorTipoEContratante(int tipoFluxoId, int contratanteId)
        {
            try
            {
                return
                    DbSet.FirstOrDefault(
                        x => x.FLUXO_TP_ID.Equals(tipoFluxoId) && x.CONTRATANTE_ID.Equals(contratanteId));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }

        public List<Fluxo> BuscarPorContratanteId(int contratanteId)
        {
            try
            {
                return DbSet.Where(x => x.CONTRATANTE_ID == contratanteId).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao Listar um Fluxo", ex);
            }
        }
    }
}