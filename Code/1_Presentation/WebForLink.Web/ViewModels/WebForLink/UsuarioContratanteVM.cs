namespace WebForLink.Web.ViewModels
{
    public class UsuarioContratanteVM
    {
        public int UsuarioModelId { get; set; }
        public int ContratanteModelId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public ContratanteVM Contratante { get; set; }
    }
}