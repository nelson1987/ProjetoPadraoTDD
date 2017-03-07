using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using AutoMapper;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.ViewModels.Novo
{
    public class PesquisaCategoriaVM
    {
        public PesquisaCategoriaVM()
        {
            Filtro = new FiltroPesquisaCategoriaVM();
            Grid = new List<ListaPesquisaCategoriaVM>();
        }
        public FiltroPesquisaCategoriaVM Filtro { get; set; }
        public List<ListaPesquisaCategoriaVM> Grid { get; set; }
        public string MensagemError { get; internal set; }
        public string MensagemSucesso { get; internal set; }
    }
    public class FiltroPesquisaCategoriaVM
    {
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public string Ativo { get; set; }
        public int? Pagina { get; set; }
        public string MensagemSucesso { get; set; }
        public int? TotalNiveis { get; set; }
        public PaginacaoModel Paginacao { get; internal set; }
    }
    public class ListaPesquisaCategoriaVM
    {
        public int Id { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public string Ativo { get; set; }
        public bool TemFilhos { get; set; }
        public string UrlEditar { get; set; }
        public string UrlExcluir { get; set; }
        public string UrlNovaSubCategoria { get; set; }
        public UrlHelper Url { get; set; }
        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            string key = "r10X310y";
            //Url = new UrlHelper("http");

            UrlEditar = Url.Action("CategoriaFrm", "Categoria", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Alterar", Id), key)
            });

            UrlExcluir = Url.Action("CategoriaFrm", "Categoria", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Excluir", Id), key)
            });

            UrlNovaSubCategoria = Url.Action("CategoriaFrm", "Categoria", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=NovaSubcategoria", Id), key)
            });
        }

        public static List<ListaPesquisaCategoriaVM> ModelToViewModel(IList<FORNECEDOR_CATEGORIA> list, UrlHelper url)
        {
            List<ListaPesquisaCategoriaVM> viewModel = Mapper.Map<List<ListaPesquisaCategoriaVM>>(list);
            foreach (var model in viewModel)
            {
                model.Url = url;
                model.Validar();
            }
            return viewModel;
        }
    }
}