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
    public interface IFluxoWebForLinkAppService : IAppService<Fluxo>
    {
        Fluxo BuscarPorTipoEContratante(int tipoFluxoId, int contratanteId);
        List<Fluxo> ListarPorContratanteId(int contratanteId);
    }

    public class FluxoWebForLinkAppService : AppService<WebForLinkContexto>, IFluxoWebForLinkAppService
    {
        private readonly IFluxoWebForLinkService _fluxoService;

        public FluxoWebForLinkAppService(IFluxoWebForLinkService fluxoService)
        {
            try
            {
                _fluxoService = fluxoService;
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
        /// <param name="tipoFluxoId"></param>
        /// <param name="contratanteId"></param>
        /// <returns></returns>
        public Fluxo BuscarPorTipoEContratante(int tipoFluxoId, int contratanteId)
        {
            try
            {
                return _fluxoService.BuscarPorTipoEContratante(tipoFluxoId, contratanteId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }

        public List<Fluxo> ListarPorContratanteId(int contratanteId)
        {
            try
            {
                Func<Fluxo, IComparable> ordenacao = (Fluxo a) => a.FLUXO_NM;
                return _fluxoService.All().Where(x => x.CONTRATANTE_ID == contratanteId).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um Fluxo", ex);
            }
        }

        public Fluxo Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Fluxo Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Fluxo GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fluxo> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fluxo> Find(Expression<Func<Fluxo, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Fluxo entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Fluxo entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Fluxo entity)
        {
            throw new NotImplementedException();
        }
    }
}