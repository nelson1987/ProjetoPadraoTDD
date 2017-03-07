using System.Collections.Generic;
using System.ComponentModel;

namespace WebForLink.Web.ViewModels
{
    public class PreCadastroListaVM
    {
        public PreCadastroListaVM()
        {
            FiltroPesquisa = new PreCadastroFiltrosPesquisaVM();
            ListaGrid = new List<PreCadastroGridPesquisaVM>();
        }
        public PreCadastroListaVM(string erro, string sucesso)
        {
            FiltroPesquisa = new PreCadastroFiltrosPesquisaVM() {  };
            ListaGrid = new List<PreCadastroGridPesquisaVM>();
            MensagemError = erro;
            MensagemSucesso = sucesso;
            Pagina = 1;
        }
        public int? Pagina { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemError { get; set; }
        public int? CategoriaSelecionada { get; set; }
        public string CategoriaSelecionadaNome { get; set; }
        public int Empresa { get; set; }
        public PreCadastroFiltrosPesquisaVM FiltroPesquisa { get; set; }
        public List<PreCadastroGridPesquisaVM> ListaGrid { get; set; }
    }
    public class PreCadastroFiltrosPesquisaVM
    {
        public string Status { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public int GrupoId { get; set; }
        public int ContratanteId { get; set; }
    }
    public class PreCadastroGridPesquisaVM
    {
        public int Id { get; set; }
        public int? Codigo { get; set; }
        [DisplayName("Nome/Razão Social/Nome Fantasia")]
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Empresa { get; set; }
        public string Status { get; set; }
        public string Acao { get; set; }
        public string UrlEditar { get; set; }
    }
    public class PreCadastroEdicaoVM
    {
        public int Id { get; set; }
    }
}