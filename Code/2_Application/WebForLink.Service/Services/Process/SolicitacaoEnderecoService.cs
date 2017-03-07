using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoModificacaoEnderecoWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoModificacaoEnderecoWebForLinkAppService
    {
        private readonly ISolicitacaoModificacaoEnderecoWebForLinkService _solicitacaoEnderecoService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public SolicitacaoModificacaoEnderecoWebForLinkAppService(ISolicitacaoWebForLinkService solicitacaoService,
            ISolicitacaoModificacaoEnderecoWebForLinkService solicitacaoEnderecoService)
        {
            try
            {
                _solicitacaoService = solicitacaoService;
                _solicitacaoEnderecoService = solicitacaoEnderecoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO BuscarPorId(int id)
        {
            return _solicitacaoEnderecoService.Get(id);
        }

        public List<SOLICITACAO_MODIFICACAO_ENDERECO> ListarPorSolicitacaoId(int solicitacaoId)
        {
            return _solicitacaoEnderecoService.Find(x => x.SOLICITACAO_ID == solicitacaoId).ToList();
        }

        public void InserirSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacaoEndereco, SOLICITACAO solicitacao)
        {
            _solicitacaoEnderecoService.Add(solicitacaoEndereco);
        }

        public void InserirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes, SOLICITACAO solicitacao)
        {
            _solicitacaoService.Add(solicitacao);
            foreach (var item in solicitacoes)
            {
                item.SOLICITACAO_ID = solicitacao.ID;
                item.CONTRATANTE_ID = solicitacao.CONTRATANTE_ID;
                item.PJPF_ID = solicitacao.PJPF_ID;
                _solicitacaoEnderecoService.Add(item);
            }
        }

        public void InserirOuAtualizarSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao)
        {
            _solicitacaoEnderecoService.InserirOuAtualizar(solicitacao);
        }

        public void InserirOuAtualizarSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes)
        {
            foreach (var item in solicitacoes)
            {
                _solicitacaoEnderecoService.InserirOuAtualizar(item);
            }
            //Dispose();
        }

        public void ExcluirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes)
        {
            _solicitacaoEnderecoService.Delete(solicitacoes);
        }

        public void Dispose()
        {
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MODIFICACAO_ENDERECO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MODIFICACAO_ENDERECO> Find(
            Expression<Func<SOLICITACAO_MODIFICACAO_ENDERECO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SOLICITACAO_MODIFICACAO_ENDERECO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SOLICITACAO_MODIFICACAO_ENDERECO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SOLICITACAO_MODIFICACAO_ENDERECO entity)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO Get(int id)
        {
            throw new NotImplementedException();
        }

        public SOLICITACAO_MODIFICACAO_ENDERECO Get(Expression<Func<SOLICITACAO_MODIFICACAO_ENDERECO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MODIFICACAO_ENDERECO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SOLICITACAO_MODIFICACAO_ENDERECO> Find(
            Expression<Func<SOLICITACAO_MODIFICACAO_ENDERECO, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}