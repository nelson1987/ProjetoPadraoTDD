using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class AcessoLogWebForLinkService : Service<WAC_ACESSO_LOG>, IAcessoLogWebForLinkService
    {
        private readonly IAcessoLogWebForLinkRepository _acessoLog;

        public AcessoLogWebForLinkService(IAcessoLogWebForLinkRepository acessoLog) : base(acessoLog)
        {
            try
            {
                _acessoLog = acessoLog;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void GravarLogAcesso(int usuarioId, string ip, string navegador)
        {
            try
            {
                var log = new WAC_ACESSO_LOG
                {
                    DATA = DateTime.Now,
                    IP = ip,
                    NAVEGADOR = navegador,
                    USUARIO_ID = usuarioId
                };
                _acessoLog.Add(log);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao gravar o log de acesso", ex);
            }
        }
    }
}