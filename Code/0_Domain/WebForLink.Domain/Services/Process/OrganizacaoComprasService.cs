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
    public interface IOrganizacaoComprasWebForLinkService : IService<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
        CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorId(int id);
        List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante);
    }

    public class OrganizacaoComprasWebForLinkService : Service<CONTRATANTE_ORGANIZACAO_COMPRAS>,
        IOrganizacaoComprasWebForLinkService
    {
        private readonly IContratanteOrganizacaoCompraWebForLinkRepository _contratanteOrganizacaoCompra;

        public OrganizacaoComprasWebForLinkService(
            IContratanteOrganizacaoCompraWebForLinkRepository contratanteOrganizacaoCompra)
            : base(contratanteOrganizacaoCompra)
        {
            try
            {
                _contratanteOrganizacaoCompra = contratanteOrganizacaoCompra;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorId(int id)
        {
            try
            {
                return _contratanteOrganizacaoCompra.Get(id);
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
                return _contratanteOrganizacaoCompra.Find(x => x.CONTRATANTE_ID == idContratante).ToList();
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