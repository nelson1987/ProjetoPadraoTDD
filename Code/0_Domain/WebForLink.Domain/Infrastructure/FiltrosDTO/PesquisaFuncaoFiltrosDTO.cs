namespace WebForLink.Domain.Infrastructure.FiltrosDTO
{
    public class PesquisaFuncaoFiltrosDTO
    {
        public int Aplicacao { get; set; }
        public string Codigo { get; set; }
        public int ContratanteUsuario { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public int? PaiFuncao { get; set; }
        public string Tela { get; set; }
    }
}