using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class AplicacaoWebForLinkAppService : AppService<WebForLinkContexto>, IAplicacaoWebForLinkAppService
    {
        private readonly IAplicacaoWebForLinkService _aplicacaoService;

        public AplicacaoWebForLinkAppService(IAplicacaoWebForLinkService aplicacaoService)
        {
            try
            {
                _aplicacaoService = aplicacaoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Commit();
        }

        //public RetornoPesquisa<APLICACAO> PesquisarAplicacao(PesquisaAplicacaoFiltrosDTO filtros, int pagina, int tamanhoPagina)
        //{
        //    try
        //    {
        //        var predicate = PredicateBuilder.New<APLICACAO>();
        //        if (!string.IsNullOrEmpty(filtros.Nome))
        //            predicate = predicate.And(c => c.APLICACAO_NM.Contains(filtros.Nome));

        //        if (!string.IsNullOrEmpty(filtros.Descricao))
        //            predicate = predicate.And(c => c.APLICACAO_DSC.Contains(filtros.Descricao));

        //        List<APLICACAO> Aplicacoes = _aplicacaoService.All().OrderBy(predicate).ToList();
        //        var totalRegistro = Aplicacoes.Count();
        //        var registroExibicao = Aplicacoes
        //                                .Skip(tamanhoPagina * (pagina - 1))
        //                                .Take(tamanhoPagina)
        //                                .ToList();

        //        return new RetornoPesquisa<APLICACAO>()
        //        {
        //            RegistrosPagina = registroExibicao,
        //            TotalRegistros = totalRegistro,
        //            TotalPaginas = (int)Math.Ceiling(totalRegistro / (double)tamanhoPagina)
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Aplicacoes", ex);
        //    }
        //}
        public APLICACAO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public APLICACAO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public APLICACAO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<APLICACAO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<APLICACAO> Find(Expression<Func<APLICACAO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(APLICACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(APLICACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(APLICACAO entity)
        {
            throw new NotImplementedException();
        }

        public APLICACAO Get(int id)
        {
            throw new NotImplementedException();
        }

        public APLICACAO Get(Expression<Func<APLICACAO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<APLICACAO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<APLICACAO> Find(Expression<Func<APLICACAO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APLICACAO BuscarPorId(int id)
        {
            try
            {
                return _aplicacaoService.Get(id);
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
                return _aplicacaoService.Get(x => x.APLICACAO_NM.Contains(nomeAplicacao) && x.ID != id);
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
                return _aplicacaoService.Get(x => x.APLICACAO_NM.Contains(nomeAplicacao));
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
                _aplicacaoService.Update(entidade);
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
                _aplicacaoService.ExcluirAplicacao(id);
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
                _aplicacaoService.Add(entidade);
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
                return _aplicacaoService.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao listar aplicações", ex);
            }
        }

        void IAplicacaoWebForLinkAppService.AlterarAplicacao(APLICACAO aplicacao)
        {
            throw new NotImplementedException();
        }

        object IAplicacaoWebForLinkAppService.BuscarPorIdNomeAplicacao(int id, string nome)
        {
            throw new NotImplementedException();
        }

        object IAplicacaoWebForLinkAppService.BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        void IAplicacaoWebForLinkAppService.InserirAplicacao(APLICACAO aplicacao)
        {
            throw new NotImplementedException();
        }

        public object PesquisarAplicacao(PesquisaAplicacaoFiltrosDTO filtros, int pagina, int v)
        {
            throw new NotImplementedException();
        }

        IEnumerable IAplicacaoWebForLinkAppService.ListarTodos()
        {
            throw new NotImplementedException();
        }

        RetornoPesquisa<APLICACAO> IAplicacaoWebForLinkAppService.PesquisarAplicacao(PesquisaAplicacaoFiltrosDTO filtros, int pagina, int v)
        {
            throw new NotImplementedException();
        }
    }
}