using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IContratanteWebForLinkService : IService<Contratante>
    {
        void Dispose();
        Contratante BuscarPorId(int id);
        WFD_CONTRATANTE_PJPF BuscarPorTipoPjpfId(int tipoPjpfId, int contratanteId);
        Contratante BuscarPorId(int id, bool incluir);
        Contratante BuscarPorIdDocumentoSolicitado(int id);
        Contratante InserirContratante(Contratante entidade);
        Contratante AlterarContratante(Contratante entidade);
        List<Contratante> ListarTodosPorGrupo(int idGrupo);
        List<Contratante> ListarTodosPorUsuario(int idUsuario);
        List<Contratante> ListarTodosPorPapel(int papelId);
        List<Contratante> ListarTodos(int grupoId);
        List<Contratante> ListarTodos();

        RetornoPesquisa<Contratante> PesquisarContratantes(PesquisaContratanteFiltrosDTO filtros, int pagina,
            int tamanhoPagina);

        RetornoPesquisa<Contratante> PesquisarContratantes(Expression<Func<Contratante, bool>> predicate,
            int pagina, int tamanhoPagina);

        int[] ListarTodasAprovadas();
        bool ProcurarPorCnpj(string cnpj);
        List<Contratante> Listar(Expression<Func<Contratante, bool>> filtro);
    }

    public class ContratanteWebForLinkService : Service<Contratante>, IContratanteWebForLinkService
    {
        private readonly IContratanteWebForLinkRepository _contratanteService;
        private readonly IContratanteFornecedorWebForLinkRepository _contratanteServicePjpf;

        public ContratanteWebForLinkService(IContratanteWebForLinkRepository contratanteService,
            IContratanteFornecedorWebForLinkRepository contratanteService_pjpf)
            : base(contratanteService)
        {
            try
            {
                _contratanteService = contratanteService;
                _contratanteServicePjpf = contratanteService_pjpf;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contratante BuscarPorId(int id)
        {
            try
            {
                return _contratanteService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um contratante", ex);
            }
        }

        public WFD_CONTRATANTE_PJPF BuscarPorTipoPjpfId(int tipoPjpfId, int contratanteId)
        {
            try
            {
                return
                    _contratanteServicePjpf.Find(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == tipoPjpfId)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incluir"></param>
        /// <returns></returns>
        public Contratante BuscarPorId(int id, bool incluir)
        {
            try
            {
                if (incluir)
                    return _contratanteService.Get(id);
                return BuscarPorId(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contratante BuscarPorIdDocumentoSolicitado(int id)
        {
            try
            {
                return _contratanteService.BuscarPorIdDocumentoSolicitado(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de contratantes por Usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Contratante InserirContratante(Contratante entidade)
        {
            try
            {
                _contratanteService.Add(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Contratante AlterarContratante(Contratante entidade)
        {
            try
            {
                _contratanteService.Update(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao alterar um contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public List<Contratante> ListarTodosPorGrupo(int idGrupo)
        {
            try
            {
                return _contratanteService.ListarTodosPorGrupo(idGrupo);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de contratantes por Grupo", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<Contratante> ListarTodosPorUsuario(int idUsuario)
        {
            try
            {
                return _contratanteService.ListarTodosPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de contratantes por Usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="papelId"></param>
        /// <returns></returns>
        public List<Contratante> ListarTodosPorPapel(int papelId)
        {
            try
            {
                return _contratanteService.ListarTodosPorPapel(papelId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de contratantes por Usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<Contratante> ListarTodos(int grupoId)
        {
            try
            {
                return _contratanteService.Find(x => x.WFD_GRUPO.Any(y => y.ID == grupoId)).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Perguntas", ex);
            }
        }

        public List<Contratante> ListarTodos()
        {
            try
            {
                return _contratanteService.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Perguntas", ex);
            }
        }

        public List<Contratante> Listar(Expression<Func<Contratante, bool>> filtro)
        {
            try
            {
                return _contratanteService.Find(filtro).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Perguntas", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns></returns>
        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns></returns>
        public RetornoPesquisa<Contratante> PesquisarContratantes(Expression<Func<Contratante, bool>> predicate,
            int pagina, int tamanhoPagina)
        {
            try
            {
                return _contratanteService.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Contratantes", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int[] ListarTodasAprovadas()
        {
            return _contratanteService.ListarTodasAprovadas();
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public bool ProcurarPorCnpj(string cnpj)
        {
            try
            {
                return _contratanteService.Find(c => c.CNPJ == cnpj) != null;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um contratante", ex);
            }
        }

        public RetornoPesquisa<Contratante> PesquisarContratantes(PesquisaContratanteFiltrosDTO filtros, int pagina,
            int tamanhoPagina)
        {
            throw new NotImplementedException();
        }
    }
}