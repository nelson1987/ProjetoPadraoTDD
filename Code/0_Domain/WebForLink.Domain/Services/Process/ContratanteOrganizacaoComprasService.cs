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
    public class ContratanteOrganizacaoComprasWebForLinkService
        : Service<CONTRATANTE_ORGANIZACAO_COMPRAS>,
            IContratanteOrganizacaoComprasWebForLinkService
    {
        private readonly IContratanteOrganizacaoCompraWebForLinkRepository _organizacaoComprasContratante;

        public ContratanteOrganizacaoComprasWebForLinkService(
            IContratanteOrganizacaoCompraWebForLinkRepository organizacaoComprasContratante)
            : base(organizacaoComprasContratante)
        {
            try
            {
                _organizacaoComprasContratante = organizacaoComprasContratante;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorContratanteId(int contratanteId)
        {
            try
            {
                return _organizacaoComprasContratante.Find(x => x.CONTRATANTE_ID == contratanteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorId(int id)
        {
            try
            {
                return _organizacaoComprasContratante.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Organização de Compras", ex);
            }
        }

        public List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante)
        {
            try
            {
                return _organizacaoComprasContratante.Find(x => x.CONTRATANTE_ID == idContratante).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Organização de Compras", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}