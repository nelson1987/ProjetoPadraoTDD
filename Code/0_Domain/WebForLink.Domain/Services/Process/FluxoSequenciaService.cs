using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFluxoSequenciaWebForLinkService : IService<FLUXO_SEQUENCIA>
    {
        bool NecessitaExecucaoManual(int contratanteId, int fluxoId, int papelId, int solicitacaoId);
    }

    public class FluxoSequenciaWebForLinkService : Service<FLUXO_SEQUENCIA>, IFluxoSequenciaWebForLinkService
    {
        private readonly IFluxoSequenciaWebForLinkRepository _fluxoSequenciaRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public FluxoSequenciaWebForLinkService(ISolicitacaoWebForLinkRepository solicitacao,
            IFluxoSequenciaWebForLinkRepository fluxoSequencia) : base(fluxoSequencia)
        {
            try
            {
                _solicitacaoRepository = solicitacao;
                _fluxoSequenciaRepository = fluxoSequencia;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public bool NecessitaExecucaoManual(int contratanteId, int fluxoId, int papelId, int solicitacaoId)
        {
            var grupoOrigemId = _solicitacaoRepository.Find(x => x.ID == solicitacaoId).FirstOrDefault()
                .WFD_SOLICITACAO_TRAMITE.FirstOrDefault(t => t.PAPEL_ID == papelId
                                                             && t.SOLICITACAO_STATUS_ID == 1)
                .GRUPO_DESTINO;

            return _fluxoSequenciaRepository.Find(f => f.CONTRATANTE_ID == contratanteId
                                                       && f.FLUXO_ID == fluxoId
                                                       && f.PAPEL_ID_INI == papelId
                                                       && f.GRUPO_ORIGEM == grupoOrigemId).FirstOrDefault()
                .EXECUCAO_MANUAL;
        }

        public void Dispose()
        {
        }
    }
}