using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class FornecedorCategoriaWebForLinkAppService : AppService<WebForLinkContexto>, IFornecedorCategoriaWebForLinkAppService
    {
        private readonly IFornecedorCategoriaWebForLinkService _statusSolicitacaoService;

        public FornecedorCategoriaWebForLinkAppService(IFornecedorCategoriaWebForLinkService statusSolicitacaoService)
        {
            try
            {
                _statusSolicitacaoService = statusSolicitacaoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public IEnumerable<FORNECEDOR_CATEGORIA> All()
        {
            return _statusSolicitacaoService.All();
        }

        public IEnumerable<FORNECEDOR_CATEGORIA> All(bool @readonly = false)
        {
            return _statusSolicitacaoService.All(@readonly);
        }

        public void AlterarCategoria(FORNECEDOR_CATEGORIA categoriaInserir)
        {
            _statusSolicitacaoService.AlterarCategoria(categoriaInserir);
        }

        public FORNECEDOR_CATEGORIA Buscar(Expression<Func<FORNECEDOR_CATEGORIA, bool>> p)
        {
            return _statusSolicitacaoService.Buscar(p);
        }

        public List<FORNECEDOR_CATEGORIA> BuscarCategorias(int contratanteId)
        {
            return _statusSolicitacaoService.BuscarCategorias(contratanteId);
        }

        public FORNECEDOR_CATEGORIA BuscarEmSolicitacaoFornecedor(int contratanteId, int categoria)
        {
            return _statusSolicitacaoService.BuscarEmSolicitacaoFornecedor(contratanteId, categoria);
        }

        public List<FORNECEDOR_CATEGORIA> BuscarPorCategoriaPai(int idPai, int contratanteId)
        {
            return _statusSolicitacaoService.BuscarPorCategoriaPai(idPai, contratanteId);
        }

        public FORNECEDOR_CATEGORIA BuscarPorId(int categoriaId)
        {
            return _statusSolicitacaoService.BuscarPorId(categoriaId);
        }

        public FORNECEDOR_CATEGORIA BuscarPorId(int id, int contratanteId)
        {
            return _statusSolicitacaoService.BuscarPorId(id, contratanteId);
        }

        public ValidationResult Create(FORNECEDOR_CATEGORIA entity)
        {
            return _statusSolicitacaoService.Add(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ExcluirCategoriaDireto(FORNECEDOR_CATEGORIA categoriaInserir)
        {
            _statusSolicitacaoService.ExcluirCategoriaDireto(categoriaInserir);
        }

        public IEnumerable<FORNECEDOR_CATEGORIA> Find(Expression<Func<FORNECEDOR_CATEGORIA, bool>> predicate)
        {
            return _statusSolicitacaoService.Find(predicate);
        }

        public IEnumerable<FORNECEDOR_CATEGORIA> Find(Expression<Func<FORNECEDOR_CATEGORIA, bool>> predicate, bool @readonly = false)
        {
            return _statusSolicitacaoService.Find(predicate, @readonly);
        }

        public FORNECEDOR_CATEGORIA Get(Expression<Func<FORNECEDOR_CATEGORIA, bool>> predicate)
        {
            return _statusSolicitacaoService.Get(predicate);
        }

        public FORNECEDOR_CATEGORIA Get(int id)
        {
            return _statusSolicitacaoService.Get(id);
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
            //return _statusSolicitacaoService.Get(id, @readonly);
        }

        public FORNECEDOR_CATEGORIA Get(int id, bool @readonly = false)
        {
            return _statusSolicitacaoService.Get(id, @readonly);
        }

        public FORNECEDOR_CATEGORIA GetAllReferences(int id, bool @readonly = false)
        {
            return _statusSolicitacaoService.GetAllReferences(id, @readonly);
        }

        public void InserirCategoria(FORNECEDOR_CATEGORIA categoriaInserir)
        {
            _statusSolicitacaoService.InserirCategoria(categoriaInserir);
        }

        public List<FORNECEDOR_CATEGORIA> ListarTodosPorIdContratanteAtivo(int contratanteId)
        {
            return _statusSolicitacaoService.ListarTodosPorIdContratanteAtivo(contratanteId);
        }

        public RetornoPesquisa<FORNECEDOR_CATEGORIA> PesquisarCategorias(string descricao, string codigo, int contratanteId, int tamanhoPagina, int paginaAtual)
        {
            try
            {
                //BUSCA FORNECEDORES E MONTA PAGINAÇÃO
                var predicate = PredicateBuilder.New<FORNECEDOR_CATEGORIA>();

                if (String.IsNullOrEmpty(descricao) && String.IsNullOrEmpty(codigo))
                    predicate = predicate.And(d => d.CONTRATANTE_ID == contratanteId && d.CATEGORIA_PAI_ID == null);
                else
                    predicate = predicate.And(d => d.CONTRATANTE_ID == contratanteId);

                if (!String.IsNullOrEmpty(descricao))
                    predicate = predicate.And(c => c.DESCRICAO.ToUpper().Contains(descricao.ToUpper()));

                if (!String.IsNullOrEmpty(codigo))
                    predicate = predicate.And(c => c.CODIGO.ToUpper().Contains(codigo.ToUpper()));

                return _statusSolicitacaoService.PesquisarCategorias(predicate, tamanhoPagina, paginaAtual);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de categorias", ex);
            }
        }

        public ValidationResult Remove(FORNECEDOR_CATEGORIA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDOR_CATEGORIA entity)
        {
            throw new NotImplementedException();
        }
    }

    public class SolicitacaoStatusWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoStatusWebForLinkAppService
    {
        private readonly ISolicitacaoStatusWebForLinkService _statusSolicitacaoService;

        public SolicitacaoStatusWebForLinkAppService(ISolicitacaoStatusWebForLinkService statusSolicitacaoService)
        {
            try
            {
                _statusSolicitacaoService = statusSolicitacaoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public SOLICITACAO_STATUS Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_STATUS Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_STATUS GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_STATUS> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_STATUS> Find(Expression<Func<SOLICITACAO_STATUS, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SOLICITACAO_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO_STATUS entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_STATUS Get(int id)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_STATUS Get(Expression<Func<SOLICITACAO_STATUS, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_STATUS> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_STATUS> Find(Expression<Func<SOLICITACAO_STATUS, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SOLICITACAO_STATUS BuscarPorID(int id)
        {
            try
            {
                return _statusSolicitacaoService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}