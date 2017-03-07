using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IVisaoWebForLinkService
    {
        List<TIPO_VISAO> ListarTodos();
    }

    public class VisaoWebForLinkService : Service<TIPO_VISAO>, IVisaoWebForLinkService
    {
        private readonly ITipoVisaoWebForLinkRepository _visaoRepository;

        public VisaoWebForLinkService(ITipoVisaoWebForLinkRepository tipoVisao)
            : base(tipoVisao)
        {
            try
            {
                _visaoRepository = tipoVisao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<TIPO_VISAO> ListarTodos()
        {
            return _visaoRepository.All().ToList();
        }

        public void Dispose()
        {
        }
    }
}