using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class FluxoSequenciaWebForLinkRepository : EntityFrameworkRepository<FLUXO_SEQUENCIA, WebForLinkContexto>,
        IFluxoSequenciaWebForLinkRepository
    {
        public List<FLUXO_SEQUENCIA> ListarPorContratanteId(int contratanteId)
        {
            try
            {
                return DbSet.Where(x => x.CONTRATANTE_ID == contratanteId).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }

        public List<FLUXO_SEQUENCIA> ListarPorContratanteIdEFluxoTipoId(int contratanteId, int fluxoId)
        {
            try
            {
                return DbSet.Include(x => x.WFL_FLUXO)
                    .Where(x => x.WFL_FLUXO.CONTRATANTE_ID == contratanteId && x.WFL_FLUXO.FLUXO_TP_ID == fluxoId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }
    }
}