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
    public class RoboLogWebForLinkAppService : AppService<WebForLinkContexto>, IRoboLogWebForLinkAppService
    {
        private readonly ILogRoboWebForLinkService _logRoboService;

        public RoboLogWebForLinkAppService(ILogRoboWebForLinkService logRoboService)
        {
            _logRoboService = logRoboService;
            try
            {
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public ROBO_LOG Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ROBO_LOG Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ROBO_LOG GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO_LOG> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO_LOG> Find(Expression<Func<ROBO_LOG, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(ROBO_LOG entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(ROBO_LOG entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(ROBO_LOG entity)
        {
            throw new NotImplementedException();
        }

        public ROBO_LOG Get(int id)
        {
            throw new NotImplementedException();
        }

        public ROBO_LOG Get(Expression<Func<ROBO_LOG, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO_LOG> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO_LOG> Find(Expression<Func<ROBO_LOG, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Inserir(ROBO_LOG entidade)
        {
            try
            {
                _logRoboService.Add(entidade);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um log do robô de fornecedor por ID", ex);
            }
        }
    }
}