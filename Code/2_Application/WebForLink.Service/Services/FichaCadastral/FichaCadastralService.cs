using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Entities.WebForLink;

using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public interface IFichaCadastralService
    {
        void ExibirFichaCadastral(int solicitacaoId);
        Fornecedor BuscarFichaCadastralMeuContratante(int contratanteId);
        List<Fornecedor> ListarTodosFornecedores(int contratanteId);
        Fornecedor BuscarFichaCadastralFornecedorIndividual(int contratanteId, int fornecedorId);
        Fornecedor BuscarFichaCadastralFornecedorConvencional(int contratanteId, int fornecedorId);
        Fornecedor BuscarFichaCadastralContratante(int contratanteId);
    }
    public class FichaCadastralService : AppService<WebForLinkContexto>, IFichaCadastralService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        public FichaCadastralService(IUnitOfWork processo, ISolicitacaoWebForLinkRepository solicitacao, IFornecedorWebForLinkRepository fornecedorRepository)
        {
            try
            {
                _unitOfWork = processo;
                _solicitacaoRepository = solicitacao;
                _fornecedorRepository = fornecedorRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void ExibirFichaCadastral(int solicitacaoId)
        {
            int tpFluxoId = _solicitacaoRepository.BuscarTipoFluxoId(solicitacaoId);
            SOLICITACAO solicitacao = _solicitacaoRepository.BuscarPorIdControleSolicitacoes(solicitacaoId);
        }
        public Fornecedor BuscarFichaCadastralMeuContratante(int contratanteId)
        {
            //Todos Contratante que pagam o sistema
            Fornecedor minhaFichaCadastral = _fornecedorRepository.Buscar(x => x.CONTRATANTE_ID == contratanteId && x.WFD_CONTRATANTE_PJPF.Any(y => y.TP_PJPF == 1));
            return new Fornecedor();
        }

        public List<Fornecedor> ListarTodosFornecedores(int contratanteId)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            var fornecedor = _fornecedorRepository.Listar().ToList();
            fornecedores.AddRange(fornecedor
                    .Where(x => x.FornecedorConvencional(x, contratanteId))
                    .Union(fornecedor.Where(x => x.FornecedorIndividual(x)))
                    .ToList());
            //fornecedores.AddRange(fornecedor.Where(x => x.FornecedorIndividual(x)).ToList());
            return fornecedores;
        }

        public Fornecedor BuscarFichaCadastralFornecedorIndividual(int contratanteId, int fornecedorId)
        {
            return _fornecedorRepository.Buscar(x => x.CONTRATANTE_ID == contratanteId && x.WFD_CONTRATANTE_PJPF.Any(y => y.TP_PJPF == 3 & y.PJPF_ID == fornecedorId));
        }

        public Fornecedor BuscarFichaCadastralFornecedorConvencional(int contratanteId, int fornecedorId)
        {
            return _fornecedorRepository.Buscar(x => x.CONTRATANTE_ID == contratanteId && x.WFD_CONTRATANTE_PJPF.Any(y => y.TP_PJPF == 2 && y.PJPF_ID == fornecedorId));
        }

        public Fornecedor BuscarFichaCadastralContratante(int contratanteId)
        {
            return _fornecedorRepository.Buscar(x => x.CONTRATANTE_ID == contratanteId && x.WFD_CONTRATANTE_PJPF.Any(y => y.TP_PJPF == 1));
        }

        public void Dispose()
        {
            _unitOfWork.Finalizar();
        }
    }
}
