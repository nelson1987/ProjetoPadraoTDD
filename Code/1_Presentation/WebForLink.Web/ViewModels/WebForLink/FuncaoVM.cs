namespace WebForLink.Web.ViewModels
{
    public class FuncaoVM
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pai { get; set; }
        public int AplicacaoId { get; set; }
        public AplicacaoVM Aplicacao { get; set; }
    }
}