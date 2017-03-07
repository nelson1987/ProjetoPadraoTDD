namespace WebForLink.Domain.Infrastructure.FiltrosDTO
{
    public class GerenciarContasFiltrosDTO
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string CPF { get; set; }
        public int? ContratanteId { get; set; }
        public int ContratanteUsuario { get; set; }
        public int GrupoId { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public bool Administrador { get; set; }
    }
}