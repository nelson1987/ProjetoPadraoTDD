using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoModificacaoContatoWebForLinkService : IService<SolicitacaoModificacaoDadosContato>
    {
        SolicitacaoModificacaoDadosContato BuscarPorId(int id);
        List<SolicitacaoModificacaoDadosContato> ListarPorSolicitacaoId(int solicitacaoID);
        void InserirSolicitacao(SolicitacaoModificacaoDadosContato solicitacao);
        void InserirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes);
        void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosContato solicitacao);
        void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes);
        void ExcluirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes);
        SOLICITACAO IncluirSolicitacao(int contratanteId, int fornecedorId, int usuarioId, int tipoFluxoId);
        void ManterContatoCadastroFornecedor(List<SolicitacaoModificacaoDadosContato> contatos, int SolicitacaoId);
        void InserirOuAtualizar(SolicitacaoModificacaoDadosContato solicitacao);
        void Excluir(SolicitacaoModificacaoDadosContato item);
    }

    public class SolicitacaoContatoWebForLinkService : Service<SolicitacaoModificacaoDadosContato>,
        ISolicitacaoModificacaoContatoWebForLinkService
    {
        private readonly IContratanteConfiguracaoWebForLinkRepository _contratanteConfig;
        private readonly IFluxoWebForLinkRepository _fluxoRepository;
        private readonly ISolicitacaoModificacaoContatoWebForLinkRepository _solicitacaoContatoRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public SolicitacaoContatoWebForLinkService(
            IFluxoWebForLinkRepository fluxoRepository,
            ISolicitacaoWebForLinkRepository solicitacaoRepository,
            ISolicitacaoModificacaoContatoWebForLinkRepository solicitacaoContatoRepository,
            IContratanteConfiguracaoWebForLinkRepository contratanteConfig) : base(solicitacaoContatoRepository)
        {
            try
            {
                _fluxoRepository = fluxoRepository;
                _solicitacaoRepository = solicitacaoRepository;
                _solicitacaoContatoRepository = solicitacaoContatoRepository;
                _contratanteConfig = contratanteConfig;
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
        public SolicitacaoModificacaoDadosContato BuscarPorId(int id)
        {
            return _solicitacaoContatoRepository.Get(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public List<SolicitacaoModificacaoDadosContato> ListarPorSolicitacaoId(int solicitacaoID)
        {
            return _solicitacaoContatoRepository.ListarPorSolicitacaoId(solicitacaoID);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void InserirSolicitacao(SolicitacaoModificacaoDadosContato solicitacao)
        {
            _solicitacaoContatoRepository.Add(solicitacao);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacoes"></param>
        public void InserirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes)
        {
            foreach (var item in solicitacoes)
            {
                _solicitacaoContatoRepository.Add(item);
            }
            //Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosContato solicitacao)
        {
            _solicitacaoContatoRepository.InserirOuAtualizar(solicitacao);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacoes"></param>
        public void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes)
        {
            foreach (var item in solicitacoes)
            {
                _solicitacaoContatoRepository.InserirOuAtualizar(item);
            }
            //Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacoes"></param>
        public void ExcluirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes)
        {
            foreach (var item in solicitacoes)
            {
                _solicitacaoContatoRepository.Excluir(item);
            }
        }

        public SOLICITACAO IncluirSolicitacao(int contratanteId, int fornecedorId, int usuarioId, int tipoFluxoId)
        {
            var fluxo = _fluxoRepository.BuscarPorTipoEContratante(tipoFluxoId, contratanteId).ID; // Bloqueio
            var solicitacaoStatus = (int) EnumStatusTramite.EmAprovacao; // EM APROVACAO
            var dsta = _contratanteConfig.Get(contratanteId).PRAZO_ENTREGA_FICHA;
            var solicitacaoExu = new SOLICITACAO
            {
                CONTRATANTE_ID = contratanteId,
                FLUXO_ID = fluxo, // Bloqueio
                SOLICITACAO_DT_CRIA = DateTime.Now,
                SOLICITACAO_STATUS_ID = solicitacaoStatus, // EM APROVACAO
                USUARIO_ID = usuarioId,
                PJPF_ID = fornecedorId,
                MOTIVO = null,
                TP_PJPF = null,
                DT_PRAZO = DateTime.Now.AddDays(dsta)
            };
            _solicitacaoRepository.Add(solicitacaoExu);
            return solicitacaoExu;
        }

        public void ManterContatoCadastroFornecedor(List<SolicitacaoModificacaoDadosContato> contatos, int SolicitacaoId)
        {
            try
            {
                _solicitacaoContatoRepository.Delete(
                    _solicitacaoContatoRepository.All().Where(x => x.SOLICITACAO_ID == SolicitacaoId).ToList());
                _solicitacaoContatoRepository.Add(contatos);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os contatos na solicitação.", e);
            }
        }

        public void InserirOuAtualizar(SolicitacaoModificacaoDadosContato solicitacao)
        {
            throw new NotImplementedException();
        }

        public void Excluir(SolicitacaoModificacaoDadosContato item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}