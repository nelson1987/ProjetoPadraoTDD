using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IDestinatarioWebForLinkService : IService<DESTINATARIO>
    {
        DESTINATARIO BuscarPorId(int id);
        //RetornoPesquisa<DESTINATARIO> BuscarPesquisa(Expression<Func<DESTINATARIO, bool>> filtros, int tamanhoPagina,
        //    int pagina, Func<DESTINATARIO, IComparable> ordenacao);

        DESTINATARIO Incluir(DESTINATARIO destinatario);
        DESTINATARIO Editar(DESTINATARIO destinatario);
        DESTINATARIO Excluir(DESTINATARIO destinatario);
    }

    public class DestinatarioWebForLinkService : Service<DESTINATARIO>, IDestinatarioWebForLinkService
    {
        private readonly IDestinatarioWebForLinkRepository _destinatarioRepository;

        public DestinatarioWebForLinkService(IDestinatarioWebForLinkRepository destinatarioRepository)
            : base(destinatarioRepository)
        {
            try
            {
                _destinatarioRepository = destinatarioRepository;
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
        public DESTINATARIO BuscarPorId(int id)
        {
            try
            {
                return _destinatarioRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        ///// <summary>
        ///// </summary>
        ///// <param name="filtros"></param>
        ///// <param name="tamanhoPagina"></param>
        ///// <param name="pagina"></param>
        ///// <param name="ordenacao"></param>
        ///// <returns></returns>
        //public RetornoPesquisa<DESTINATARIO> BuscarPesquisa(Expression<Func<DESTINATARIO, bool>> filtros,
        //    int tamanhoPagina, int pagina, Func<DESTINATARIO, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        return _destinatarioRepository.Pesquisar(filtros, tamanhoPagina, pagina, ordenacao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
        //    }
        //}

        /// <summary>
        /// </summary>
        /// <param name="destinatario"></param>
        /// <returns></returns>
        public DESTINATARIO Incluir(DESTINATARIO destinatario)
        {
            try
            {
                _destinatarioRepository.Add(destinatario);
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
        public DESTINATARIO Editar(DESTINATARIO destinatario)
        {
            try
            {
                _destinatarioRepository.Update(destinatario);
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
                _destinatarioRepository.Delete(destinatario);
                return destinatario;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}