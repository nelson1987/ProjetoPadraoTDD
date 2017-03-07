using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoUnspscService
    {
        SOLICITACAO_UNSPSC BuscarPorID(int id);
        void ManterUnspscSolicitacao(List<SOLICITACAO_UNSPSC> unscpsc, int idSolicitacao);
        void InserirSolicitacoes(List<SOLICITACAO_UNSPSC> unscpsc);
    }

    public class SolicitacaoUnspscWebForLinkService : Service<SOLICITACAO_UNSPSC>, ISolicitacaoUnspscService
    {
        private readonly IConfiguracaoWebForLinkRepository _configuracao;
        private readonly ISolicitacaoServicoMaterialWebForLinkRepository _unspscSolicitacao;

        public SolicitacaoUnspscWebForLinkService(
            ISolicitacaoServicoMaterialWebForLinkRepository solUnspsc,
            IConfiguracaoWebForLinkRepository configuracao) : base(solUnspsc)
        {
            try
            {
                _configuracao = configuracao;
                _unspscSolicitacao = solUnspsc;
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
        public SOLICITACAO_UNSPSC BuscarPorID(int id)
        {
            try
            {
                return _unspscSolicitacao.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(
                    "Erro ao buscar uma solicitacao de cadastro de Materias de serviço por ID", ex);
            }
        }

        public void ManterUnspscSolicitacao(List<SOLICITACAO_UNSPSC> unscpsc, int idSolicitacao)
        {
            try
            {
                foreach (var item in _unspscSolicitacao.BuscarPorSolicitacaoId(idSolicitacao))
                    _unspscSolicitacao.Delete(item);
                foreach (var item in unscpsc)
                    _unspscSolicitacao.Add(item);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar atualizar o UNSPSC.", ex);
            }
        }

        public void InserirSolicitacoes(List<SOLICITACAO_UNSPSC> unscpsc)
        {
            try
            {
                foreach (var item in unscpsc)
                    _unspscSolicitacao.Add(item);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar inserir a solicitacao de modificação de Unspsc.",
                    ex);
            }
        }

        public void Dispose()
        {
        }
    }
}