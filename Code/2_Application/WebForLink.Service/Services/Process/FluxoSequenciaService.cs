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
    public interface IFluxoSequenciaWebForLinkAppService : IAppService<FLUXO_SEQUENCIA>
    {
        bool NecessitaExecucaoManual(int contratanteId, int fluxoId, int papelId, int solicitacaoId);
    }

    public class FluxoSequenciaWebForLinkAppService : AppService<WebForLinkContexto>,
        IFluxoSequenciaWebForLinkAppService
    {
        private readonly IFluxoSequenciaWebForLinkService _fluxoSequenciaService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public FluxoSequenciaWebForLinkAppService(ISolicitacaoWebForLinkService solicitacao,
            IFluxoSequenciaWebForLinkService fluxoSequencia)
        {
            try
            {
                _solicitacaoService = solicitacao;
                _fluxoSequenciaService = fluxoSequencia;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public bool NecessitaExecucaoManual(int contratanteId, int fluxoId, int papelId, int solicitacaoId)
        {
            var grupoOrigemId = _solicitacaoService.Get(x => x.ID == solicitacaoId)
                .WFD_SOLICITACAO_TRAMITE.FirstOrDefault(t => t.PAPEL_ID == papelId
                                                             && t.SOLICITACAO_STATUS_ID == 1)
                .GRUPO_DESTINO;

            return _fluxoSequenciaService.Get(f => f.CONTRATANTE_ID == contratanteId
                                                   && f.FLUXO_ID == fluxoId
                                                   && f.PAPEL_ID_INI == papelId
                                                   && f.GRUPO_ORIGEM == grupoOrigemId)
                .EXECUCAO_MANUAL;
        }

        public void Dispose()
        {
        }

        public FLUXO_SEQUENCIA Get(int id, bool @readonly = false)
        {
            return _fluxoSequenciaService.Get(id, @readonly);
        }

        public FLUXO_SEQUENCIA Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FLUXO_SEQUENCIA GetAllReferences(int id, bool @readonly = false)
        {
            return _fluxoSequenciaService.GetAllReferences(id, @readonly);
        }

        public IEnumerable<FLUXO_SEQUENCIA> All(bool @readonly = false)
        {
            return _fluxoSequenciaService.All(@readonly);
        }

        public IEnumerable<FLUXO_SEQUENCIA> Find(Expression<Func<FLUXO_SEQUENCIA, bool>> predicate, bool @readonly = false)
        {
            return _fluxoSequenciaService.Find(predicate, @readonly);
        }

        public ValidationResult Create(FLUXO_SEQUENCIA entity)
        {
            return _fluxoSequenciaService.Add(entity);
        }

        public ValidationResult Update(FLUXO_SEQUENCIA entity)
        {
            return _fluxoSequenciaService.Update(entity);
        }

        public ValidationResult Remove(FLUXO_SEQUENCIA entity)
        {
            return _fluxoSequenciaService.Delete(entity);
        }
    }
}