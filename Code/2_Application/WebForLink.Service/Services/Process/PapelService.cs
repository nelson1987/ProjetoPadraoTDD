using System;
using System.Collections.Generic;
using LinqKit;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Services.Process;
using WebForLink.Application.Interfaces;

namespace WebForLink.Application.Services.Process
{
    public class PapelWebForLinkAppService : AppService<WebForLinkContexto>, IPapelWebForLinkAppService
    {
        private readonly IPapelWebForLinkService _papelService;
        public PapelWebForLinkAppService(IPapelWebForLinkService papel)
        {
            try
            {
                _papelService = papel;
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
        public Papel BuscarPorID(int id)
        {
            try
            {
                return _papelService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Papel InserirPapel(Papel entidade)
        {
            try
            {
                _papelService.InserirPapel(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um papel.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<Papel> ListarTodos(int contratanteId)
        {
            try
            {
                return _papelService.ListarTodos(contratanteId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="papeis"></param>
        /// <returns></returns>
        public List<Papel> ListarTodos(int[] papeis)
        {
            try
            {
                return _papelService.ListarTodos(papeis);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public Papel BuscarPorContratanteETipoPapel(int contratanteId, int tipo)
        {
            try
            {
                return _papelService.BuscarPorContratanteETipoPapel(contratanteId, tipo);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o papel por Tipo", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public int[] EmpilharPorUsuarioId(int usuarioId)
        {
            try
            {
                return _papelService.EmpilharPorUsuarioId(usuarioId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Papel AlterarPapel(Papel entidade)
        {
            try
            {
                _papelService.AlterarPapel(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um papel.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns></returns>
        public Papel BuscarPorSigla(string sigla)
        {
            try
            {
                return _papelService.BuscarPorSigla(sigla);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um papel por sigla", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns></returns>
        public RetornoPesquisa<Papel> PesquisarPapel(PesquisaPapelFiltrosDTO filtros, int pagina, int tamanhoPagina)
        {
            var predicate = PredicateBuilder.New<Papel>();
            predicate = predicate.And(d => d.CONTRATANTE_ID == filtros.ContratanteUsuario);

            if (!string.IsNullOrEmpty(filtros.Nome))
                predicate = predicate.And(c => c.PAPEL_NM.Contains(filtros.Nome));
            if (!string.IsNullOrEmpty(filtros.Sigla))
                predicate = predicate.And(c => c.PAPEL_SGL.Contains(filtros.Sigla));
            if (filtros.ContratanteId.HasValue && filtros.ContratanteId != 0)
                predicate = predicate.And(c => c.CONTRATANTE_ID == filtros.ContratanteId);

            try
            {
                return _papelService.PesquisarPapel(filtros, pagina, tamanhoPagina);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Papeis", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ExcluirPapel(int id)
        {
            try
            {
                _papelService.ExcluirPapel(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um papel.", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}