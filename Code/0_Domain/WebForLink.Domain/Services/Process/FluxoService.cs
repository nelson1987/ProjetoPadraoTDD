using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ITipoEnderecoWebForLinkService : IService<TIPO_ENDERECO>
    {
    }

    public class FornecedorDocumentosVersaoWebForLinkService : Service<VersionamentoDeDocumentoDoFornecedor>,
        IFornecedorDocumentosVersaoWebForLinkService
    {
        private readonly IFornecedorDocumentosVersaoWebForLinkRepository _repository;

        public FornecedorDocumentosVersaoWebForLinkService(IFornecedorDocumentosVersaoWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class CompartilhamentoWebForLinkService : Service<Compartilhamentos>, ICompartilhamentoWebForLinkService
    {
        private readonly ICompartilhamentoWebForLinkRepository _repository;

        public CompartilhamentoWebForLinkService(ICompartilhamentoWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public RetornoPesquisa<Compartilhamentos> BuscarPesquisaInvertido(Expression<Func<Compartilhamentos, bool>> filtros, int tamanhoPagina, int pagina, Func<Compartilhamentos, IComparable> ordenacao)
        {
            return _repository.PesquisarInvertido(filtros, tamanhoPagina, pagina, ordenacao);
        }
    }

    public class LogRoboWebForLinkService : Service<ROBO_LOG>, ILogRoboWebForLinkService
    {
        private readonly ILogRoboWebForLinkRepository _repository;

        public LogRoboWebForLinkService(ILogRoboWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class FornecedorInformacaoComplementarComplWebForLinkService : Service<FORNECEDOR_INFORM_COMPL>,
        IFornecedorInformacaoComplementarComplWebForLinkService
    {
        private readonly IFornecedorInformacaoComplementarComplWebForLinkRepository _repository;

        public FornecedorInformacaoComplementarComplWebForLinkService(
            IFornecedorInformacaoComplementarComplWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class SolicitacaoProrrogacaoWebForLinkService : Service<SOLICITACAO_PRORROGACAO>,
        ISolicitacaoProrrogacaoWebForLinkService
    {
        private readonly ISolicitacao_prorrogacaoWebForLinkRepository _repository;

        public SolicitacaoProrrogacaoWebForLinkService(ISolicitacao_prorrogacaoWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SOLICITACAO_PRORROGACAO BuscarPorIdIncluindoSolicitacao(int id)
        {
            return _repository.BuscarPorIdIncluindoSolicitacao(id);
        }
    }


    public class TipoDescricaoWebForLinkService : Service<TIPO_DESCRICAO>, ITipoDescricaoWebForLinkService
    {
        private readonly ITipoDescricaoWebForLinkRepository _repository;

        public TipoDescricaoWebForLinkService(ITipoDescricaoWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId)
        {
            return _repository.ListarPorGrupoId(grupoId);
        }
    }

    public class TipoVisaoWebForLinkService : Service<TIPO_VISAO>, ITipoVisaoWebForLinkService
    {
        private readonly ITipoVisaoWebForLinkRepository _repository;

        public TipoVisaoWebForLinkService(ITipoVisaoWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class ContratanteOrganizacaoCompraService : Service<CONTRATANTE_ORGANIZACAO_COMPRAS>,
        IContratanteOrganizacaoCompraService
    {
        private readonly IContratanteOrganizacaoCompraWebForLinkRepository _repository;

        public ContratanteOrganizacaoCompraService(IContratanteOrganizacaoCompraWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class UfWebForLinkService : Service<TiposDeEstado>, IEstadoWebForLinkService
    {
        private readonly IEstadoWebForLinkRepository _repository;

        public UfWebForLinkService(IEstadoWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class TipoFuncaoBloqueioWebForLinkService : Service<TIPO_FUNCAO_BLOQUEIO>,
        ITipoFuncaoBloqueioWebForLinkService
    {
        private readonly ITipoFuncaoBloqueioWebForLinkRepository _repository;

        public TipoFuncaoBloqueioWebForLinkService(ITipoFuncaoBloqueioWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class QuestionarioWebForLinkService : Service<QUESTIONARIO>, IQuestionarioWebForLinkService
    {
        private readonly IQuestionarioWebForLinkRepository _repository;

        public QuestionarioWebForLinkService(IQuestionarioWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public QUESTIONARIO BuscarPorIdEIdContratante(int id, int idContratante)
        {
            return _repository.BuscarPorIdEIdContratante(id, idContratante);
        }
    }

    public class ContratanteConfiguracaoEmailWebForLinkService : Service<CONTRATANTE_CONFIGURACAO_EMAIL>,
        IContratanteConfiguracaoEmailWebForLinkService
    {
        private readonly IContratanteConfiguracaoEmailWebForLinkRepository _repository;

        public ContratanteConfiguracaoEmailWebForLinkService(
            IContratanteConfiguracaoEmailWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class ContratanteOrganizacaoCompraWebForLinkService : Service<CONTRATANTE_ORGANIZACAO_COMPRAS>,
        IContratanteOrganizacaoCompraWebForLinkService
    {
        private readonly IContratanteOrganizacaoCompraWebForLinkRepository _repository;

        public ContratanteOrganizacaoCompraWebForLinkService(
            IContratanteOrganizacaoCompraWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class FornecedorBaseEnderecoWebForLinkService : Service<FORNECEDORBASE_ENDERECO>,
        IFornecedorBaseEnderecoWebForLinkService
    {
        private readonly IFornecedorBaseEnderecoWebForLinkRepository _repository;

        public FornecedorBaseEnderecoWebForLinkService(IFornecedorBaseEnderecoWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }
    public class FornecedorBaseContatosWebForLinkService : Service<FORNECEDORBASE_CONTATOS>,
        IFornecedorBaseContatosWebForLinkService
    {
        private readonly IFornecedorBaseContatosWebForLinkRepository _repository;

        public FornecedorBaseContatosWebForLinkService(IFornecedorBaseContatosWebForLinkRepository repository)
            : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }
    public class FornecedorBaseUnspscWebForLinkService : Service<FORNECEDORBASE_UNSPSC>,
        IFornecedorBaseUnspscWebForLinkService
    {
        private readonly IFornecedorBaseUnspscWebForLinkRepository _repository;

    public FornecedorBaseUnspscWebForLinkService(IFornecedorBaseUnspscWebForLinkRepository repository)
            : base(repository)
        {
        try
        {
            _repository = repository;
        }
        catch (Exception ex)
        {
            throw new ServiceWebForLinkException(ex.Message);
        }
    }
}
public class FornecedorInformacaoComplementarComplService : Service<FORNECEDOR_INFORM_COMPL>,
        IFornecedorInformacaoComplementarComplService
    {
        private readonly IFornecedorInformacaoComplementarComplWebForLinkRepository _repository;

        public FornecedorInformacaoComplementarComplService(
            IFornecedorInformacaoComplementarComplWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class TipoEnderecoWebForLinkService : Service<TIPO_ENDERECO>, ITipoEnderecoWebForLinkService
    {
        private readonly ITipoEnderecoWebForLinkRepository _repository;

        public TipoEnderecoWebForLinkService(ITipoEnderecoWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class TipoUnspscWebForLinkService : Service<TIPO_UNSPSC>, ITipoUnspscWebForLinkService
    {
        private readonly ITipoUnspscWebForLinkRepository _repository;

        public TipoUnspscWebForLinkService(ITipoUnspscWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class FornecedorUnspscWebForLinkService : Service<FORNECEDOR_UNSPSC>, IFornecedorUnspscWebForLinkService
    {
        private readonly IFornecedorUnspscWebForLinkRepository _repository;

        public FornecedorUnspscWebForLinkService(IFornecedorUnspscWebForLinkRepository repository) : base(repository)
        {
            try
            {
                _repository = repository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class SolicitacaoServicoMaterialWebForLinkService : Service<SOLICITACAO_UNSPSC>,
        ISolicitacaoServicoMaterialWebForLinkService
    {
        private readonly ISolicitacaoServicoMaterialWebForLinkRepository _fluxoRepository;

        public SolicitacaoServicoMaterialWebForLinkService(
            ISolicitacaoServicoMaterialWebForLinkRepository fluxoRepository) : base(fluxoRepository)
        {
            try
            {
                _fluxoRepository = fluxoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public IEnumerable<SOLICITACAO_UNSPSC> BuscarPorSolicitacaoId(int idSolicitacao)
        {
            return _fluxoRepository.Find(x => x.SOLICITACAO_ID == idSolicitacao);
        }
    }

    public class SolicitacaoBloqueioWebForLinkService : Service<SOLICITACAO_BLOQUEIO>,
        ISolicitacaoBloqueioWebForLinkService
    {
        private readonly ISolicitacaoBloqueioWebForLinkRepository _fluxoRepository;

        public SolicitacaoBloqueioWebForLinkService(ISolicitacaoBloqueioWebForLinkRepository fluxoRepository)
            : base(fluxoRepository)
        {
            try
            {
                _fluxoRepository = fluxoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class FornecedorBaseConviteWebForLinkService : Service<FORNECEDORBASE_CONVITE>,
        IFornecedorBaseConviteWebForLinkService
    {
        private readonly IFornecedorBaseConviteWebForLinkRepository _fluxoRepository;

        public FornecedorBaseConviteWebForLinkService(IFornecedorBaseConviteWebForLinkRepository fluxoRepository)
            : base(fluxoRepository)
        {
            try
            {
                _fluxoRepository = fluxoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
    }

    public class FluxoWebForLinkService : Service<Fluxo>, IFluxoWebForLinkService
    {
        private readonly IFluxoWebForLinkRepository _fluxoRepository;

        public FluxoWebForLinkService(IFluxoWebForLinkRepository fluxoRepository) : base(fluxoRepository)
        {
            try
            {
                _fluxoRepository = fluxoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tipoFluxoId"></param>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public Fluxo BuscarPorTipoEContratante(int tipoFluxoId, int contratanteId)
        {
            try
            {
                return _fluxoRepository.BuscarPorTipoEContratante(tipoFluxoId, contratanteId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }

        public List<Fluxo> ListarPorContratanteId(int contratanteId)
        {
            try
            {
                Func<Fluxo, IComparable> ordenacao = (Fluxo a) => a.FLUXO_NM;
                return _fluxoRepository.Find(x => x.CONTRATANTE_ID == contratanteId).OrderBy(ordenacao).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}