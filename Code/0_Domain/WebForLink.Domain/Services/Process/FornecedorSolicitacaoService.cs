using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class FornecedorSolicitacaoService : Service<FORNECEDOR_SOLICITACAO>
    {
        private readonly IFornecedorSolicitacaoWebForLinkRepository _solicitacaoOrigemFornecedorRepository;

        public FornecedorSolicitacaoService(
            IFornecedorSolicitacaoWebForLinkRepository solicitacaoOrigemFornecedorRepository)
            : base(solicitacaoOrigemFornecedorRepository)
        {
            try
            {
                _solicitacaoOrigemFornecedorRepository = solicitacaoOrigemFornecedorRepository;
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
        public FORNECEDOR_SOLICITACAO BuscarPorID(int id)
        {
            try
            {
                return _solicitacaoOrigemFornecedorRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma solicitação de cadastro de fornecedor por ID",
                    ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FORNECEDOR_SOLICITACAO BuscarPorIDDocumentoSolicitados(int id)
        {
            try
            {
                return _solicitacaoOrigemFornecedorRepository.Find(c => c.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(
                    "Erro ao buscar uma solicitacao de cadastro de Fornecedor por Id de Documentos Solicitados", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}