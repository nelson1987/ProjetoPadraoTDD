using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IEnderecoWebForLinkService : IService<TIPO_ENDERECO>
    {
        TiposDeEstado BuscarPorID(string siglaUf);
        List<TiposDeEstado> ListarTodosPorNome();
        TIPO_ENDERECO BuscarTipoEnderecoPorID(int id);
        List<TIPO_ENDERECO> ListarTodosTiposEnderecosPorNome();
    }

    public class EnderecoWebForLinkService : Service<TIPO_ENDERECO>, IEnderecoWebForLinkService
    {
        private readonly ITipoEnderecoWebForLinkRepository _enderecoRepository;
        private readonly IEstadoWebForLinkRepository _ufRepository;

        public EnderecoWebForLinkService(IEstadoWebForLinkRepository uf, ITipoEnderecoWebForLinkRepository endereco)
            : base(endereco)
        {
            try
            {
                _ufRepository = uf;
                _enderecoRepository = endereco;
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
        public TiposDeEstado BuscarPorID(string siglaUf)
        {
            try
            {
                return _ufRepository.Find(x => x.UF_SGL.ToUpper() == siglaUf.ToUpper()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o UF por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<TiposDeEstado> ListarTodosPorNome()
        {
            try
            {
                return _ufRepository.All().OrderBy(b => b.UF_SGL).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de UF", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TIPO_ENDERECO BuscarTipoEnderecoPorID(int id)
        {
            try
            {
                return _enderecoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o tipo de endereço por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<TIPO_ENDERECO> ListarTodosTiposEnderecosPorNome()
        {
            try
            {
                return _enderecoRepository.All().OrderBy(x => x.NM_TP_ENDERECO).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de tipo de endereços", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}