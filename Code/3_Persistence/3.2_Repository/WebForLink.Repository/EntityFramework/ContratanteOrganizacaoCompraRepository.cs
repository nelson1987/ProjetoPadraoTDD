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
    public class ContratanteOrganizacaoCompraWebForLinkRepository :
        EntityFrameworkRepository<CONTRATANTE_ORGANIZACAO_COMPRAS, WebForLinkContexto>,
        IContratanteOrganizacaoCompraWebForLinkRepository
    {
        public List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante)
        {
            try
            {
                return DbSet.Where(c => c.CONTRATANTE_ID == idContratante).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Organização de Compras", ex);
            }
        }
    }
}