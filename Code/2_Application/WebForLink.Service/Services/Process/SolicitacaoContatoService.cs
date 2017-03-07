using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoContatoWebForLinkAppService : AppService<WebForLinkContexto>,
        ISolicitacaoModificacaoContatoWebForLinkAppService
    {
        private readonly IContratanteConfiguracaoWebForLinkService _contratanteConfig;
        private readonly IFluxoWebForLinkService _fluxoService;
        private readonly ISolicitacaoModificacaoContatoWebForLinkService _solicitacaoContatoService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public SolicitacaoContatoWebForLinkAppService(
            IFluxoWebForLinkService fluxoService,
            ISolicitacaoWebForLinkService solicitacaoService,
            ISolicitacaoModificacaoContatoWebForLinkService solicitacaoContatoService,
            IContratanteConfiguracaoWebForLinkService contratanteConfig)
        {
            _fluxoService = fluxoService;
            _solicitacaoService = solicitacaoService;
            _solicitacaoContatoService = solicitacaoContatoService;
            _contratanteConfig = contratanteConfig;
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SolicitacaoModificacaoDadosContato BuscarPorId(int id)
        {
            return _solicitacaoContatoService.BuscarPorId(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public List<SolicitacaoModificacaoDadosContato> ListarPorSolicitacaoId(int solicitacaoId)
        {
            return _solicitacaoContatoService.ListarPorSolicitacaoId(solicitacaoId);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void InserirSolicitacao(SolicitacaoModificacaoDadosContato solicitacao)
        {
            _solicitacaoContatoService.InserirSolicitacao(solicitacao);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacoes"></param>
        public void InserirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes)
        {
            _solicitacaoContatoService.InserirSolicitacoes(solicitacoes);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosContato solicitacao)
        {
            _solicitacaoContatoService.InserirOuAtualizar(solicitacao);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacoes"></param>
        public void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes)
        {
            foreach (var item in solicitacoes)
            {
                _solicitacaoContatoService.InserirOuAtualizar(item);
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
                _solicitacaoContatoService.Excluir(item);
            }
        }

        public SOLICITACAO IncluirSolicitacao(int contratanteId, int fornecedorId, int usuarioId, int tipoFluxoId)
        {
            var fluxo = _fluxoService.BuscarPorTipoEContratante(tipoFluxoId, contratanteId).ID; // Bloqueio
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
            _solicitacaoService.Add(solicitacaoExu);
            return solicitacaoExu;
        }

        public void ManterContatoCadastroFornecedor(List<SolicitacaoModificacaoDadosContato> contatos, int solicitacaoId)
        {
            try
            {
                _solicitacaoContatoService.Delete(
                    _solicitacaoContatoService.Find(x => x.SOLICITACAO_ID == solicitacaoId).ToList());
                _solicitacaoContatoService.Add(contatos);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os contatos na solicitação.", e);
            }
        }

        public SolicitacaoModificacaoDadosContato Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosContato Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosContato GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosContato> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosContato> Find(
            Expression<Func<SolicitacaoModificacaoDadosContato, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SolicitacaoModificacaoDadosContato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SolicitacaoModificacaoDadosContato entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SolicitacaoModificacaoDadosContato entity)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosContato Get(int id)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoModificacaoDadosContato Get(
            Expression<Func<SolicitacaoModificacaoDadosContato, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosContato> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoModificacaoDadosContato> Find(
            Expression<Func<SolicitacaoModificacaoDadosContato, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}