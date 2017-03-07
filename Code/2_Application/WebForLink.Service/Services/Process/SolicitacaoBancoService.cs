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
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoBancoWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoModificacaoBancoWebForLinkAppService
    {
        private readonly ISolicitacaoModificacaoBancoWebForLinkService _solicitacaoBancoService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public SolicitacaoBancoWebForLinkAppService(
            ISolicitacaoModificacaoBancoWebForLinkService solicitacaoBancoService,
            ISolicitacaoWebForLinkService solicitacaoService)
        {
            try
            {
                _solicitacaoBancoService = solicitacaoBancoService;
                _solicitacaoService = solicitacaoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public SolicitacaoModificacaoDadosBancario BuscarPorId(int id)
        {
            try
            {
                return _solicitacaoBancoService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public List<SolicitacaoModificacaoDadosBancario> ListarPorSolicitacaoId(int solicitacaoId)
        {
            try
            {
                return _solicitacaoBancoService.Find(x => x.SOLICITACAO_ID == solicitacaoId).ToList();
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
                _solicitacaoBancoService.Add(solicitacao);
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
                _solicitacaoBancoService.Add(solicitacoes);
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
                    _solicitacaoBancoService.Add(solicitacao);
                else
                    _solicitacaoBancoService.Update(solicitacao);
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
                        _solicitacaoBancoService.Add(solicitacao);
                    else
                        _solicitacaoBancoService.Update(solicitacao);
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
                _solicitacaoBancoService.Delete(solicitacoes);
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
                _solicitacaoService.Add(solicitacao);
                bancos.ForEach(x =>
                {
                    x.SOLICITACAO_ID = solicitacao.ID;
                    _solicitacaoBancoService.Add(x);
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
                BeginTransaction();
                _solicitacaoBancoService.Delete(
                    _solicitacaoBancoService.All().Where(x => x.SOLICITACAO_ID == solicitacaoId).ToList());
                _solicitacaoBancoService.Add(bancos);
                Commit();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os bancos na solicitação.", e);
            }
        }

        public SolicitacaoModificacaoDadosBancario Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosBancario Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosBancario GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosBancario> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosBancario> Find(
            Expression<Func<SolicitacaoModificacaoDadosBancario, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SolicitacaoModificacaoDadosBancario entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SolicitacaoModificacaoDadosBancario entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SolicitacaoModificacaoDadosBancario entity)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosBancario Get(int id)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosBancario Get(
            Expression<Func<SolicitacaoModificacaoDadosBancario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosBancario> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosBancario> Find(
            Expression<Func<SolicitacaoModificacaoDadosBancario, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}