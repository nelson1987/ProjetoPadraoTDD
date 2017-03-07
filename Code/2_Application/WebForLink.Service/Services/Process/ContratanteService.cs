using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IContratanteWebForLinkAppService : IAppService<Contratante>
    {
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

    public class ContratanteWebForLinkAppService : AppService<WebForLinkContexto>, IContratanteWebForLinkAppService
    {
        private readonly IContratanteWebForLinkService _contratanteService;
        private readonly IContratantePjpfWebForLinkService _contratanteServicePjpf;

        public ContratanteWebForLinkAppService(
            IContratanteWebForLinkService contratanteService,
            IContratantePjpfWebForLinkService contratanteService_pjpf)
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
                return _contratanteServicePjpf.Get(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == tipoPjpfId);
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
                    return _contratanteService.Get(id, incluir);
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
                return _contratanteService.All().AsQueryable().OrderBy(filtro).ToList();
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
        public RetornoPesquisa<Contratante> PesquisarContratantes(PesquisaContratanteFiltrosDTO filtros, int pagina,
            int tamanhoPagina)
        {
            try
            {
                var predicate = PredicateBuilder.New<Contratante>();
                if (!string.IsNullOrEmpty(filtros.CNPJ))
                    predicate = predicate.And(x => x.CNPJ.Contains(filtros.CNPJ));

                if (!string.IsNullOrEmpty(filtros.RazaoSocial))
                    predicate = predicate.And(x => x.RAZAO_SOCIAL.Contains(filtros.RazaoSocial));

                if (!string.IsNullOrEmpty(filtros.NomeFantasia))
                    predicate = predicate.And(x => x.NOME_FANTASIA.Contains(filtros.NomeFantasia));

                if (!string.IsNullOrEmpty(filtros.Estilo))
                    predicate = predicate.And(x => x.ESTILO.Contains(filtros.Estilo));

                //if (!string.IsNullOrEmpty(filtros.ContratanteCodErp))
                //    predicate = predicate.And(x => x.CONTRANTE_COD_ERP.Contains(filtros.ContratanteCodErp));

                if (filtros.TipoCadastroId.HasValue && filtros.TipoCadastroId != 0)
                    predicate = predicate.And(x => x.TIPO_CADASTRO_ID == filtros.TipoCadastroId);

                return PesquisarContratantes(predicate, pagina, tamanhoPagina);
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

        public RetornoPesquisa<Contratante> PesquisarContratantes(Expression<Func<Contratante, bool>> predicate,
            int pagina, int tamanhoPagina)
        {
            throw new NotImplementedException();
        }

        public Contratante Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Contratante Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Contratante GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contratante> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contratante> Find(Expression<Func<Contratante, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Contratante entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Contratante entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Contratante entity)
        {
            throw new NotImplementedException();
        }
    }
}