using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Application.Interfaces
{
    public interface ICategoriaAppService : IAppService<Categoria>
    {
        IEnumerable<Categoria> PesquisarCategoria(string codigo, string descricao);

        DataTableResponse<Categoria> RespostaPesquisaCategoria(int draw, int comeco, int tamanho,
            Func<Categoria, IComparable> ordenacao, bool ascendente, string codigo, string descricao);

        Task<IEnumerable<Categoria>> AllAsync();
    }
}