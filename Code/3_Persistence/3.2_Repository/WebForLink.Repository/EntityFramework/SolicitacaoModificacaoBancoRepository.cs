using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface ISolicitacaoBancoRepository : IRepository<SolicitacaoModificacaoDadosBancario>
    {
        List<SolicitacaoModificacaoDadosBancario> BuscarPorSolicitacaoId(int id);
    }

    public class SolicitacaoBancoRepository :
        EntityFrameworkRepository<SolicitacaoModificacaoDadosBancario, WebForLinkContexto>,
        ISolicitacaoBancoRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SolicitacaoModificacaoDadosBancario> BuscarPorSolicitacaoId(int id)
        {
            try
            {
                return DbSet.Include("T_BANCO").Where(x => x.SOLICITACAO_ID == id).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }
    }
}