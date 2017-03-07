namespace WebForLink.Web.ViewModels
{
    public class UsuarioPerfilVM
    {
        public int UsuarioModelId { get; set; }
        public int PerfilModelId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public PerfilVM Perfil { get; set; }
    }
}