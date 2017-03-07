using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorBaseContatoWebForLinkAppService
    {
        FORNECEDORBASE_CONTATOS BuscarPorID(int id);
    }

    public class FornecedorBaseContatoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorBaseContatoWebForLinkAppService
    {
        private readonly IFornecedorBaseContatosWebForLinkService _fornecedorBaseServiceContatos;
        public FornecedorBaseContatoWebForLinkAppService(
            IFornecedorBaseContatosWebForLinkService fornecedorBaseServiceContatos)
        {
            try
            {
                _fornecedorBaseServiceContatos = fornecedorBaseServiceContatos;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDORBASE_CONTATOS BuscarPorID(int id)
        {
            try
            {
                return _fornecedorBaseServiceContatos.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar os Contatos dos Fornecedores Base por id", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}