using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebForLink.Domain.Entities;

namespace WebForLink.Web.ViewModels
{
    public class CategoriaVM
    {
        [UIHint("DatePicker")]
        public DateTime Data { get; set; }

        public Categoria ToModel
        {
            get { return Mapper.Map<Categoria>(this); }
        }
    }

    public class CategoriaPesquisaVM
    {
        [Display(Name = "Código")] //, ResourceType = typeof(Resources.Language))]
        public string Codigo { get; set; }

        [Display(Name = "Descrição")] //, ResourceType = typeof(Resources.Language))]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")] //, ResourceType = typeof(Resources.Language))]
        public bool Ativo { get; set; }

        [Display(Name = "Ação")] //, ResourceType = typeof(Resources.Language))]
        public bool Acao { get; set; }
    }

    public class GridPesquisaCategoriaVM
    {
        public GridPesquisaCategoriaVM(int id, string descricao, string codigo, bool ativo)
        {
            Id = id.ToString();
            Codigo = codigo;
            Descricao = descricao;
            Ativo = ativo ? "Ativo" : "Inativo";
        }

        public string Id { get; private set; }

        [Display(Name = "Código")] //, ResourceType = typeof(Resources.Language))]
        public string Codigo { get; private set; }

        [Display(Name = "Descrição")] //, ResourceType = typeof(Resources.Language))]
        public string Descricao { get; private set; }

        public string Ativo { get; private set; }
        public string urlSubCategoria { get; set; }
        public string urlEditar { get; set; }
        public string urlExcluir { get; set; }

        public static GridPesquisaCategoriaVM ToViewModel(Categoria categoria)
        {
            return Mapper.Map<GridPesquisaCategoriaVM>(categoria);
        }

        public static ICollection<GridPesquisaCategoriaVM> ToViewModel(IEnumerable<Categoria> genre)
        {
            return Mapper.Map<ICollection<GridPesquisaCategoriaVM>>(genre);
        }
    }
}