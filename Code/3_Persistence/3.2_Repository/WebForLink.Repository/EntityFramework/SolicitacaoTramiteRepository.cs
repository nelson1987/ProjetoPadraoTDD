using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class SolicitacaoTramiteWebForLinkRepository :
        EntityFrameworkRepository<SOLICITACAO_TRAMITE, WebForLinkContexto>,
        ISolicitacaoTramiteWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <param name="papelId"></param>
        /// <returns></returns>
        public SOLICITACAO_TRAMITE BuscarTramitePorSolicitacaoIdPapelId(int solicitacaoId, int papelId)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.SOLICITACAO_ID == solicitacaoId && x.PAPEL_ID == papelId);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tramiteInclusao"></param>
        /// <returns></returns>
        public SOLICITACAO_TRAMITE IncluirTramiteStatusUm(SOLICITACAO_TRAMITE tramiteInclusao)
        {
            try
            {
                Add(tramiteInclusao);
                return tramiteInclusao;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tramiteInclusao"></param>
        /// <returns></returns>
        public SOLICITACAO_TRAMITE IncluirTramiteStatusDoisComTramiteAtual(SOLICITACAO_TRAMITE tramiteInclusao)
        {
            try
            {
                Add(tramiteInclusao);
                return tramiteInclusao;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tramiteInclusao"></param>
        /// <returns></returns>
        public SOLICITACAO_TRAMITE IncluirTramiteStatusDoisSemTramiteAtual(SOLICITACAO_TRAMITE tramiteInclusao)
        {
            try
            {
                Add(tramiteInclusao);
                return tramiteInclusao;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tramiteInclusao"></param>
        /// <returns></returns>
        public SOLICITACAO_TRAMITE ReprovarTramite(SOLICITACAO_TRAMITE tramiteInclusao)
        {
            try
            {
                Add(tramiteInclusao);
                return tramiteInclusao;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <returns></returns>
        public List<SOLICITACAO_TRAMITE> BuscarTramiteAtual(int solicitacao)
        {
            try
            {
                return DbSet
                    .Where(t => t.SOLICITACAO_ID == solicitacao
                                && t.SOLICITACAO_STATUS_ID == 1).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de Solicitacao Tramite", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <returns></returns>
        public List<SOLICITACAO_TRAMITE> BuscarTramiteAtualcomPapel(int solicitacao)
        {
            try
            {
                return DbSet
                    .Include(x => x.Papel)
                    .Where(t => t.SOLICITACAO_ID == solicitacao
                                && t.SOLICITACAO_STATUS_ID == 1).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de Solicitacao Tramite", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tramite"></param>
        /// <returns></returns>
        public SOLICITACAO_TRAMITE incluirTramite(SOLICITACAO_TRAMITE tramite)
        {
            try
            {
                Add(tramite);
                return tramite;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <returns></returns>
        public bool SolicitacaoAprovadaPorUmAprovador(int solicitacao)
        {
            try
            {
                return DbSet
                    .Include(x => x.Papel)
                    .Any(t => t.SOLICITACAO_ID == solicitacao
                              && t.SOLICITACAO_STATUS_ID == 2
                              && t.Papel.PAPEL_TP_ID == null);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de Solicitacao Tramite", ex);
            }
        }

        public bool SolicitacaoFornecedorFinalizou(int solicitacao)
        {
            try
            {
                return DbSet
                    .Include(x => x.Papel)
                    .Any(t => t.SOLICITACAO_ID == solicitacao
                              && t.SOLICITACAO_STATUS_ID == (int) EnumStatusTramite.Aprovado
                              && t.Papel.PAPEL_TP_ID == (int) EnumTiposPapel.Fornecedor);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de Solicitacao Tramite", ex);
            }
        }
    }
}