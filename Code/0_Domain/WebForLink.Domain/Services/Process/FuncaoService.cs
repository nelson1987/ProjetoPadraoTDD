using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFuncaoWebForLinkService : IService<FUNCAO>
    {
        FUNCAO BuscarPorID(int id);
        FUNCAO BuscarPorCodigo(string codigo);

        RetornoPesquisa<FUNCAO> PesquisarFuncao(PesquisaFuncaoFiltrosDTO filtros, int pagina, int tamanhoPagina,
            int contratanteId);

        List<FUNCAO> ListarTodos(int ContratanteId);
        List<FUNCAO> ListarTodosPorPerfil(int idPerfil);
        FUNCAO InserirFuncao(FUNCAO entidade);
        FUNCAO AlterarFuncao(FUNCAO entidade);
    }

    public class FuncaoWebForLinkService : Service<FUNCAO>, IFuncaoWebForLinkService
    {
        private readonly IFuncaoWebForLinkRepository _funcaoRepository;

        public FuncaoWebForLinkService(IFuncaoWebForLinkRepository funcao) : base(funcao)
        {
            try
            {
                _funcaoRepository = funcao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FUNCAO BuscarPorID(int id)
        {
            try
            {
                return _funcaoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma função por ID", ex);
            }
        }

        public FUNCAO BuscarPorCodigo(string codigo)
        {
            try
            {
                return _funcaoRepository.Find(x => x.CODIGO == codigo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma função por ID", ex);
            }
        }

        //public RetornoPesquisa<FUNCAO> PesquisarFuncao(PesquisaFuncaoFiltrosDTO filtros, int pagina, int tamanhoPagina, int contratanteId)
        //{
        //    var predicate = PredicateBuilder.New<FUNCAO>();
        //    predicate = predicate.And(x => x.Contratante.Any(y => y.ID == contratanteId));

        //    if (!string.IsNullOrEmpty(filtros.Codigo))
        //        predicate = predicate.And(x => x.CODIGO.Contains(filtros.Codigo));

        //    if (!string.IsNullOrEmpty(filtros.Nome))
        //        predicate = predicate.And(x => x.FUNCAO_NM.Contains(filtros.Nome));

        //    if (!string.IsNullOrEmpty(filtros.Tela))
        //        predicate = predicate.And(x => x.FUNCAO_TELA.Contains(filtros.Tela));

        //    if (!string.IsNullOrEmpty(filtros.Descricao))
        //        predicate = predicate.And(x => x.FUNCAO_DSC.Contains(filtros.Descricao));

        //    if (filtros.PaiFuncao != 0 && filtros.PaiFuncao != null)
        //        predicate = predicate.And(x => x.FUNCAO_PAI == filtros.PaiFuncao);

        //    if (filtros.Aplicacao != 0)
        //        predicate = predicate.And(x => x.APLICACAO_ID == filtros.Aplicacao);

        //    try
        //    {
        //        return _funcaoRepository.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao listar Funções por aplicações", ex);
        //    }
        //}

        public List<FUNCAO> ListarTodos(int ContratanteId)
        {
            try
            {
                var funcoes =
                    _funcaoRepository.Find(
                        x => x.FUNCAO_PAI == null && x.WFD_CONTRATANTE.Any(y => y.ID == ContratanteId)).ToList();
                return funcoes;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma função por ID", ex);
            }
        }

        public List<FUNCAO> ListarTodosPorPerfil(int idPerfil)
        {
            try
            {
                return _funcaoRepository.Find(x => x.WAC_PERFIL.Any(c => c.ID == idPerfil))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao listar funções por perfil", ex);
            }
        }

        public FUNCAO InserirFuncao(FUNCAO entidade)
        {
            try
            {
                _funcaoRepository.Add(entidade);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma função.", ex);
            }
        }

        public FUNCAO AlterarFuncao(FUNCAO entidade)
        {
            try
            {
                _funcaoRepository.Update(entidade);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma função.", ex);
            }
        }

        public RetornoPesquisa<FUNCAO> PesquisarFuncao(PesquisaFuncaoFiltrosDTO filtros, int pagina, int tamanhoPagina,
            int contratanteId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}