using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class BancoWebForLinkService : Service<TiposDeBanco>, IBancoWebForLinkService
    {
        private readonly IBancoWebForLinkRepository _bancoRepository;

        public BancoWebForLinkService(IBancoWebForLinkRepository banco) : base(banco)
        {
            try
            {
                _bancoRepository = banco;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<TiposDeBanco> ListarTodosPorNome()
        {
            try
            {
                return _bancoRepository.All(true).OrderBy(a => a.BANCO_NM).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao listar os bancos", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}