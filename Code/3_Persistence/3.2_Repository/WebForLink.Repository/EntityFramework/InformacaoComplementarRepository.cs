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
    //public interface IInformacaoComplementarWebForLinkRepository : IRepository<WFD_INFORM_COMPL>
    //{
    //    WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta);
    //    WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta, int idSolicitacao);
    //    WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoId(int idPergunta, int idSolicitacao);
    //    WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoIdResposta(int idPergunta, int idSolicitacao, string resposta);
    //    WFD_INFORM_COMPL ValidaExistente(WFD_INFORM_COMPL entidade);
    //    FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdFornecedorId(int idPergunta, int idPJPF);
    //    List<WFD_INFORM_COMPL> UpdateAll(List<WFD_INFORM_COMPL> entidade);
    //    List<WFD_INFORM_COMPL> InserirTodos(List<WFD_INFORM_COMPL> entidade);
    //    bool ValidaDuplicado(WFD_INFORM_COMPL entidade);
    //}

    public class FornecedorInformacaoComplementarComplWebForLinkRepository :
        EntityFrameworkRepository<FORNECEDOR_INFORM_COMPL, WebForLinkContexto>,
        IFornecedorInformacaoComplementarComplWebForLinkRepository
    {
    }

    public class InformacaoComplementarWebForLinkRepository :
        EntityFrameworkRepository<WFD_INFORM_COMPL, WebForLinkContexto>,
        IInformacaoComplementarWebForLinkRepository
    {
        private readonly IFornecedorInformacaoComplementarComplRepository _fornecedorInformacaoComplementares;

        public InformacaoComplementarWebForLinkRepository(
            IFornecedorInformacaoComplementarComplRepository fornecedorInformacaoComplementar)
        {
            _fornecedorInformacaoComplementares = fornecedorInformacaoComplementar;
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoId(int idPergunta, int idSolicitacao)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.PERG_ID == idPergunta && x.SOLICITACAO_ID == idSolicitacao);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdFornecedorId(int idPergunta, int idPjpf)
        {
            try
            {
                return _fornecedorInformacaoComplementares.BuscarPorPerguntaIdFornecedorId(idPergunta, idPjpf);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.PERG_ID == idPergunta);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta, int idSolicitacao)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.PERG_ID == idPergunta && x.SOLICITACAO_ID == idSolicitacao);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <param name="idSolicitacao"></param>
        /// <param name="resposta"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoIdResposta(int idPergunta, int idSolicitacao,
            string resposta)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.PERG_ID == idPergunta
                                                 && x.SOLICITACAO_ID == idSolicitacao
                                                 && x.RESPOSTA == resposta);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<WFD_INFORM_COMPL> UpdateAll(List<WFD_INFORM_COMPL> entidade)
        {
            try
            {
                foreach (var item in entidade)
                {
                    var novo = Get(item.ID);
                    if (!item.Equals(novo))
                    {
                        Update(item);
                    }
                }
                return entidade;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao inserir uma Pergunta", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<WFD_INFORM_COMPL> InserirTodos(List<WFD_INFORM_COMPL> entidade)
        {
            try
            {
                foreach (var item in entidade)
                {
                    if (item.ID != 0)
                    {
                        var novo = Get(item.ID);
                        if (!item.Equals(novo))
                        {
                            Add(item);
                        }
                    }
                    else
                        Add(item);
                }
                return entidade;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao inserir uma Pergunta", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL ValidaExistente(WFD_INFORM_COMPL entidade)
        {
            return BuscarPorPerguntaIdSolicitacaoId(entidade.PERG_ID, entidade.SOLICITACAO_ID);
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public bool ValidaDuplicado(WFD_INFORM_COMPL entidade)
        {
            if (
                BuscarPorPerguntaIdSolicitacaoIdResposta(entidade.PERG_ID, entidade.SOLICITACAO_ID, entidade.RESPOSTA) !=
                null)
                return true;
            return false;
        }
    }
}