using System;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Common;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Service.Process
{
    public class TipoUfWebForLinkService : Service<TIPO_UF>, IUfWebForLinkService
    {
        public TipoUfWebForLinkService()
        {
            try
            {
                if (Processo == null)
                    Processo = new UnitOfWork(new WebForLinkContexto());
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// Buscar Estado por Sigla
        /// </summary>
        /// <param name="sigla">Sigla do Estado</param>
        /// <returns>T_UF</returns>
        public TIPO_UF BuscarPorID(string sigla)
        {
            try
            {
                return Processo.Uf.Get(sigla);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }
    }
}

