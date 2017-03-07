using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class PesquisarListaVM
    {
        public int? Pagina { get; set; }
        public string MensagemSucesso { get; set; }
    }

    public class CategoriaVM : PesquisarListaVM
    {
        public int ID { get; set; }

        public int? PaiId { get; set; }

        public int Contratante_ID { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Informe o Código da Categoria de Fornecedor")]
        public string Codigo { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a Descrição da Categoria de Fornecedor")]
        public string Descricao { get; set; }

        [Display(Name = "Categoria")]
        public string DescricaoCategoriaPai { get; set; }

        [Display(Name = "Isento de Documentos")]
        public bool IsentoDocumentos { get; set; }

        [Display(Name = "Isento de Dados Bancários")]
        public bool IsentoDadosBancarios { get; set; }

        [Display(Name = "Isento de Dados de Contato")]
        public bool IsentoDadosContato { get; set; }

        public bool TemFilhos { get; set; }

        public int? CH_ID { get; set; }

        public bool Ativo { get; set; }

        public int Nivel { get; set; }

        public int TotalNiveis { get; set; }

        public string UrlEditar { get; set; }

        public string UrlExcluir { get; set; }

        public string UrlNovaSubCategoria { get; set; }


        public static CategoriaVM ModelToViewModel(FORNECEDOR_CATEGORIA categoria, UrlHelper url)
        {
            var viewModel = Mapper.Map<CategoriaVM>(categoria);
            viewModel.Url = url;
            viewModel.Validar();
            return viewModel;
        }
        public static List<CategoriaVM> ModelToViewModel(List<FORNECEDOR_CATEGORIA> categorias, UrlHelper url)
        {
            var viewModel = Mapper.Map<List<CategoriaVM>>(categorias);
            foreach (var model in viewModel)
            {
                model.Url = url;
                model.Validar();
            }
            return viewModel;
        }



        public UrlHelper Url { get; set; }

        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            string key = "r10X310y";

            UrlEditar = Url.Action("CategoriaFrm", "Categoria", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Alterar", ID), key)
            });

            UrlExcluir = Url.Action("CategoriaFrm", "Categoria", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Excluir", ID), key)
            });

            UrlNovaSubCategoria = Url.Action("CategoriaFrm", "Categoria", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=NovaSubcategoria", ID), key)
            });
        }
    }
}