using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IContratanteConfiguracaoWebForLinkAppService : IAppService<CONTRATANTE_CONFIGURACAO>
    {
        CONTRATANTE_CONFIGURACAO BuscarPorID(int id);
        CONTRATANTE_CONFIGURACAO BuscarPorIdSolicitacao(int id);
        List<CONTRATANTE_CONFIGURACAO> ListarTodos();
        string BuscarPrazo(SOLICITACAO solicitacao);
    }

    public class ContratanteConfiguracaoWebForLinkAppService : AppService<WebForLinkContexto>,
        IContratanteConfiguracaoWebForLinkAppService
    {
        private readonly IContratanteConfiguracaoWebForLinkService _configuracaoContratante;
        private readonly ISolicitacaoWebForLinkService _solicitacao;

        public ContratanteConfiguracaoWebForLinkAppService(
            IContratanteConfiguracaoWebForLinkService configuracaoContratante,
            ISolicitacaoWebForLinkService solicitacao)
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

        public void Dispose()
        {
        }

        public CONTRATANTE_CONFIGURACAO BuscarPorID(int id)
        {
            try
            {
                return _configuracaoContratante.Get(x => x.CONTRATANTE_ID == id);
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

        public CONTRATANTE_CONFIGURACAO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO> Find(Expression<Func<CONTRATANTE_CONFIGURACAO, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(CONTRATANTE_CONFIGURACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(CONTRATANTE_CONFIGURACAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(CONTRATANTE_CONFIGURACAO entity)
        {
            throw new NotImplementedException();
        }

        private int BuscarPrazo(int idContratante)
        {
            try
            {
                return _configuracaoContratante.Get(x => x.CONTRATANTE_ID == idContratante).PRAZO_ENTREGA_FICHA;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar as configurações dos Contratantes", ex);
            }
        }
    }
}