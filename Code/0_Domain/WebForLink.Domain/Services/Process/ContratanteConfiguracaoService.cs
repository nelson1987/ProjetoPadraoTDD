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
    public interface IContratanteConfiguracaoWebForLinkService : IService<CONTRATANTE_CONFIGURACAO>
    {
        CONTRATANTE_CONFIGURACAO BuscarPorID(int id);
        CONTRATANTE_CONFIGURACAO BuscarPorIdSolicitacao(int id);
        List<CONTRATANTE_CONFIGURACAO> ListarTodos();
        string BuscarPrazo(SOLICITACAO solicitacao);
    }

    public class ContratanteConfiguracaoWebForLinkService : Service<CONTRATANTE_CONFIGURACAO>,
        IContratanteConfiguracaoWebForLinkService
    {
        private readonly IContratanteConfiguracaoWebForLinkRepository _configuracaoContratante;
        private readonly ISolicitacaoWebForLinkRepository _solicitacao;

        public ContratanteConfiguracaoWebForLinkService(
            IContratanteConfiguracaoWebForLinkRepository configuracaoContratante,
            ISolicitacaoWebForLinkRepository solicitacao)
            : base(configuracaoContratante)
        {
            try
            {
                _configuracaoContratante = configuracaoContratante;
                _solicitacao = solicitacao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public CONTRATANTE_CONFIGURACAO BuscarPorID(int id)
        {
            try
            {
                return _configuracaoContratante.Find(x => x.CONTRATANTE_ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar as configurações do Contratante por ID", ex);
            }
        }

        public CONTRATANTE_CONFIGURACAO BuscarPorIdSolicitacao(int id)
        {
            try
            {
                return _solicitacao.Get(id).Contratante.WFD_CONTRATANTE_CONFIG;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar as configurações do Contratante por ID", ex);
            }
        }

        public List<CONTRATANTE_CONFIGURACAO> ListarTodos()
        {
            try
            {
                return _configuracaoContratante.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar as configurações dos Contratantes", ex);
            }
        }

        public string BuscarPrazo(SOLICITACAO solicitacao)
        {
            if (solicitacao != null)
            {
                var dias = BuscarPrazo(solicitacao.CONTRATANTE_ID);
                var criacao = DateTime.MinValue;
                var datafinal = DateTime.MinValue;

                if (solicitacao.DT_PRORROGACAO_PRAZO != null)
                {
                    criacao = (DateTime) solicitacao.DT_PRORROGACAO_PRAZO;
                    datafinal = criacao;
                }
                else
                {
                    criacao = solicitacao.SOLICITACAO_DT_CRIA;
                    datafinal = criacao.AddDays(dias);
                }

                var hoje = datafinal.Subtract(DateTime.Now);
                return string.Format("{0:dd/MM/yyyy}", datafinal);
            }
            return null;
        }

        public void Dispose()
        {
        }

        private int BuscarPrazo(int idContratante)
        {
            try
            {
                return
                    _configuracaoContratante.Find(x => x.CONTRATANTE_ID == idContratante)
                        .FirstOrDefault()
                        .PRAZO_ENTREGA_FICHA;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar as configurações dos Contratantes", ex);
            }
        }
    }
}