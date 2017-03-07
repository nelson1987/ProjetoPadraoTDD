using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IMeusDocumentosWebForLinkService : IService<Compartilhamentos>
    {
        Compartilhamentos BuscarPorID(int id);
    }

    public class MeusDocumentosWebForLinkService : Service<Compartilhamentos>, IMeusDocumentosWebForLinkService
    {
        private readonly ICompartilhamentoWebForLinkRepository _compartilhamentoRepository;

        public MeusDocumentosWebForLinkService(ICompartilhamentoWebForLinkRepository compartilhamentos)
            : base(compartilhamentos)
        {
            try
            {
                _compartilhamentoRepository = compartilhamentos;
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
        public Compartilhamentos BuscarPorID(int id)
        {
            try
            {
                return _compartilhamentoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        //public RetornoPesquisa<Compartilhamentos> BuscarPesquisa(
        //    Expression<Func<Compartilhamentos, bool>> filtros, int tamanhoPagina, int pagina,
        //    Func<Compartilhamentos, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        return _compartilhamentoRepository.Pesquisar(filtros, tamanhoPagina, pagina, ordenacao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
        //    }
        //}

        public void Dispose()
        {
        }
    }
}