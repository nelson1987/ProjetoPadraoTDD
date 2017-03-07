namespace WebForLink.Web.ViewModels.Adesao
{
    public class AdesaoPropriedadePlanoVM
    {
        public AdesaoPropriedadePlanoVM()
        {
        }

        public AdesaoPropriedadePlanoVM(bool valido, string descricao)
        {
            Valido = valido;
            Descricao = descricao;
        }
        public int Id { get; set; }
        public bool Valido { get; set; }
        public string Descricao { get; set; }
    }
}