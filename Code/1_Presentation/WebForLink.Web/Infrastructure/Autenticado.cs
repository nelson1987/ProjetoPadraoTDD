using System.Collections.Generic;

namespace WebForLink.Web.Infrastructure
{
    public class Autenticado
    {
        public int? ContratanteId { get; set; }
        public int? TipoContratante { get; set; }
        public int UsuarioId { get; set; }
        public string NomeCompletoUsuario { get; set; }
        public string NomeReduzidoUsuario { get; set; }
        public string Imagem { get; set; }
        public string NomeEmpresa { get; set; }
        public string Estilo { get; set; }
        public int? Grupo { get; set; }
        public bool SolicitaDocumentos { get; set; }
        public bool Principal { get; set; }
        public bool SolicitaFichaCadastral { get; set; }
        public List<int> Perfil { get; set; }
    }
}