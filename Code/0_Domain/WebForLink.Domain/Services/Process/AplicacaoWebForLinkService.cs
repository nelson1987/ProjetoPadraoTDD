using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class AplicacaoWebForLinkService : Service<APLICACAO>, IAplicacaoWebForLinkService
    {
        private readonly IAplicacaoWebForLinkRepository _aplicacaoRepository;

        public AplicacaoWebForLinkService(IAplicacaoWebForLinkRepository aplicacaoRepository)
            : base(aplicacaoRepository)
        {
            try
            {
                _aplicacaoRepository = aplicacaoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APLICACAO BuscarPorID(int id)
        {
            try
            {
                return _aplicacaoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nomeAplicacao"></param>
        /// <returns></returns>
        public APLICACAO BuscarPorIdNomeAplicacao(int id, string nomeAplicacao)
        {
            try
            {
                return
                    _aplicacaoRepository.Find(x => x.APLICACAO_NM.Contains(nomeAplicacao) && x.ID != id)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma aplicação por nome", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="nomeAplicacao"></param>
        /// <returns></returns>
        public APLICACAO BuscarPorNome(string nomeAplicacao)
        {
            try
            {
                return _aplicacaoRepository.Find(x => x.APLICACAO_NM.Contains(nomeAplicacao)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma aplicação por nome", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public APLICACAO AlterarAplicacao(APLICACAO entidade)
        {
            try
            {
                _aplicacaoRepository.Update(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma aplicação", ex);
            }
        }

        public void ExcluirAplicacao(int id)
        {
            try
            {
                _aplicacaoRepository.Delete(_aplicacaoRepository.Get(id));
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma aplicação", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public APLICACAO InserirAplicacao(APLICACAO entidade)
        {
            try
            {
                _aplicacaoRepository.Add(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma aplicação", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<APLICACAO> ListarTodos()
        {
            try
            {
                return _aplicacaoRepository.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao listar aplicações", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns></returns>
        public RetornoPesquisa<APLICACAO> PesquisarAplicacao(PesquisaAplicacaoFiltrosDTO filtros, int pagina,
            int tamanhoPagina)
        {
            try
            {
                var Aplicacoes = _aplicacaoRepository.All().ToList();
                var totalRegistro = Aplicacoes.Count();
                var registroExibicao = Aplicacoes
                    .Skip(tamanhoPagina*(pagina - 1))
                    .Take(tamanhoPagina)
                    .ToList();

                return new RetornoPesquisa<APLICACAO>
                {
                    RegistrosPagina = registroExibicao,
                    TotalRegistros = totalRegistro,
                    TotalPaginas = (int) Math.Ceiling(totalRegistro/(double) tamanhoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Aplicacoes", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}