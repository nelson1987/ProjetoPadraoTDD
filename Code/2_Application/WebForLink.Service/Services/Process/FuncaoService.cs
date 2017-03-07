using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class FuncaoWebForLinkAppService : AppService<WebForLinkContexto>, IFuncaoWebForLinkAppService
    {
        private readonly IFuncaoWebForLinkService _funcaoService;

        public FuncaoWebForLinkAppService(IFuncaoWebForLinkService funcao)
        {
            try
            {
                _funcaoService = funcao;
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
                return _funcaoService.Get(id);
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
                return _funcaoService.Get(x => x.CODIGO == codigo);
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
        //        return _funcaoService.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
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
                    _funcaoService.Find(x => x.FUNCAO_PAI == null && x.WFD_CONTRATANTE.Any(y => y.ID == ContratanteId))
                        .ToList();
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
                return _funcaoService.Find(x => x.WAC_PERFIL.Any(c => c.ID == idPerfil))
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
                _funcaoService.Add(entidade);

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
                _funcaoService.Update(entidade);

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

        public FUNCAO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FUNCAO Get(Expression<Func<FUNCAO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FUNCAO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FUNCAO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FUNCAO> Find(Expression<Func<FUNCAO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(FUNCAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(List<FUNCAO> entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FUNCAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(FUNCAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(List<FUNCAO> entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        List<ValidationResult> IService<FUNCAO>.Add(List<FUNCAO> entity)
        {
            throw new NotImplementedException();
        }

        List<ValidationResult> IService<FUNCAO>.Delete(List<FUNCAO> entity)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<FUNCAO> BuscarPesquisa(Expression<Func<FUNCAO, bool>> filtros, int tamanhoPagina, int pagina, Func<FUNCAO, IComparable> ordenacao)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Modificar(FUNCAO entity)
        {
            throw new NotImplementedException();
        }
    }
}