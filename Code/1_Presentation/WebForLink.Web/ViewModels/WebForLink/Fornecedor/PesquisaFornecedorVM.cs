using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.ViewModels.Fornecedores
{
    public class PesquisaFornecedorVM
    {
        public PesquisaFornecedorVM()
        {
            Filtro = new FiltroPesquisaFornecedorVM();
            Grid = new List<ListaPesquisaFornecedorVM>();
        }
        public FiltroPesquisaFornecedorVM Filtro { get; set; }
        public List<ListaPesquisaFornecedorVM> Grid { get; set; }
    }
    public class FiltroPesquisaFornecedorVM
    {
        public FiltroPesquisaFornecedorVM()
        {
            Categorias = new List<CategoriaVM>();
            Paginacao = new PaginacaoModel();
        }
        public int? Pagina { get; set; }
        public PaginacaoModel Paginacao { get; set; }
        public string MensagemSucesso { get; set; }
        public int HiddenFornecedorId { get; set; }
        public int? CategoriaSelecionada { get; set; }
        public string CategoriaSelecionadaNome { get; set; }
        public string MensagemError { get; set; }
        [Display(Name = "Nome/Razão Social/Nome Fantasia")]
        public string Fornecedor { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        [Display(Name = "Categoria (Grupo Contas)")]
        public List<CategoriaVM> Categorias { get; set; }
        [Display(Name = "Empresa")]
        public List<SelectListItem> Empresas { get; set; }
    }
    public class ListaPesquisaFornecedorVM
    {
        [Display(Name = " ")]
        public int Id { get; set; }
        [Display(Name = "Código")]
        public string CodigoERP { get; set; }
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }
        [Display(Name = "Nome da Empresa")]
        public string NomeEmpresa { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Ação")]
        public string Acao { get; set; }
        public int ContratanteId { get; set; }
        public int FornecedorId { get; set; }
        public string UrlEditar { get; set; }
        public string UrlExcluir { get; set; }
    }
}