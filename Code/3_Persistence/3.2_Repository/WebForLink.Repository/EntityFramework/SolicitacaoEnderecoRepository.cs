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
    public class SolicitacaoEnderecoWebForLinkRepository :
        EntityFrameworkRepository<SOLICITACAO_MODIFICACAO_ENDERECO, WebForLinkContexto>,
        ISolicitacaoEnderecoWebForLinkRepository
    {
        public List<SOLICITACAO_MODIFICACAO_ENDERECO> ListarPorSolicitacaoId(int solicitacaoID)
        {
            try
            {
                return
                    DbSet.Include("WFD_T_TP_ENDERECO")
                        .Include("WFD_SOLICITACAO")
                        .Where(x => x.SOLICITACAO_ID == solicitacaoID)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(
                    "Ocorreu um erro ao buscar a lista de solicitações de modificação de dados de endereço por solicitação.",
                    ex);
            }
        }

        public void InserirOuAtualizar(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao)
        {
            try
            {
                var entidade = Get(solicitacao.ID);

                if (Get(solicitacao.ID) == null)
                    Add(solicitacao);
                else
                    Update(entidade);
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException(
                    "Ocorreu um erro ao inserir ou atualizar a solicitação de modificação de endereço.", e);
            }
        }
    }
}