namespace WebForLink.Web.ViewModels
{
    public class PerfilFuncaoVM
    {
        public int PerfilModelId { get; set; }
        public int FuncaoModelId { get; set; }
        public PerfilVM Perfil { get; set; }
        public FuncaoVM Funcao { get; set; }
    }
}