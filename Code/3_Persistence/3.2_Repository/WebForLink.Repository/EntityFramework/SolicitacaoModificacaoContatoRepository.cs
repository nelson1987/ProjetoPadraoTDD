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
    public class SolicitacaoModificacaoContatoWebForLinkRepository :
        EntityFrameworkRepository<SolicitacaoModificacaoDadosContato, WebForLinkContexto>,
        ISolicitacaoModificacaoContatoWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public List<SolicitacaoModificacaoDadosContato> ListarPorSolicitacaoId(int solicitacaoID)
        {
            try
            {
                return DbSet.Where(x => x.SOLICITACAO_ID == solicitacaoID).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(
                    "Ocorreu um erro ao buscar a lista de solicitações de modificação de contato por solicitação.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void InserirOuAtualizar(SolicitacaoModificacaoDadosContato solicitacao)
        {
            try
            {
                var entidade = DbSet.FirstOrDefault(x => x.ID == solicitacao.ID);

                if (entidade == null)
                    Add(solicitacao);
                else
                    Update(solicitacao);
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException(
                    "Ocorreu um erro ao inserir ou atualizar a solicitação de modificação de contato.", e);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void Excluir(SolicitacaoModificacaoDadosContato solicitacao)
        {
            try
            {
                Delete(solicitacao);
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException(
                    "Ocorreu um erro ao inserir a solicitação de modificação de contato.", e);
            }
        }

        public void ManterContatoCadastroFornecedor(List<SolicitacaoModificacaoDadosContato> contatos, int SolicitacaoId)
        {
            try
            {
                //DbSet.RemoveRange(DbSet.Where(x => x.SOLICITACAO_ID == SolicitacaoId));
                contatos.ForEach(x => Add(x));
            }
            catch (Exception e)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao tentar salvar os contatos na solicitação.",
                    e);
            }
        }
    }
}