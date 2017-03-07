using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.UnitOfWork;

using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Domain.Services.Fornecedores
{
    public interface IFornecedoresService
    {
        Fornecedor InserirCliente(Fornecedor usuario);
    }
    public class FornecedoresService : AppService<WebForLinkContexto>, IFornecedoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFornecedorRepository _fornecedorRepository;
        public FornecedoresService(IUnitOfWork unitOfWork, IFornecedorRepository fornecedorRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _fornecedorRepository = fornecedorRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// Caso de Uso 1.a - Inserir Cliente
        /// </summary>
        /// <param name="usuario"></param>
        public Fornecedor InserirCliente(Fornecedor usuario)
        {
            try
            {
                //Inserir na Tabela
                return _fornecedorRepository.Inserir(usuario);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar inserir Usuário. Tente novamente.", ex);
            }
        }

        public void Dispose()
        {
            _unitOfWork.Finalizar();
        }
    }
}
