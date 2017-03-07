using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IDestinatarioWebForLinkAppService : IAppService<DESTINATARIO>
    {
        DESTINATARIO BuscarPorId(int id);

        RetornoPesquisa<DESTINATARIO> BuscarPesquisa(Expression<Func<DESTINATARIO, bool>> filtros, int tamanhoPagina,
            int pagina, Func<DESTINATARIO, IComparable> ordenacao);

        DESTINATARIO Incluir(DESTINATARIO destinatario);
        DESTINATARIO Editar(DESTINATARIO destinatario);
        DESTINATARIO Excluir(DESTINATARIO destinatario);
        bool ValidarEmailDuplicado(string email);
        bool ValidarSeHaCompartilhamentosAtivos(string email);
    }

    public class DestinatarioWebForLinkAppService : AppService<WebForLinkContexto>, IDestinatarioWebForLinkAppService
    {
        private readonly IDestinatarioWebForLinkService _destinatarioService;
        private readonly IDocumentosCompartilhadosWebForLinkService _comparilhamentosService;

        public DestinatarioWebForLinkAppService(IDestinatarioWebForLinkService destinatarioService, IDocumentosCompartilhadosWebForLinkService comparilhamentosService)
        {
            try
            {
                _destinatarioService = destinatarioService;
                _comparilhamentosService = comparilhamentosService;
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
        public DESTINATARIO BuscarPorId(int id)
        {
            try
            {
                return _destinatarioService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="pagina"></param>
        /// <param name="ordenacao"></param>
        /// <returns></returns>
        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public DESTINATARIO Incluir(DESTINATARIO entidade)
        {
            try
            {
                BeginTransaction();
                _destinatarioService.Add(entidade);
                Commit();
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao editar um destinatário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="destinatario"></param>
        /// <returns></returns>
        public DESTINATARIO Editar(DESTINATARIO destinatario)
        {
            try
            {
                BeginTransaction();
                _destinatarioService.Update(destinatario);
                Commit();
                return destinatario;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao editar um destinatário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="destinatario"></param>
        /// <returns></returns>
        public DESTINATARIO Excluir(DESTINATARIO destinatario)
        {
            try
            {
                BeginTransaction();
                _destinatarioService.Delete(destinatario);
                Commit();
                return destinatario;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        public DESTINATARIO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DESTINATARIO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DESTINATARIO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DESTINATARIO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DESTINATARIO> Find(Expression<Func<DESTINATARIO, bool>> predicate, bool @readonly = false)
        {
            return _destinatarioService.Find(predicate);
        }

        public ValidationResult Create(DESTINATARIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(DESTINATARIO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(DESTINATARIO entity)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<DESTINATARIO> BuscarPesquisa(Expression<Func<DESTINATARIO, bool>> filtros,
            int tamanhoPagina, int pagina, Func<DESTINATARIO, IComparable> ordenacao)
        {
            try
            {
                return _destinatarioService.BuscarPesquisa(filtros, tamanhoPagina, pagina, ordenacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao Buscar a pesquisa dos destinatários", ex);
            }
        }

        public bool ValidarEmailDuplicado(string email)
        {
            return _destinatarioService.Find(x => x.EMAIL == email).Any();
        }

        public bool ValidarSeHaCompartilhamentosAtivos(string email)
        {
            return _comparilhamentosService.Find(y => y.WFD_DESTINATARIO
            .Any(x => x.EMAIL == email
            && x.ATIVO
            )).Any();
        }
    }
}