using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class MeusDocumentosWebForLinkAppService : AppService<WebForLinkContexto>,
        IMeusDocumentosWebForLinkAppService
    {
        private readonly ICompartilhamentoWebForLinkService _compartilhamentoService;

        public MeusDocumentosWebForLinkAppService(ICompartilhamentoWebForLinkService compartilhamentos)
        {
            try
            {
                _compartilhamentoService = compartilhamentos;
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
        /// <param name="id"></param>
        /// <returns></returns>
        public Compartilhamentos BuscarPorID(int id)
        {
            try
            {
                return _compartilhamentoService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        //public RetornoPesquisa<Compartilhamentos> BuscarPesquisa(Expression<Func<Compartilhamentos, bool>> filtros, int tamanhoPagina, int pagina, Func<Compartilhamentos, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        return _compartilhamentoService.Pesquisar(filtros, tamanhoPagina, pagina, ordenacao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
        //    }
        //}

        public Compartilhamentos Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Compartilhamentos Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Compartilhamentos GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Compartilhamentos> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Compartilhamentos> Find(Expression<Func<Compartilhamentos, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Compartilhamentos entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Compartilhamentos entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Compartilhamentos entity)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<Compartilhamentos> BuscarPesquisa(
            Expression<Func<Compartilhamentos, bool>> filtros, int tamanhoPagina, int pagina,
            Func<Compartilhamentos, IComparable> ordenacao)
        {
            return _compartilhamentoService.BuscarPesquisa(filtros, tamanhoPagina, pagina, ordenacao);
        }

        public RetornoPesquisa<Compartilhamentos> BuscarPesquisaInvertido(Expression<Func<Compartilhamentos, bool>> filtros, int tamanhoPagina, int pagina, Func<Compartilhamentos, IComparable> ordenacao)
        {
            return _compartilhamentoService.BuscarPesquisaInvertido(filtros, tamanhoPagina, pagina, ordenacao);
        }
    }
}