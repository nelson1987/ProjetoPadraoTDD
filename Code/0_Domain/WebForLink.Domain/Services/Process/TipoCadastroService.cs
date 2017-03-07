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
    public interface ITipoCadastroWebForLinkService : IService<TIPO_CADASTRO_FORNECEDOR>
    {
        TIPO_CADASTRO_FORNECEDOR BuscarPorID(int id);
        List<TIPO_CADASTRO_FORNECEDOR> ListarTodos();
    }

    public class TipoCadastroWebForLinkService : Service<TIPO_CADASTRO_FORNECEDOR>, ITipoCadastroWebForLinkService
    {
        private readonly ITipoCadastroWebForLinkRepository _tipoCadastro;

        public TipoCadastroWebForLinkService(ITipoCadastroWebForLinkRepository tipoCadastro) : base(tipoCadastro)
        {
            _tipoCadastro = tipoCadastro;
            try
            {
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
        public TIPO_CADASTRO_FORNECEDOR BuscarPorID(int id)
        {
            try
            {
                return _tipoCadastro.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<TIPO_CADASTRO_FORNECEDOR> ListarTodos()
        {
            try
            {
                return _tipoCadastro.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}