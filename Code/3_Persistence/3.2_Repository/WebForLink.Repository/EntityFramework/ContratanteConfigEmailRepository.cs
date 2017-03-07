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
    public class ContratanteConfiguracaoEmailWebForLinkRepository :
        EntityFrameworkRepository<CONTRATANTE_CONFIGURACAO_EMAIL, WebForLinkContexto>,
        IContratanteConfiguracaoEmailWebForLinkRepository
    {
        public CONTRATANTE_CONFIGURACAO_EMAIL BuscarPorContratanteETipo(int contratanteId, int emailTipoId)
        {
            try
            {
                return DbSet.FirstOrDefault(e => e.CONTRATANTE_ID == contratanteId && e.EMAIL_TP_ID == 1);
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao buscar a configuração de email.", e);
            }
        }

        public List<CONTRATANTE_CONFIGURACAO_EMAIL> ListarTodosPorIdContratante(int contratanteId)
        {
            try
            {
                return DbSet.Where(e => e.CONTRATANTE_ID == contratanteId).ToList();
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao buscar a configuração de email.", e);
            }
        }
    }
}