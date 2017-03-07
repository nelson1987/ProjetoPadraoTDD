using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class ConfiguracaoEmailContratanteWebForLinkService : Service<CONTRATANTE_CONFIGURACAO_EMAIL>,
        IConfiguracaoEmailContratanteWebForLinkService
    {
        private readonly IContratanteConfiguracaoEmailWebForLinkRepository _configuracaoEmailContratante;

        public ConfiguracaoEmailContratanteWebForLinkService(
            IContratanteConfiguracaoEmailWebForLinkRepository configuracaoEmail) : base(configuracaoEmail)
        {
            try
            {
                _configuracaoEmailContratante = configuracaoEmail;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <param name="emailTipoId"></param>
        /// <returns></returns>
        public CONTRATANTE_CONFIGURACAO_EMAIL BuscarPorContratanteETipo(int contratanteId, int emailTipoId)
        {
            try
            {
                return _configuracaoEmailContratante.BuscarPorContratanteETipo(contratanteId, 1);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar a configuração de email.", e);
            }
        }

        public void Dispose()
        {
        }
    }
}