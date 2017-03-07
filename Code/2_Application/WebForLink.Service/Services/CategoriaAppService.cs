using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;
using System.Threading.Tasks;

namespace WebForLink.Application.Services
{
    public class CategoriaAppService : AppService<MusicStoreContext>, ICategoriaAppService
    {
        private readonly ICategoriaService _service;

        private readonly List<Categoria> ListaCategoria = new List<Categoria>
        {
            new Categoria(1, "Clínica", "CATTX", true),
            new Categoria(2, "Concessionária", "CAT2", true),
            new Categoria(3, "Entidade / Instituição", "CATI", true)
        };

        public CategoriaAppService(ICategoriaService albumService)
        {
            _service = albumService;
        }

        public DataTableResponse<Categoria> RespostaPesquisaCategoria(int draw, int comeco, int tamanho,
            Func<Categoria, IComparable> ordenacao, bool ascendente, string codigo, string descricao)
        {
            var predicate = PredicateBuilder.New<Categoria>();
            predicate = predicate.And(x => x.Ativo);

            if (!string.IsNullOrEmpty(codigo))
                predicate = predicate.And(x => x.Codigo.ToUpper().Contains(codigo.ToUpper()));

            if (!string.IsNullOrEmpty(descricao))
                predicate = predicate.And(x => x.Descricao.ToUpper().Contains(descricao.ToUpper()));

            var totalDados = _service.All(true);

            IOrderedEnumerable<Categoria> filtrados;
            if (ascendente)
                filtrados = totalDados
                    .Where(predicate)
                    .OrderByDescending(ordenacao);
            else
                filtrados = totalDados
                    .Where(predicate)
                    .OrderBy(ordenacao);

            var displayedCompanies = filtrados
                .Skip(comeco)
                .Take(tamanho);

            return new DataTableResponse<Categoria>(draw, totalDados.Count(), filtrados.Count(), displayedCompanies);
        }

        public IEnumerable<Categoria> PesquisarCategoria(string codigo, string descricao)
        {
            var predicate = PredicateBuilder.New<Categoria>();
            predicate = predicate.And(x => x.Ativo);

            if (!string.IsNullOrEmpty(codigo))
                predicate = predicate.And(x => x.Codigo == codigo);

            if (!string.IsNullOrEmpty(descricao))
                predicate = predicate.And(x => x.Descricao == descricao);

            return _service.All(true).Where(predicate).ToList();

            //return ListaCategoria.Where(predicate).ToList();
        }

        public Categoria Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> All(bool @readonly = false)
        {
            return _service.All(@readonly);
        }

        public async Task<IEnumerable<Categoria>> AllAsync()
        {
            return await Task.Run(() => All(true));
        }

        public IEnumerable<Categoria> Find(Expression<Func<Categoria, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Categoria orderDetail)
        {
            BeginTransaction();
            ValidationResult.Add(_service.Add(orderDetail));
            if (ValidationResult.EstaValidado)
                Commit();

            return ValidationResult;
        }

        public ValidationResult Update(Categoria orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Categoria orderDetail)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Adicionar(Categoria categoria)
        {
            try
            {
                ListaCategoria.Add(categoria);
            }
            catch (Exception ex)
            {
                //throw new ServiceException("Erro ao tentar adicionar uma categoria.", ex);
            }
        }
    }
}