using System.Collections.Generic;

namespace WebForLink.Web.ViewModels.PadraoVMs
{
    public class PesquisaVM<TEntity> where TEntity : class
    {
        public PesquisaVM()
        {
            Filtro = new FiltroPesquisaVM<TEntity>();
            Grid = new List<ListaPesquisaVM<TEntity>>();
        }
        public FiltroPesquisaVM<TEntity> Filtro { get; set; }
        public List<ListaPesquisaVM<TEntity>> Grid { get; set; }
    }
    public class FiltroPesquisaVM<TEntity> where TEntity : class
    {
        public int? Pagina { get; set; }
        public string MensagemSucesso { get; set; }
        public TEntity Entidade { get; set; }
    }
    public class ListaPesquisaVM<TEntity> where TEntity : class
    {
        public TEntity Entidade { get; set; }
        public string UrlEditar { get; set; }
        public string UrlExcluir { get; set; }
    }
}