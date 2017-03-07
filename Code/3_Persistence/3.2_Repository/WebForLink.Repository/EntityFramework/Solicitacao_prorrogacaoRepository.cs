using System;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class SolicitacaoProrrogacaoWebForLinkRepository :
        EntityFrameworkRepository<SOLICITACAO_PRORROGACAO, WebForLinkContexto>,
        ISolicitacao_prorrogacaoWebForLinkRepository
    {
        public SOLICITACAO_PRORROGACAO BuscarPorIdIncluindoSolicitacao(int id)
        {
            try
            {
                return DbSet.Include("WFD_SOLICITACAO").FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma solicitação de prorrogação por Id", ex);
            }
        }
    }
}