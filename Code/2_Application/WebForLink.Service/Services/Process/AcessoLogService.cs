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
    public class AcessoLogWebForLinkAppService : AppService<WebForLinkContexto>, IAcessoLogWebForLinkAppService
    {
        private readonly IAcessoLogWebForLinkService _acessoLog;

        public AcessoLogWebForLinkAppService(IAcessoLogWebForLinkService acessoLog)
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

        public IEnumerable<WAC_ACESSO_LOG> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WAC_ACESSO_LOG> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(WAC_ACESSO_LOG entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Commit();
        }

        public IEnumerable<WAC_ACESSO_LOG> Find(Expression<Func<WAC_ACESSO_LOG, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WAC_ACESSO_LOG> Find(Expression<Func<WAC_ACESSO_LOG, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public WAC_ACESSO_LOG Get(Expression<Func<WAC_ACESSO_LOG, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public WAC_ACESSO_LOG Get(int id)
        {
            throw new NotImplementedException();
        }

        public WAC_ACESSO_LOG Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public WAC_ACESSO_LOG Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public WAC_ACESSO_LOG GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public void GravarLogAcesso(int usuarioId, string ip, string navegador)
        {
            try
            {
                _acessoLog.GravarLogAcesso(usuarioId, ip, navegador);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao gravar o log de acesso", ex);
            }
        }

        public ValidationResult Remove(WAC_ACESSO_LOG entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(WAC_ACESSO_LOG entity)
        {
            throw new NotImplementedException();
        }
    }
}