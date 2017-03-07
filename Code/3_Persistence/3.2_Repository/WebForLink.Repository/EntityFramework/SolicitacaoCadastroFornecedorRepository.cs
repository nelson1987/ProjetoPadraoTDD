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
    public class SolicitacaoCadastroFornecedorWebForLinkRepository :
        EntityFrameworkRepository<SolicitacaoCadastroFornecedor, WebForLinkContexto>,
        ISolicitacaoCadastroFornecedorWebForLinkRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="razaoSocial"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorRazaoSocial(string razaoSocial)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.NOME.Contains(razaoSocial) || x.RAZAO_SOCIAL.Contains(razaoSocial));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpjteste"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorId(string cnpjteste, int statusId)
        {
            try
            {
                return
                    DbSet.FirstOrDefault(
                        x =>
                            x.WFD_SOLICITACAO.SOLICITACAO_STATUS_ID != statusId &&
                            (x.CNPJ == cnpjteste || x.CPF == cnpjteste));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public int BuscarIdSolicitacaoPorCnpj(string cnpj)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.CNPJ == cnpj).SOLICITACAO_ID;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public string BuscarRazaoSocialPorCnpj(string cnpj)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.CNPJ == cnpj).RAZAO_SOCIAL;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public string BuscarRazaoSocialPorcpf(string cpf)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.CPF == cpf).NOME;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorSolicitacaoID(int solicitacaoID)
        {
            try
            {
                return DbSet
                    .Include("WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO")
                    .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacaoID);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao consultar", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorSolicitacaoIDComDocumentos(int solicitacaoID)
        {
            try
            {
                return DbSet
                    .Include("WFD_PJPF_CATEGORIA.ListaDeDocumentosDeFornecedor.DescricaoDeDocumentos.TipoDeDocumento")
                    .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacaoID);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao consultar", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarCadFornPorSolicitacao(int solicitacaoID)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.SOLICITACAO_ID == solicitacaoID);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Ocorreu um erro ao consultar", ex);
            }
        }
    }
}