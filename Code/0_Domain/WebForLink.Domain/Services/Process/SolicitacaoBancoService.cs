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
    public interface ISolicitacaoModificacaoBancoWebForLinkService : IService<SolicitacaoModificacaoDadosBancario>
    {
        SolicitacaoModificacaoDadosBancario BuscarPorId(int id);
        List<SolicitacaoModificacaoDadosBancario> ListarPorSolicitacaoId(int solicitacaoID);
        void InserirSolicitacao(SolicitacaoModificacaoDadosBancario solicitacao);
        void InserirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes);
        void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosBancario solicitacao);
        void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes);
        void ExcluirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes);
        void InserirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> bancos, SOLICITACAO solicitacao);
        void ManterBancoCadastroFornecedor(List<SolicitacaoModificacaoDadosBancario> bancos, int solicitacaoId);
    }

    public class SolicitacaoModificacaoBancoWebForLinkService : Service<SolicitacaoModificacaoDadosBancario>,
        ISolicitacaoModificacaoBancoWebForLinkService
    {
        private readonly ISolicitacaoBancoWebForLinkRepository _solicitacaoBancoRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public SolicitacaoModificacaoBancoWebForLinkService(
            ISolicitacaoBancoWebForLinkRepository solicitacaoBancoRepository,
            ISolicitacaoWebForLinkRepository solicitacaoRepository)
            : base(solicitacaoBancoRepository)
        {
            try
            {
                _solicitacaoBancoRepository = solicitacaoBancoRepository;
                _solicitacaoRepository = solicitacaoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SolicitacaoModificacaoDadosBancario BuscarPorId(int id)
        {
            try
            {
                return _solicitacaoBancoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public List<SolicitacaoModificacaoDadosBancario> ListarPorSolicitacaoId(int solicitacaoID)
        {
            try
            {
                return _solicitacaoBancoRepository.Find(x => x.SOLICITACAO_ID == solicitacaoID).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao buscar a lista de solicitações de modificação de banco por solicitação.", ex);
            }
        }

        public void InserirSolicitacao(SolicitacaoModificacaoDadosBancario solicitacao)
        {
            try
            {
                _solicitacaoBancoRepository.Add(solicitacao);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao inserir a solicitação de modificação de banco.", e);
            }
        }

        public void InserirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes)
        {
            try
            {
                _solicitacaoBancoRepository.Add(solicitacoes);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao inserir a solicitação de modificação de banco.", e);
            }
        }

        public void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosBancario solicitacao)
        {
            try
            {
                if (solicitacao.ID == 0)
                    _solicitacaoBancoRepository.Add(solicitacao);
                else
                    _solicitacaoBancoRepository.Update(solicitacao);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao inserir ou atualizar a solicitação de modificação de banco.", e);
            }
        }

        public void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes)
        {
            try
            {
                foreach (var solicitacao in solicitacoes)
                {
                    if (solicitacao.ID == 0)
                        _solicitacaoBancoRepository.Add(solicitacao);
                    else
                        _solicitacaoBancoRepository.Update(solicitacao);
                }
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao inserir ou atualizar a solicitação de modificação de banco.", e);
            }
        }

        public void ExcluirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes)
        {
            try
            {
                _solicitacaoBancoRepository.Delete(solicitacoes);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException(
                    "Ocorreu um erro ao excluir a solicitação de modificação de banco.", e);
            }
        }

        public void InserirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> bancos, SOLICITACAO solicitacao)
        {
            try
            {
                _solicitacaoRepository.Add(solicitacao);
                bancos.ForEach(x =>
                {
                    x.SOLICITACAO_ID = solicitacao.ID;
                    _solicitacaoBancoRepository.Add(x);
                });
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os bancos na solicitação.", e);
            }
        }

        public void ManterBancoCadastroFornecedor(List<SolicitacaoModificacaoDadosBancario> bancos, int solicitacaoId)
        {
            try
            {
                _solicitacaoBancoRepository.Delete(
                    _solicitacaoBancoRepository.All().Where(x => x.SOLICITACAO_ID == solicitacaoId).ToList());
                _solicitacaoBancoRepository.Add(bancos);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os bancos na solicitação.", e);
            }
        }

        public void Dispose()
        {
        }
    }
}