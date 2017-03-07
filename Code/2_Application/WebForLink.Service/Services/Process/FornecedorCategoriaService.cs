using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorCategoriaService : IService<FORNECEDOR_CATEGORIA>
    {
        FORNECEDOR_CATEGORIA BuscarPorId(int id);
        FORNECEDOR_CATEGORIA Buscar(Expression<Func<FORNECEDOR_CATEGORIA, bool>> filtro);
        FORNECEDOR_CATEGORIA BuscarPorId(int id, int contratanteId);
        FORNECEDOR_CATEGORIA InserirCategoria(FORNECEDOR_CATEGORIA categoria);
        bool verificaDuplicidadeCodigo(int Id, string Codigo, int Contratante);
        bool verificaDuplicidadeDescricao(int Id, string Descricao, int Contratante);
        FORNECEDOR_CATEGORIA AlterarCategoria(FORNECEDOR_CATEGORIA categoria);
        FORNECEDOR_CATEGORIA ExcluirCategoria(FORNECEDOR_CATEGORIA categoria);
        FORNECEDOR_CATEGORIA ExcluirCategoriaDireto(FORNECEDOR_CATEGORIA categoria);
        FORNECEDOR_CATEGORIA BuscarPorCodigoContratanteId(string codigo, int contratanteId);
        FORNECEDOR_CATEGORIA BuscarPorCodigo(string codigo, int contratanteId);
        FORNECEDOR_CATEGORIA ValidarCategoriaExistente(string codigo, int contratanteId);
        FORNECEDOR_CATEGORIA BuscarEmSolicitacaoFornecedor(int idContratante, int idCategoria);
        List<FORNECEDOR_CATEGORIA> ListarTodosPorIdContratanteAtivo(int idContratante);

        RetornoPesquisa<FORNECEDOR_CATEGORIA> PesquisarCategorias(string descricao, string codigo, int contratanteId,
            int tamanhoPagina, int pagina);

        List<FORNECEDOR_CATEGORIA> BuscarCategorias(int contratanteId);
        List<FORNECEDOR_CATEGORIA> BuscarPorCategoriaPai(int id, int contratanteId);
        void Dispose();
    }

    public class FornecedorCategoriaService : AppService<WebForLinkContexto>, IFornecedorCategoriaService
    {
        private const string ErroDuplicidadeCodigo = "Já existe categoria cadastrada com esse código.";
        private const string ErroDuplicidadeDescricao = "Já existe categoria cadastrada com essa descrição.";
        private readonly IFornecedorCategoriaService _categoriaFornecedorService;

        public FornecedorCategoriaService(IFornecedorCategoriaService fornecedorCategoriaService)
        {
            try
            {
                _categoriaFornecedorService = fornecedorCategoriaService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        ///     Buscar Categoria de Fornecedor por Id da Categoria
        /// </summary>
        /// <param name="id">Id da Categoria</param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA BuscarPorId(int id)
        {
            try
            {
                return _categoriaFornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma categoria por ID", ex);
            }
        }

        public FORNECEDOR_CATEGORIA Buscar(Expression<Func<FORNECEDOR_CATEGORIA, bool>> filtro)
        {
            try
            {
                return _categoriaFornecedorService.Get(filtro);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma categoria", ex);
            }
        }

        /// <summary>
        ///     Buscar Categoria de Fornecedor por Id da Categoria e Id do Contratatante
        /// </summary>
        /// <param name="id">Id da Categoria</param>
        /// <param name="contratanteId">Id do Contratante</param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA BuscarPorId(int id, int contratanteId)
        {
            try
            {
                return _categoriaFornecedorService.Get(c => c.CONTRATANTE_ID == contratanteId && c.ID == id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma categoria por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA InserirCategoria(FORNECEDOR_CATEGORIA categoria)
        {
            VerificarDuplicidade(categoria);
            try
            {
                _categoriaFornecedorService.InserirCategoria(categoria);
                return categoria;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar incluir esta Categoria!", ex);
            }
        }

        public bool verificaDuplicidadeCodigo(int Id, string Codigo, int Contratante)
        {
            try
            {
                return
                    _categoriaFornecedorService.Find(
                        x => x.ID != Id && x.CONTRATANTE_ID == Contratante && x.CODIGO == Codigo).Any();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade por Codigo", ex);
            }
        }

        public bool verificaDuplicidadeDescricao(int Id, string Descricao, int Contratante)
        {
            try
            {
                return
                    _categoriaFornecedorService.Find(
                        x => x.ID != Id && x.CONTRATANTE_ID == Contratante && x.DESCRICAO == Descricao).Any();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade por Descrição", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA AlterarCategoria(FORNECEDOR_CATEGORIA categoria)
        {
            //using (_unitOfWork)
            //{
            //FORNECEDOR_CATEGORIA novo = _categoriaFornecedorService.Get(categoria.ID);
            //if (novo == null)
            //    throw new ServiceWebForLinkException("Erro ao tentar alterar a categoria, identificador não encontrado!");
            VerificarDuplicidade(categoria);
            try
            {
                //if (!categoria.Equals(novo))
                //{
                //    novo.ATIVO = categoria.ATIVO;
                //    novo.CATEGORIA_PAI_ID = categoria.CATEGORIA_PAI_ID;
                //    novo.CODIGO = categoria.CODIGO;
                //    novo.CONTRATANTE_ID = categoria.CONTRATANTE_ID;
                //    novo.DESCRICAO = categoria.DESCRICAO;
                //    novo.ISENTO_CONTATOS = categoria.ISENTO_CONTATOS;
                //    novo.ISENTO_DADOSBANCARIOS = categoria.ISENTO_DADOSBANCARIOS;
                //    novo.ISENTO_DOCUMENTOS = categoria.ISENTO_DOCUMENTOS;
                //    novo.PJPF_CATEGORIA_CH_ID = categoria.PJPF_CATEGORIA_CH_ID;

                //    _categoriaFornecedorService.Update(novo);
                //}
                _categoriaFornecedorService.Update(categoria);
                return categoria;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar alterar a categoria.", ex);
            }

            //}


            //FORNECEDOR_CATEGORIA novo = _categoriaFornecedorService.Get(categoria.ID);
            //if (novo == null)
            //    throw new ServiceWebForLinkException("Erro ao tentar alterar a categoria, identificador não encontrado!");
            //VerificarDuplicidade(categoria);
            //try
            //{
            //    if (!categoria.Equals(novo))
            //    {
            //        novo.ATIVO = categoria.ATIVO;
            //        novo.CATEGORIA_PAI_ID = categoria.CATEGORIA_PAI_ID;
            //        novo.CODIGO = categoria.CODIGO;
            //        novo.CONTRATANTE_ID = categoria.CONTRATANTE_ID;
            //        novo.DESCRICAO = categoria.DESCRICAO;
            //        novo.ISENTO_CONTATOS = categoria.ISENTO_CONTATOS;
            //        novo.ISENTO_DADOSBANCARIOS = categoria.ISENTO_DADOSBANCARIOS;
            //        novo.ISENTO_DOCUMENTOS = categoria.ISENTO_DOCUMENTOS;
            //        novo.PJPF_CATEGORIA_CH_ID = categoria.PJPF_CATEGORIA_CH_ID;

            //        _categoriaFornecedorService.Update(novo);
            //    }

            //    return categoria;
            //}
            //catch (Exception ex)
            //{
            //    throw new ServiceWebForLinkException("Erro ao tentar alterar a categoria.", ex);
            //}
        }

        /// <summary>
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA ExcluirCategoria(FORNECEDOR_CATEGORIA categoria)
        {
            VerificarDuplicidade(categoria);
            try
            {
                _categoriaFornecedorService.Delete(categoria);
                return categoria;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma categoria por ID", ex);
            }
        }

        public FORNECEDOR_CATEGORIA ExcluirCategoriaDireto(FORNECEDOR_CATEGORIA categoria)
        {
            VerificarDuplicidade(categoria);
            try
            {
                _categoriaFornecedorService.Delete(_categoriaFornecedorService.Get(x => x.ID == categoria.ID));
                return categoria;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma categoria por ID", ex);
            }
        }

        public FORNECEDOR_CATEGORIA BuscarPorCodigoContratanteId(string codigo, int contratanteId)
        {
            try
            {
                return _categoriaFornecedorService.Get(x => x.CODIGO == codigo && x.CONTRATANTE_ID == contratanteId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma categoria por Código", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="contratanteId"></param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA BuscarPorCodigo(string codigo, int contratanteId)
        {
            return BuscarPorCodigoContratanteId(codigo, contratanteId);
        }

        /// <summary>
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="contratanteId"></param>
        /// <returns>WFD_PJPF_CATEGORIA</returns>
        public FORNECEDOR_CATEGORIA ValidarCategoriaExistente(string codigo, int contratanteId)
        {
            //WFD_PJPF_CATEGORIA categoriaInserida = pjPfCategoria.BuscarPorCodigoContratanteId(codigo, contratanteId);
            //if(categoriaInserida.ID)
            return BuscarPorCodigoContratanteId(codigo, contratanteId);
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        public FORNECEDOR_CATEGORIA BuscarEmSolicitacaoFornecedor(int idContratante, int idCategoria)
        {
            try
            {
                return _categoriaFornecedorService.Get(c => c.CONTRATANTE_ID == idContratante && c.ID == idCategoria);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de Categorias por Contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <returns></returns>
        public List<FORNECEDOR_CATEGORIA> ListarTodosPorIdContratanteAtivo(int idContratante)
        {
            try
            {
                return _categoriaFornecedorService.Find(c => c.CONTRATANTE_ID == idContratante && c.ATIVO).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de Categorias por Contratante", ex);
            }
        }

        /// <summary>
        ///     Método para retorno de Pesquisa de Categorias de Fornecedores na tela de principal da entidade
        /// </summary>
        /// <param name="descricao">Descrição da Categoria</param>
        /// <param name="codigo">Código da Categoria</param>
        /// <param name="contratanteId">Id do Contratante</param>
        /// <param name="pagina">Numero da página na tela</param>
        /// <returns>RetornoPesquisa<WFD_PJPF_CATEGORIA></returns>
        /// <summary>
        ///     Método para retorno de Pesquisa de Categorias na tela de principal da entidade
        /// </summary>
        /// <param name="descricao"></param>
        /// <param name="codigo"></param>
        /// <param name="contratanteId"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public List<FORNECEDOR_CATEGORIA> BuscarCategorias(int contratanteId)
        {
            try
            {
                return
                    _categoriaFornecedorService.Find(
                        x => x.CONTRATANTE_ID == contratanteId && x.CATEGORIA_PAI_ID == null).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar as Categorias", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public List<FORNECEDOR_CATEGORIA> BuscarPorCategoriaPai(int id, int contratanteId)
        {
            try
            {
                return
                    _categoriaFornecedorService.Find(x => x.CATEGORIA_PAI_ID == id && x.CONTRATANTE_ID == contratanteId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Esta categoria", ex);
            }
        }

        public void Dispose()
        {
        }

        public RetornoPesquisa<FORNECEDOR_CATEGORIA> PesquisarCategorias(string descricao, string codigo,
            int contratanteId, int tamanhoPagina, int pagina)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA Get(Expression<Func<FORNECEDOR_CATEGORIA, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_CATEGORIA GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_CATEGORIA> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_CATEGORIA> Find(Expression<Func<FORNECEDOR_CATEGORIA, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(FORNECEDOR_CATEGORIA department)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDOR_CATEGORIA department)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(FORNECEDOR_CATEGORIA entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(List<FORNECEDOR_CATEGORIA> entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(List<FORNECEDOR_CATEGORIA> entity)
        {
            throw new NotImplementedException();
        }

        public List<FORNECEDOR_CATEGORIA> CategoriaVisivelPesquisa(List<FORNECEDOR_CATEGORIA> categorias,
            int idFornecedor)
        {
            return categorias.Where(c => c.CategoriaVisivelPesquisa(c)).ToList();
        }

        private void VerificarDuplicidade(FORNECEDOR_CATEGORIA categoria)
        {
            if (verificaDuplicidadeCodigo(categoria.ID, categoria.CODIGO, categoria.CONTRATANTE_ID))
            {
                throw new ServiceWebForLinkException(ErroDuplicidadeCodigo);
            }
            if (verificaDuplicidadeDescricao(categoria.ID, categoria.DESCRICAO, categoria.CONTRATANTE_ID))
            {
                throw new ServiceWebForLinkException(ErroDuplicidadeDescricao);
            }
        }

        List<ValidationResult> IService<FORNECEDOR_CATEGORIA>.Add(List<FORNECEDOR_CATEGORIA> entity)
        {
            throw new NotImplementedException();
        }

        List<ValidationResult> IService<FORNECEDOR_CATEGORIA>.Delete(List<FORNECEDOR_CATEGORIA> entity)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<FORNECEDOR_CATEGORIA> BuscarPesquisa(Expression<Func<FORNECEDOR_CATEGORIA, bool>> filtros, int tamanhoPagina, int pagina, Func<FORNECEDOR_CATEGORIA, IComparable> ordenacao)
        {
            return _categoriaFornecedorService.BuscarPesquisa(filtros, tamanhoPagina,pagina,ordenacao);
        }

        public ValidationResult Modificar(FORNECEDOR_CATEGORIA entity)
        {
            throw new NotImplementedException();
        }
    }
}