using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VisaoWebForLinkAppService : AppService<WebForLinkContexto>, IVisaoWebForLinkAppService
    {
        private readonly ITipoVisaoWebForLinkService _visaoService;

        public VisaoWebForLinkAppService(ITipoVisaoWebForLinkService tipoVisao)
        {
            try
            {
                _visaoService = tipoVisao;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public List<TIPO_VISAO> ListarTodos()
        {
            return _visaoService.All(true).ToList();
        }

        public TIPO_VISAO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public TIPO_VISAO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public TIPO_VISAO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TIPO_VISAO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TIPO_VISAO> Find(Expression<Func<TIPO_VISAO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(TIPO_VISAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(TIPO_VISAO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(TIPO_VISAO entity)
        {
            throw new NotImplementedException();
        }

        public TIPO_VISAO Get(int id)
        {
            throw new NotImplementedException();
        }

        public TIPO_VISAO Get(Expression<Func<TIPO_VISAO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TIPO_VISAO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TIPO_VISAO> Find(Expression<Func<TIPO_VISAO, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}