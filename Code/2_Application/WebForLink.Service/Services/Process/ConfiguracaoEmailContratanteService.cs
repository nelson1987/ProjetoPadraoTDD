using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class ConfiguracaoEmailContratanteWebForLinkAppService : AppService<WebForLinkContexto>,
        IConfiguracaoEmailContratanteWebForLinkAppService
    {
        private readonly IContratanteConfiguracaoEmailWebForLinkService _configuracaoEmailContratante;

        public ConfiguracaoEmailContratanteWebForLinkAppService(
            IContratanteConfiguracaoEmailWebForLinkService configuracaoEmail)
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

        public void Dispose()
        {
        }
        public CONTRATANTE_CONFIGURACAO_EMAIL Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> Find(
            Expression<Func<CONTRATANTE_CONFIGURACAO_EMAIL, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(CONTRATANTE_CONFIGURACAO_EMAIL entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(CONTRATANTE_CONFIGURACAO_EMAIL entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(CONTRATANTE_CONFIGURACAO_EMAIL entity)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CONTRATANTE_CONFIGURACAO_EMAIL> Find(
            Expression<Func<CONTRATANTE_CONFIGURACAO_EMAIL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CONTRATANTE_CONFIGURACAO_EMAIL BuscarPorContratanteETipo(int contratanteId, int emailTipoId)
        {
            try
            {
                return _configuracaoEmailContratante.Get(x => x.CONTRATANTE_ID == contratanteId && x.EMAIL_TP_ID == 1);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar a configuração de email.", e);
            }
        }
    }
}