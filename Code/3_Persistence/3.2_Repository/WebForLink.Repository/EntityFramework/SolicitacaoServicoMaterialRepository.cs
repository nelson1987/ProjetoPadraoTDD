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
    public class SolicitacaoServicoMaterialWebForLinkRepository :
        EntityFrameworkRepository<SOLICITACAO_UNSPSC, WebForLinkContexto>,
        ISolicitacaoServicoMaterialWebForLinkRepository
    {
        public List<SOLICITACAO_UNSPSC> BuscarPorSolicitacaoId(int solicitacaoId)
        {
            try
            {
                return Find(x => x.SOLICITACAO_ID == solicitacaoId).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(string.Format("ErroBuscar", "NomeEntidade"), ex);
            }
        }
    }
}