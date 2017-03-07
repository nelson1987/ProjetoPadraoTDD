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
    public class SolicitacaoEnderecoWebForLinkService : Service<SOLICITACAO_MODIFICACAO_ENDERECO>,
        ISolicitacaoModificacaoEnderecoWebForLinkService
    {
        private readonly ISolicitacaoEnderecoWebForLinkRepository _solicitacaoEnderecoRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public SolicitacaoEnderecoWebForLinkService(
            ISolicitacaoWebForLinkRepository solicitacaoRepository,
            ISolicitacaoEnderecoWebForLinkRepository solicitacaoEnderecoRepository)
            : base(solicitacaoEnderecoRepository)
        {
            try
            {
                _solicitacaoRepository = solicitacaoRepository;
                _solicitacaoEnderecoRepository = solicitacaoEnderecoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO BuscarPorId(int id)
        {
            return _solicitacaoEnderecoRepository.Get(id);
        }

        public List<SOLICITACAO_MODIFICACAO_ENDERECO> ListarPorSolicitacaoId(int solicitacaoID)
        {
            return _solicitacaoEnderecoRepository.Find(x => x.SOLICITACAO_ID == solicitacaoID).ToList();
        }

        public void InserirSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacaoEndereco, SOLICITACAO solicitacao)
        {
            _solicitacaoEnderecoRepository.Add(solicitacaoEndereco);
        }

        public void InserirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes, SOLICITACAO solicitacao)
        {
            _solicitacaoRepository.Add(solicitacao);
            foreach (var item in solicitacoes)
            {
                item.SOLICITACAO_ID = solicitacao.ID;
                item.CONTRATANTE_ID = solicitacao.CONTRATANTE_ID;
                item.PJPF_ID = solicitacao.PJPF_ID;
                _solicitacaoEnderecoRepository.Add(item);
            }
        }

        public void InserirOuAtualizarSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao)
        {
            _solicitacaoEnderecoRepository.InserirOuAtualizar(solicitacao);
        }

        public void InserirOuAtualizarSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes)
        {
            foreach (var item in solicitacoes)
            {
                _solicitacaoEnderecoRepository.InserirOuAtualizar(item);
            }
            //Dispose();
        }

        public void ExcluirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes)
        {
            _solicitacaoEnderecoRepository.Delete(solicitacoes);
        }

        public void InserirOuAtualizar(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao)
        {
            _solicitacaoEnderecoRepository.InserirOuAtualizar(solicitacao);
        }

        public void Dispose()
        {
        }
    }
}