using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class TipoCadastroWebForLinkAppService : AppService<WebForLinkContexto>, ITipoCadastroWebForLinkAppService
    {
        private readonly ITipoCadastroWebForLinkService _tipoCadastro;

        public TipoCadastroWebForLinkAppService(ITipoCadastroWebForLinkService tipoCadastro)
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