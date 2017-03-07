namespace WebForLink.Web.ViewModels
{
    public class UsuarioPapelVM
    {
        public int UsuarioModelId { get; set; }
        public int PapelModelId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public PerfilVM Papel { get; set; }
    }
}