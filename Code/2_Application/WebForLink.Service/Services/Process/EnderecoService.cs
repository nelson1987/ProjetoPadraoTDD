using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IEnderecoWebForLinkAppService : IAppService<TiposDeEstado>
    {
        TiposDeEstado BuscarPorID(string siglaUf);
        List<TiposDeEstado> ListarTodosPorNome();
        TIPO_ENDERECO BuscarTipoEnderecoPorID(int id);
        List<TIPO_ENDERECO> ListarTodosTiposEnderecosPorNome();
    }

    public class EnderecoWebForLinkAppService : AppService<WebForLinkContexto>, IEnderecoWebForLinkAppService
    {
        private readonly ITipoEnderecoWebForLinkService _enderecoService;
        private readonly IEstadoWebForLinkService _estadoService;

        public EnderecoWebForLinkAppService(IEstadoWebForLinkService uf, ITipoEnderecoWebForLinkService endereco)
        {
            try
            {
                _estadoService = uf;
                _enderecoService = endereco;
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
        public TiposDeEstado BuscarPorID(string siglaUf)
        {
            try
            {
                return _estadoService.Get(x => x.UF_SGL.ToUpper() == siglaUf.ToUpper());
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
                return _estadoService.All().OrderBy(b => b.UF_SGL).ToList();
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
                return _enderecoService.Get(id);
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
                return _enderecoService.All().OrderBy(x => x.NM_TP_ENDERECO).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de tipo de endereços", ex);
            }
        }

        public TiposDeEstado Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public TiposDeEstado Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public TiposDeEstado GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TiposDeEstado> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TiposDeEstado> Find(Expression<Func<TiposDeEstado, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(TiposDeEstado entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(TiposDeEstado entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(TiposDeEstado entity)
        {
            throw new NotImplementedException();
        }
    }
}